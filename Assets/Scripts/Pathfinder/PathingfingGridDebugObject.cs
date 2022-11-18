using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PathingfingGridDebugObject : GridDebugObject
{
    [SerializeField] private TextMeshPro gCostText;

    [SerializeField] private TextMeshPro hCostText;

    [SerializeField] private TextMeshPro fCostText;

    private PathNode pathNode;

    public override void SetGridObject(object gridObject)
    {
        base.SetGridObject(gridObject);
        pathNode = (PathNode)gridObject;
    }

    protected override void Update()
    {
        base.Update();
        gCostText.text = pathNode.GetGCost().ToString();

        hCostText.text = pathNode.GetHCost().ToString();

        fCostText.text = pathNode.GetFCost().ToString();
    }
}
