using TMPro;
using UnityEngine;

public class showFps : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsLabel;
    private float deltaTime;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("showFps") == 1) {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            if (fps < 80000) {
                fpsLabel.text = "FPS: " + Mathf.Ceil (fps).ToString ();
            } else {
                fpsLabel.text = "FPS: " + Mathf.Ceil (80000).ToString ();
            }
        }
    }
}
