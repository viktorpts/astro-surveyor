using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace AstroSurveyor
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Canvas canvas;
        public GameObject progressBarTemplate;
        public GameObject inventory;

        private Queue<Message> messages;
        private Dictionary<GameObject, ProgressBar> progressBars;
        public Text output;
        public RectTransform arrow;

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one instance!");
                return;
            }

            Instance = this;
        }

        void Start()
        {
            messages = new Queue<Message>();
            progressBars = new Dictionary<GameObject, ProgressBar>();
            var arrowObject = GameObject.FindWithTag("TargetPointer");
            arrow = arrowObject.GetComponent<RectTransform>();
        }

        void Update()
        {
            foreach (var message in messages)
            {
                message.aliveFor -= Time.deltaTime;
            }

            while (messages.Count > 0 && messages.Peek().aliveFor <= 0)
            {
                messages.Dequeue();
            }

            output.text = string.Join("\n", messages.Select(x => x.Content).ToArray());
        }

        public void ShowMessage(string msg)
        {
            messages.Enqueue(new Message(msg));
        }

        public void ShowTarget(GameObject target)
        {
            var offsetX = Camera.main.WorldToScreenPoint(target.transform.position).x;
            var offsetY = Camera.main.WorldToScreenPoint(target.transform.position).y;
            arrow.anchoredPosition = new Vector2(offsetX, offsetY + 150);
            arrow.gameObject.SetActive(true);
        }

        public void HideTarget()
        {
            arrow.gameObject.SetActive(false);
        }

        public void UpdateProgressBar(GameObject source, float progress)
        {
            if (progressBars.ContainsKey(source) == false)
            {
                var bar = Instantiate(progressBarTemplate);
                bar.transform.SetParent(canvas.transform);
                progressBars.Add(source, bar.GetComponent<ProgressBar>());
            }

            var current = progressBars[source];

            if (progress >= 1)
            {
                progressBars.Remove(source);
                Destroy(current.gameObject);
            }
            else
            {
                current.progress = progress;
                current.offsetX = Camera.main.WorldToScreenPoint(source.transform.position).x;
                current.offsetY = Camera.main.WorldToScreenPoint(source.transform.position).y;
            }
        }

        public void HideProgressBar(GameObject source)
        {
            if (progressBars.ContainsKey(source))
            {
                var current = progressBars[source];
                progressBars.Remove(source);
                Destroy(current.gameObject);
            }
        }

        public void UpdateInventory(GameObject item, int slotIndex)
        {
            var target = inventory.transform.GetChild(slotIndex).gameObject;
            if (item == null)
            {
                target.SetActive(false);
            }
            else
            {
                var image = target.GetComponent<Image>();
                image.sprite = item.GetComponentInChildren<SpriteRenderer>().sprite;
                image.preserveAspect = true;
                target.SetActive(true);
            }
        }
    }
}