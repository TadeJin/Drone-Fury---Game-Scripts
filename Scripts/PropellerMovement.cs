using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerMovement : MonoBehaviour
{
    private float rotationSpeed = 2500f;
    [SerializeField] private bool rotationDirection = false;
     // Update is called once per frame
    void Update()
    {
        if (rotationDirection) {
            transform.Rotate(0f,0f,-rotationSpeed * Time.deltaTime, Space.Self);
        } else {
            transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime, Space.Self);
        }
        
    }
}
