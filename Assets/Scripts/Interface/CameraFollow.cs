using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    Transform t;

    void Start()
    {
        t = target.transform;    
    }

    void Update()
    {
        if (target != null) {
            transform.position = new Vector3(t.position.x, t.position.y, transform.position.z);
        }
    }
}
