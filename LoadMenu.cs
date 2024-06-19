using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("returnToMenu",5);
    }

      private void returnToMenu() {
        SceneManager.LoadScene("main_menu");
    }
}
