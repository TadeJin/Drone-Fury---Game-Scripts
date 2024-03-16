using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    public GameObject FPV;
    public GameObject Target;
    public GameObject DestroyedTarget;
    //public GameObject cam;
    //public GameObject scoutCam;
    [SerializeField] private CameraSwitch mainCam;
    [SerializeField] private Canvas canvasScout;
    [SerializeField] private Canvas canvasFPV;
    [SerializeField] private TextMeshProUGUI hitConfirm;
    [SerializeField] private DetectTarget DetectTarget;
    public bool destruction = false;
    public bool hit = false;
    private bool change = false;
    private bool start = false;
    [SerializeField] Animator animator;
    [SerializeField] private Transform mainCamObject;
    [SerializeField] private Transform soldier1NoAn;
    [SerializeField] private Transform soldier1WithAn;
    [SerializeField] private Transform soldier2NoAn;
    [SerializeField] private Transform soldier2WithAn;
    private bool switched = false;
    [SerializeField] private AudioSource explosion;



    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Ground" && !hit) {
            explosion.Play();
            Debug.Log("Ground");
            destruction = true;
            Invoke("droneDestroyed",2);
        } else if (col.gameObject.tag == "Tree" && !hit) {
            explosion.Play();
            Debug.Log("Tree");
            destruction = true;
            Invoke("droneDestroyed",2);
        } else if (col.gameObject.tag == "Target") {
            explosion.Play();
            Debug.Log("Target");
            destruction = true;
            Invoke("switchCamera",2);
            hit = true;
            DestroyedTarget.transform.position = Target.transform.position;
            DestroyedTarget.transform.rotation = Target.transform.rotation;
            //DestroyedTarget.transform.rotation =  Quaternion.Euler(DestroyedTarget.transform.rotation.x - 77.65f,DestroyedTarget.transform.rotation.y,DestroyedTarget.transform.rotation.z);
            Target.transform.position = new Vector3(Target.transform.position.x,Target.transform.position.y - 20, Target.transform.position.z);
            FPV.transform.position = new Vector3(FPV.transform.position.x,FPV.transform.position.y-20,FPV.transform.position.z);
            hitConfirm.enabled = true;
        } else if (col.gameObject.tag == "Friendly") {
            missionFailed();
        }
    }
    private void Update() {
        if (hit == true && DetectTarget.track && Input.GetKey(KeyCode.Return)) {
            start = true;
            //SceneManager.LoadScene("Mission_completed");
        }
        if (start && (mainCamObject.position != new Vector3(522.656f, 24.8251f, 107.8038f))) {
            mainCamObject.position = Vector3.MoveTowards(mainCamObject.position ,new Vector3(522.656f, 24.8251f, 107.8038f), 0.005f);
        }
        
        if (mainCamObject.position == new Vector3(522.656f, 24.8251f, 107.8038f) && start) {
            if (!switched) {
                soldier1WithAn.position = new Vector3(soldier1NoAn.position.x,28.13f,soldier1NoAn.position.z);
                soldier1NoAn.position = new Vector3(soldier1NoAn.position.x,soldier1NoAn.position.y-20f,soldier1NoAn.position.z);

                soldier2WithAn.position = soldier2NoAn.position;
                soldier2NoAn.position = new Vector3(soldier2NoAn.position.x,soldier2NoAn.position.y-20f,soldier2NoAn.position.z);
                switched = true;
            }

                mainCamObject.rotation = Quaternion.Slerp(mainCamObject.rotation, Quaternion.Euler(-2.163f,-78.766f,-14.113f), 0.05f);
                if (mainCamObject.rotation == Quaternion.Euler(-2.163f,-78.766f,-14.113f)) {
                    animator.enabled = true;
                    Invoke("missionCompleted",3);
            }
        }



        if (change) {
            if (mainCam.moveCamToScout()) {
                change = false;
            } else {
                mainCam.moveCamToScout();
            }
        }
    }
    private void missionFailed(){
        SceneManager.LoadScene("Mission_failed");
    }

    private void droneDestroyed() {
        SceneManager.LoadScene("DroneDestroyed");
    }

    private void missionCompleted() {
        SceneManager.LoadScene("Mission_completed");
    }

    private void switchCamera() {
        change = true;
        
       /* canvasScout.enabled = true;
        hitConfirm.enabled = true;
        canvasFPV.enabled = false;
        cam.SetActive(false);
        scoutCam.SetActive(true);
        RenderSettings.fog = false;*/
    }
}
