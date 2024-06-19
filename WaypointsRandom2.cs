using UnityEngine;

public class WaypointsRandom2 : MonoBehaviour
{

    [SerializeField] private NpcMovement2 npcMovement2;
    
    // Start is called before the first frame update
   public Transform GetNextWaypoint2(Transform currentWaypoint, int targetIndex) {
        int spawnNumber = npcMovement2.getVehiclePath(targetIndex);
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
