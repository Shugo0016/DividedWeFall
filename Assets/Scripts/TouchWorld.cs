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

    public static Vector3 GetPosition()
    {
        Ray ray;
        RaycastHit hit;
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            // Tests RayCast
            //Debug.DrawRay(ray.origin, ray.direction * 20, Color.cyan);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, instance.touchPlaneLayerMask))
            {
                Debug.Log("TouchingGround");

                // transform.position = hit.point;
                //Destroy(hit.transform.gameObject);

                return hit.point;

            }
        }
        return instance.transform.position;
    }

    

}
