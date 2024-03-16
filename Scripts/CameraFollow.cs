using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
