using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public GameObject cameraController;

    public float rotationSpeed = 10f;
    public void TurnCameraClockWise()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        rotationVector.y = 30.0f;

        cameraController.transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    public void TurnCameraAntiClockWise()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        rotationVector.y = -30.0f;

        cameraController.transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
}
