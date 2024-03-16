using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_buttons : MonoBehaviour
{
    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Play()
    {   
        SceneManager.LoadSceneAsync(1);
    }
    public void Destroy()
    {
        Application.Quit();
    }
}
