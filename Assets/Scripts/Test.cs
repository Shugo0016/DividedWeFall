using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private Unit unit;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.T)) {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(TouchWorld.GetPosition());
            GridPosition startGridPos = new GridPosition(0, 0);

            List<GridPosition> gridPositionList = Pathfinding.Instance.FindPath(startGridPos, mouseGridPosition);
            for(int i = 0; i < gridPositionList.Count - 1; i++)
            {
                Debug.DrawLine(LevelGrid.Instance.GetWorldPosition(gridPositionList[i]),
                               LevelGrid.Instance.GetWorldPosition(gridPositionList[i + 1]), Color.white,10f);
            }
  
       }
    }
}
