using UnityEngine;

namespace AstroSurveyor
{
    public class Container : MonoBehaviour
    {
        bool isCarried = false;
        float dropTime = 0f;
        bool dropping = false;
        float spawnTime = 0f;
        bool spawning = false;
        Vector2 startPos;
        Vector2 finalPos;
        Vector3 carriedPos = new Vector3(0, 0.37f, 0);
        Vector3 groundPos = new Vector3(0, 0, 0);
        Transform sprite;
        GameObject parent;

        public bool IsCarried { get => isCarried; }

        void Start()
        {
            dropTime = 0f;
            sprite = transform.GetChild(0);
            if (sprite.gameObject.CompareTag("Container") == false)
            {
                sprite = sprite.GetChild(0);
            }
        }

        void Update()
        {
            if (isCarried)
            {
                var offset = parent.GetComponent<Character>().handsOffset;
                transform.position = new Vector3(parent.transform.position.x + offset.x, parent.transform.position.y + offset.y, 0);
            }
            if (dropping)
            {
                if (dropTime >= 0.2f)
                {
                    dropping = false;
                    sprite.transform.localPosition = groundPos;
                }
                else
                {
                    dropTime += Time.deltaTime;
                    sprite.transform.localPosition = Vector3.Lerp(carriedPos, groundPos, dropTime * 5);
                }
            }
            if (spawning)
            {
                if (spawnTime >= 0.5)
                {
                    spawning = false;
                    transform.position = finalPos;
                    sprite.transform.localPosition = groundPos;
                }
                else
                {
                    spawnTime += Time.deltaTime;
                    transform.position = new Vector3(Mathf.SmoothStep(startPos.x, finalPos.x, spawnTime * 2), Mathf.SmoothStep(startPos.y, finalPos.y, spawnTime * 2), 0);
                    sprite.transform.localPosition = new Vector3(0, Mathf.SmoothStep(groundPos.y, carriedPos.y, 1 - Mathf.Abs((spawnTime - 0.25f) * 2)), 0);
                }
            }
        }

        public void OnPickUp(GameObject parent)
        {
            this.parent = parent;
            isCarried = true;
            sprite.transform.localPosition = carriedPos;

            var range = gameObject.GetComponent<RangeIndicator>();
            if (range != null)
            {
                range.sprite.SetActive(true);
            }

            // Deactivate stationary equipment on pick up
            var interactive = GetComponent<Interactive>();
            if (interactive != null && interactive.mustCarry == false)
            {
                interactive.Stow();
            }
        }

        public void OnDrop()
        {
            this.parent = null;
            isCarried = false;
            dropping = true;
            dropTime = 0f;

            var range = gameObject.GetComponent<RangeIndicator>();
            if (range != null)
            {
                range.sprite.SetActive(false);
            }

            // Deactivate handheld equipment on drop
            var interactive = GetComponent<Interactive>();
            if (interactive != null && interactive.mustCarry == true)
            {
                interactive.Stow();
            }
        }

        public void AnimateSpawn(Vector2 finalPos)
        {
            spawning = true;
            spawnTime = 0;
            startPos = transform.position;
            this.finalPos = finalPos;
        }
    }
}
