using UnityEngine;

namespace AstroSurveyor
{
    public class Container : MonoBehaviour
    {
        bool isCarried = false;
        float dropTime = 0f;
        bool dropping = false;
        Vector3 carriedPos = new Vector3(0, 0.85f, 0);
        Vector3 groundPos = new Vector3(0, 0.48f, 0);
        Transform sprite;
        GameObject parent;

        public bool IsCarried { get => isCarried; }

        void Start()
        {
            dropTime = 0f;
            sprite = transform.GetChild(0);
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
        }

        public void OnPickUp(GameObject parent)
        {
            this.parent = parent;
            isCarried = true;
            sprite.transform.localPosition = carriedPos;
        }

        public void OnDrop()
        {
            this.parent = null;
            isCarried = false;
            dropping = true;
            dropTime = 0f;
        }
    }
}
