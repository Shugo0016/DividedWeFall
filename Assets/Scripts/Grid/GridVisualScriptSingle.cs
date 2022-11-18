using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualScriptSingle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private MeshRenderer meshRend;

    public void Show()
    {
        meshRend.enabled = true;

    }

    public void Hide()
    {
        meshRend.enabled = false;
    }
}
