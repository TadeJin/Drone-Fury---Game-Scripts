using UnityEngine;

public class PropellerMovement : MonoBehaviour
{
    public float rotationSpeed = 2500f;
    [SerializeField] private bool rotationDirection = false;
    [SerializeField] private bool throttleAdjustSpeed;
    [SerializeField] private PredatorUI predatorUI;
     // Update is called once per frame
    void Update()
    {
        if (!throttleAdjustSpeed) {
            if (rotationDirection) {
                transform.Rotate(0f,0f,-rotationSpeed * Time.deltaTime, Space.Self);
            } else {
                transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime, Space.Self);
            }
        } else {
            float speed = rotationSpeed * (predatorUI.throttle / 100);
            if (rotationDirection) {
                transform.Rotate(0f,0f,-speed * Time.deltaTime, Space.Self);
            } else {
                transform.Rotate(0f,0f,speed * Time.deltaTime, Space.Self);
            }
        }
        
    }
}
