using UnityEngine;

//Zooms camera
public class CameraZoomPredator : MonoBehaviour
{
    public Camera cam;
    public float zoomSpeed = 5f;
    public float currentFOV;
    [SerializeField] private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            if(Input.GetKey(KeyCode.Z)) {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, 3f, zoomSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.X)) {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, 60f, zoomSpeed * Time.deltaTime);
            }
            currentFOV = cam.fieldOfView;
            //Debug.Log(currentFOV);
        }
    }
}
