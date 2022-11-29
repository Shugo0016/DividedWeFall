using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;

        Hide();
    }
    // Function to show the action busy UI
    private void Show()
    {
        gameObject.SetActive(true);
    }

    // function to hide the action busy UI
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // subscribe to the OnBusyChanged event
    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
