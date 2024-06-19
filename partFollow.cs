using UnityEngine;

public class partFollow : MonoBehaviour
{
    [SerializeField] private NpcMovement2 npcMovement2;
    [SerializeField] private Transform log;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform T72;
    // Update is called once per frame
    void Update()
    {
        if (npcMovement2.notDestroyed[0]) {
            log.position = new Vector3(T72.position.x + 5.2089f,T72.position.y +  1.553f,T72.position.z - 5.4152f);
            log.rotation = T72.rotation;
            turret.position = T72.position;
            turret.rotation = T72.rotation;
        }
    }
}
