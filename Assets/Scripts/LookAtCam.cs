using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    [SerializeField] private bool invert;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            Vector3 cameraDir = (cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + cameraDir * -1);
        }
        else
        {
            transform.LookAt(cameraTransform);
        }
    }
}
