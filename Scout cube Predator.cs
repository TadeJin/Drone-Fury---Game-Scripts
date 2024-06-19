using UnityEngine;

//Scout drone cube rotation, rotation angle limitation
public class ScoutCubePredator : MonoBehaviour
{
    private Vector2 turn;
    [SerializeField] private PauseMenu pauseMenu;
    //public GameObject cam2;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(100f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused) {
            turn.y -= Input.GetAxis("Mouse Y") * PlayerPrefs.GetFloat("mouseSens");
            if (turn.y < 90f) {
                    turn.y = 90f;
            }

            if (turn.y > 270f) {
                    turn.y = 270f;
            }

            transform.localRotation = Quaternion.Euler(Mathf.Clamp(turn.y,90,270),0,0);
        }
    }
}