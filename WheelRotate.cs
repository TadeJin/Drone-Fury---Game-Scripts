using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    float rotationSpeed = 300f;
    [SerializeField] private bool rotationDirection = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (rotationDirection) {
                transform.Rotate(-rotationSpeed * Time.deltaTime ,0f,0f, Space.Self);
        } else {
                transform.Rotate(rotationSpeed * Time.deltaTime ,0f,0f, Space.Self);
        }
    }
}
