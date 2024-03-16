using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assigned to parent object of all waypoint paths for random spawns, assigns starting location and movement on selected path
public class WaypointsRandom : MonoBehaviour
{
    public NpcMovement obj;

    
    // Start is called before the first frame update
    void Start()
    {
    }


     public Transform GetNextWaypoint(Transform currentWaypoint) {
        int spawnNumber = obj.spawnNumber;
        if (currentWaypoint == null) {
            return transform.GetChild(spawnNumber).GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.GetChild(spawnNumber).childCount -1) {

            return transform.GetChild(spawnNumber).GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        // Zde se dá ukončit mise když npc vyjede ven z dosahu
        else {
                return null;
        }
    }
}
