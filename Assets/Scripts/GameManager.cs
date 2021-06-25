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
        public GameObject tooltip;
        public Text output;
        RectTransform arrow;

        Queue<Message> messages;
        Dictionary<GameObject, ProgressBar> progressBars;

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
            tooltip.SetActive(false);
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
            var offset = GetScreenPos(target.transform.position);
            arrow.anchoredPosition = new Vector2(offset.x, offset.y + 150);
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
                var offset = GetScreenPos(source.transform.position);
                current.offsetX = offset.x;
                current.offsetY = offset.y;
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

        public void UpdateTooltip(Vector2 offset, float progress)
        {
            var target = tooltip.GetComponent<ProgressBar>();
            var screenOffset = GetScreenPos(offset);
            target.offsetX = screenOffset.x;
            target.offsetY = screenOffset.y;
            target.progress = progress;
        }

        public void ShowTooltip(bool show) {
            tooltip.SetActive(show);
        }

        private Vector2 GetScreenPos(Vector2 point)
        {
            var offset = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(point), null, out offset);
            offset.x += 960;
            offset.y += 540;

            return offset;
        }
    }
}