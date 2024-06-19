using UnityEngine;

public class GuidanceCollision : MonoBehaviour
{
    public bool BTR;
    public bool URAL;
    public bool KAMAZ;
    public bool T72;
    public bool SU57;
    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "BTR") {
            BTR = true;
            Debug.Log("BTR");
        }
        else if (col.gameObject.tag == "URAL") {
            URAL = true;
        }
        else if (col.gameObject.tag == "KAMAZ") {
            KAMAZ = true;
        }
        else if (col.gameObject.tag == "T72") {
            T72 = true;
        }
        else if (col.gameObject.tag == "SU57") {
            SU57 = true;
        } else {
            BTR = false;
            SU57 = false;
            URAL = false;
            KAMAZ = false;
            T72 = false;
        }
     }
}
