using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWorld : MonoBehaviour
{
    private static TouchWorld instance;
    
    [SerializeField] private LayerMask touchPlaneLayerMask;

    private void Awake()
    {
        instance = this;
    }

    // Checks location of touch on the plane. 
    public static Vector3 GetPosition()
    {
        Ray ray;
        RaycastHit hit;

        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, instance.touchPlaneLayerMask))
            {
                return hit.point;
            }
        }
        return instance.transform.position;
    }
}
