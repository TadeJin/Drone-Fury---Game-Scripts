using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Su57Rocket : MonoBehaviour
{
    public bool hit = false;
    [SerializeField] private GameObject laserText;
    [SerializeField] private TextMeshProUGUI zoom;
    [SerializeField] private TextMeshProUGUI throttlePercentage;
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI rocketCount;
    [SerializeField] private TextMeshProUGUI altitude;
    [SerializeField] private TextMeshProUGUI warning;
    [SerializeField] private TextMeshProUGUI speedUI;
    [SerializeField] private RawImage crosshair;
    [SerializeField] private TextMeshProUGUI signalLost;
    [SerializeField] private Volume volume;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private TextMeshProUGUI AamCount;
    [SerializeField] private TextMeshProUGUI src;
    [SerializeField] private TextMeshProUGUI locked;
    [SerializeField] private TextMeshProUGUI missile;
    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Drone") {
            explosion.transform.position = volume.transform.position;
            explosion.Play();
            hit = true;
            laserText.SetActive(false);
            zoom.enabled = false;
            throttlePercentage.enabled = false;
            distance.enabled = false;
            rocketCount.enabled = false;
            altitude.enabled = false;
            warning.enabled = false;
            speedUI.enabled = false;
            crosshair.enabled = false;
            AamCount.enabled = false;
            src.enabled = false;
            locked.enabled = false;
            missile.enabled = false;
            signalLost.enabled = true;

            Invoke("signal",0.125f);
            Invoke("missionFailed",3);
        }
    }

    private void missionFailed() {
        SceneManager.LoadScene("DroneDestroyed");
    }

    private void signal() {
        volume.enabled = true;
    }
}
