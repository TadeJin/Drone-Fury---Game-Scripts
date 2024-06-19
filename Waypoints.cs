using UnityEngine;

//Creation of waypoints, for not random path
    public class waypoints : MonoBehaviour
    {
        [Range(0f,2f)]
        [SerializeField] private float waypointSize = 1f;
        public bool loop = true;
        public bool visible = true;
        private void OnDrawGizmos() {
            if (visible) {
            foreach(Transform t in transform) {

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(t.position,waypointSize);
            }

            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount -1; i++) {

                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
            }

            if (loop) {
                Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
            }
        }
        }
    public Transform GetNextWaypoint(Transform currentWaypoint) {

        if (currentWaypoint == null) {

            return transform.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.childCount -1) {

            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        // Zde se dá ukončit mise když npc vyjede ven z dosahu
        else { 
            if (loop) {
                return transform.GetChild(0);
            } else {
                return null;
            }
        }
    }
}
