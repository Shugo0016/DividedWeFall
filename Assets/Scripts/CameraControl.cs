// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraControl : MonoBehaviour
// {

//     Vector3 inputMoveDirection = new Vector3(0, 0, 0);
//     // Start is called before the first frame update
//     void Start()
//     {


//     }

//     // Update is called once per frame
//     private void Update()
//     {
//         // Keyboard Control
//         if (Input.GetKey(KeyCode.W))
//         {
//             inputMoveDirection.z = +1f;
//         }
//         if (Input.GetKey(KeyCode.S))
//         {
//             inputMoveDirection.z = -1f;
//         }
//         if (Input.GetKey(KeyCode.A))
//         {
//             inputMoveDirection.x = -1f;
//         }
//         if (Input.GetKey(KeyCode.D))
//         {
//             inputMoveDirection.x = +1f;
//         }

//         float moveSpeed = 10f;

//         Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
//         transform.position = moveVector * moveSpeed * Time.deltaTime;

//         Vector3 rotationVector = new Vector3(0, 0, 0);
//         if(Input.GetKey(KeyCode.Q))
//         {
//             rotationVector.y = +1f;
//         }
//         if (Input.GetKey(KeyCode.E))
//         {
//             rotationVector.y = -1f;
//         }

//         float rotationSpeed = 100f;
//         transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
//     }
// }
