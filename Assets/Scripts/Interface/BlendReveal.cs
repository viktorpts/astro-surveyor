using UnityEngine;

public class BlendReveal : MonoBehaviour
{
    SpriteRenderer hull;
    private float transition = 1f;
    private bool inZone = false;

    void Start()
    {
        hull = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inZone)
        {
            if (transition > 0.25f)
                transition -= Time.deltaTime;
            if (transition < 0.25f)
            {
                transition = 0.25f;
            }
            hull.color = new Color(1, 1, 1, Mathf.Lerp(0.25f, 1, transition));
        } else {
            if (transition < 1)
                transition += Time.deltaTime;
            if (transition > 1)
            {
                transition = 1;
            }
            hull.color = new Color(1, 1, 1, Mathf.Lerp(0.25f, 1, transition));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inZone = false;
        }
    }
}
