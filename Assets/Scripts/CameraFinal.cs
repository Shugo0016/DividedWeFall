
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraFinal : MonoBehaviour
{

    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float xSpeed = 5.0f;
    public float ySpeed = 5.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public float zoomRate = 10.0f;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    private Vector3 FirstPosition;
    private Vector3 SecondPosition;
    private Vector3 delta;
    private Vector3 lastOffset;
    private Vector3 lastOffsettemp;
    //private Vector3 CameraPosition;
    //private Vector3 Targetposition;
    //private Vector3 MoveDistance;

    private Touchscreen screen;


    void Start() { Init(); }
    void OnEnable() { Init(); }

    public void Init()
    {
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        //be sure to grab the current rotations as starting points.
        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);

        Debug.Log("CAMERA MOVEMENT INIT SUCCESS\nTOUCHSCREEN INPUT FOUND: " + (Touchscreen.current != null).ToString().ToUpper());
        screen = Touchscreen.current;
    }

    /*
      * Camera logic on LateUpdate to only update after all character movement logic has been handled.
      */
    void LateUpdate()
    {
        if (!Application.isEditor)
        {
            List<TouchControl> touches = ActiveTouches(screen);
            Debug.Log("touches: " + touches.Count.ToString());

            //ZOOM & PAN
            if (touches.Count == 2)
            {
                Debug.Log("CAM_MOVE Two Touches");
                TouchControl touchZero = touches[0];

                TouchControl touchOne = touches[1];


                //ZOOM
                Vector2 touchZeroPreviousPosition = touchZero.position.ReadValue() - touchZero.delta.ReadValue();

                Vector2 touchOnePreviousPosition = touchOne.position.ReadValue() - touchOne.delta.ReadValue();


                float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;

                float TouchDeltaMag = (touchZero.position.ReadValue() - touchOne.position.ReadValue()).magnitude;


                float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

                desiredDistance += deltaMagDiff * Time.deltaTime * zoomRate * 0.0025f * Mathf.Abs(desiredDistance);

                //PAN
                //store previous center position of touches
                Vector2 previousPositionMiddle = (touchZeroPreviousPosition + touchOnePreviousPosition) / 2;

                //store current center position of touches
                Vector2 currentPositionMiddle = (touchZero.position.ReadValue() + touchOne.position.ReadValue()) / 2;

                //store delta of previous two
                Vector2 middlePositionDelta = currentPositionMiddle - previousPositionMiddle;


                lastOffset = targetOffset;
                //set a multiplier for de facto pan speed based on zoom level
                float speedMulti = 0.003f * panSpeed * currentDistance;
                //move targetoffset on the XZ plane according to the delta
                targetOffset = lastOffset + transform.right * middlePositionDelta.x * speedMulti + Quaternion.Euler(0, -90, 0) * transform.right * middlePositionDelta.y * speedMulti;
            }
            //ORBIT
            if (touches.Count == 1 && touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                Debug.Log("CAM_MOVE Moving Single Touch");
                Vector2 touchposition = touches[0].delta.ReadValue();
                xDeg += touchposition.x * xSpeed * 0.002f;
                yDeg -= touchposition.y * ySpeed * 0.002f;
                yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
            }
        }
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        currentRotation = transform.rotation;
        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = rotation;

        /*
        if (Mouse.current.leftButton.isPressed)
        {
            FirstPosition = Mouse.current.position.ReadValue();
            lastOffset = targetOffset;
        }
 
        if (Mouse.current.rightButton.isPressed)
        {
            SecondPosition = Mouse.current.position.ReadValue();
            delta = SecondPosition - FirstPosition;
            targetOffset = lastOffset + transform.right * delta.x * 0.003f + transform.up * delta.y * 0.003f;
 
        }
        */

        ////////Orbit Position

        // affect the desired Zoom distance if we roll the scrollwheel
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        position = target.position - (rotation * Vector3.forward * currentDistance);

        position = position - targetOffset;

        transform.position = position;
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    private List<TouchControl> ActiveTouches(Touchscreen screen)
    {
        List<TouchControl> i = new List<TouchControl>();
        foreach (var item in screen.touches)
        {
            if (item.isInProgress)
                i.Add(item);
        }
        return i;
    }
}