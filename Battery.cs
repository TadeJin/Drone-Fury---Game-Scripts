using UnityEngine;
using UnityEngine.UI;

public class Baterry : MonoBehaviour
{

    public float batterylife = 120f;
    [SerializeField] private RawImage bat100;
    [SerializeField] private RawImage bat75;
    [SerializeField] private RawImage bat50;
    [SerializeField] private RawImage bat25;
    [SerializeField] private cameraEffect signal;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Playermovement playermovement;
    
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused && playermovement.offGround) {
            batterylife-= Time.deltaTime;
            if (!signal.signalLost) {

                if (batterylife > 90) {
                    bat100.enabled = true;
                    bat75.enabled = false;
                    bat50.enabled = false;
                    bat25.enabled = false;
                } else if (batterylife <= 90 && batterylife > 60) {
                    bat100.enabled = false;
                    bat75.enabled = true;
                    bat50.enabled = false;
                    bat25.enabled = false;
                } else if (batterylife <= 60 && batterylife > 30) {
                    bat100.enabled = false;
                    bat75.enabled = false;
                    bat50.enabled = true;
                    bat25.enabled = false;
                } else if (batterylife <= 30) {
                    bat100.enabled = false;
                    bat75.enabled = false;
                    bat50.enabled = false;
                    bat25.enabled = true;
                }
            } else {
                bat100.enabled = false;
                bat75.enabled = false;
                bat50.enabled = false;
                bat25.enabled = false;
            }
        }
    }
}
