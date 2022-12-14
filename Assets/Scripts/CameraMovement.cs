using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 touchStart;

    public float zoomOutMin = 0;
    public float zoomOutMax = 70;

    // use this for initialization    
    private void Start()
    {

    }
    public static Vector3 GetPosition()
    {
        Ray ray;
        RaycastHit hit;
        int ground_layer = LayerMask.GetMask("TouchPlane");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, ground_layer);
        // Debug.Log(Physics.Raycast(ray, out hit, Mathf.Infinity, instance.touchPlaneLayerMask));
        return hit.point;
    }

    // Update is called once per frame
    private void Update()
    {
        // runs at the start of the touch
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("This ran");
            touchStart = GetPosition();
            // Debug.Log(touchStart);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {

            Vector3 direction = touchStart - GetPosition();
            // Debug.Log(direction);
            direction.y = 0;
            transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        // Debug.Log(increment);
        float y = -(increment * 30);
        // Debug.Log(y);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + y, zoomOutMin, zoomOutMax), transform.position.z);
        // Debug.Log(Camera.main.fieldOfView);
    }

}
