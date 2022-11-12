using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GridSys gridSystem;
    // Start is called before the first frame update
    void Start()
    {
        new GridSys(10, 10, 2f);

        Debug.Log(new GridPosition(5, 7));
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            //Debug.Log(gridSystem.GetGridPosition(Input.GetTouch(0).position));
        }
        
    }
}
