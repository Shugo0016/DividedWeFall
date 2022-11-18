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

    private void Update()
    {
        transform.position = TouchWorld.GetPosition();
    }
    // Checks location of touch on the plane. 
    public static Vector3 GetPosition()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, instance.touchPlaneLayerMask);
        // Debug.Log(Physics.Raycast(ray, out hit, Mathf.Infinity, instance.touchPlaneLayerMask));
        return hit.point;


        // Code For IOS USE

        //if (Input.touchCount > 0)
        //{
        //    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, instance.touchPlaneLayerMask))
        //    {
        //        return hit.point;
        //    }
        //}
        ////
        //return instance.transform.position;



        // Code For click use
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, instance.touchPlaneLayerMask))
        //    {
        //        result = hit.point;
        //    }
        //}
        //return result;
    }

}
