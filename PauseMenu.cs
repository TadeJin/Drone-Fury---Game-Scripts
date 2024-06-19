using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);  
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (paused) {
                resumeGame();
            } else {
                pauseGame();
            }
        }
    }

    public void pauseGame() {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);  
        Time.timeScale = 0f;
        paused = true;
    }

    public void resumeGame() {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);  
        Time.timeScale = 1f;
        paused = false;
    }

    public void goToMenu() {
        SceneManager.LoadScene("Main_menu");
    }

    public void quitGame() {
        Application.Quit();
    }

}
