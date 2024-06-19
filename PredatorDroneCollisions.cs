using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PredatorDroneCollisions : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI laserText;
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
    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Ground") {
            explosion.transform.position = volume.transform.position;
            explosion.Play();
            laserText.enabled = false;
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
            signalLost.enabled = true;

            volume.enabled = true;
            Invoke("missionFailed",3);
        }
    }

    private void missionFailed() {
        SceneManager.LoadScene("DroneDestroyed");
    }

    void Update() {
        if (transform.position.x > 2000 || transform.position.x < -1400 || transform.position.y > 500 || transform.position.z > 1900 || transform.position.z < -1990) {
            laserText.enabled = false;
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
            signalLost.enabled = true;

            volume.enabled = true;
            Invoke("missionFailed",3);
        }
    }
}
