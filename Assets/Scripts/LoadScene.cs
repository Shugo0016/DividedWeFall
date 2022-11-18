using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadLevel(string load_level)
    {
        SceneManager.LoadScene(load_level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
