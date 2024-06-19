using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objectToFollow;
    public Transform cube;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x,objectToFollow.transform.position.y - 0.75f,objectToFollow.transform.position.z);
    }
}
