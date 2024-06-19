using UnityEngine;

public class turretCollision : MonoBehaviour
{
    [SerializeField] private GameObject turretFire;
    [SerializeField] private GameObject turretSmoke;
     private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Ground") {
           turretFire.SetActive(true);
           turretSmoke.SetActive(true);
        }
     }
}
