using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public void TurnCameraClockWise()
    {
        Camera.main.transform.Rotate(0.0f, 30.0f, 0.0f, Space.World);
    }

    public void TurnCameraAntiClockWise()
    {
        Camera.main.transform.Rotate(0.0f, -30.0f, 0.0f, Space.World);
    }
}
