using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustFollow :MonoBehaviour {

  public NpcMovement obj;


  private void Start() {
    
    transform.position = obj.transform.position;
  }

  private void Update() {
    transform.position = obj.transform.position;
    
    Quaternion lookrotation = Quaternion.LookRotation(new Vector3(obj.currentWaypoint.position.x, obj.waypointAdjust, obj.currentWaypoint.position.z) - transform.position);
    transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * obj.rotateSpeed);
  }
}