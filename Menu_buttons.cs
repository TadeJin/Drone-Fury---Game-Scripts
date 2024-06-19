using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_buttons : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider mouseSense;
    [SerializeField] private TextMeshProUGUI mouseSenseValueDisplay;
    private int pom;

    void Start() {
        Cursor.lockState = CursorLockMode.None;
        if (PlayerPrefs.GetInt("showFps") == 1) {
            toggle.isOn = true;
        } else if (PlayerPrefs.GetInt("showFps") == 0) {
            toggle.isOn = false;
        }
        if (PlayerPrefs.GetFloat("mouseSens") != 0) {
             mouseSense.value = PlayerPrefs.GetFloat("mouseSens");
        } else {
            mouseSense.value = 1;
        }
    }
    
    void Update() {
        if (toggle.isOn) {
            pom = 1;
        } else {
            pom = 0;
        }
        PlayerPrefs.SetInt("showFps",pom);
        PlayerPrefs.SetFloat("mouseSens",mouseSense.value);
        mouseSenseValueDisplay.text = Math.Round(mouseSense.value,2).ToString(); 
    }
    public void Play()
    {   
        
    }
    public void Destroy()
    {
        Application.Quit();
    }

    public void FPV() {
        SceneManager.LoadScene("Game");
    }

    public void MQ9() {
        SceneManager.LoadScene("Game 2");
    }
}
