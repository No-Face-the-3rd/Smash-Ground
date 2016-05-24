using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {
    public GameObject target;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        if(transform.position != target.transform.position + offset)
        {
            Vector3 tmp = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref tmp, 0.1f);
        }
    }


}
