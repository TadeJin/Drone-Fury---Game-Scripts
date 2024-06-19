using UnityEngine;

public class Waypointgenerator : MonoBehaviour
{
    public int num = 8;
    private float r = 300f;
    public GameObject parentObject;
    private float x;
    private float z;

    private void Start() {
        float[] theta = new float[num*4+1];
        theta[0] = 0;
        float offset = (2*Mathf.PI)/(num*4);

        for(int i = 0; i < num*4 ; i++)
        {
            theta[i+1] = theta[i] + offset;
        }

        for(int j = 0;j < num*4; j++)
        {
            x = r*Mathf.Cos(theta[j]);
            z = r*Mathf.Sin(theta[j]);

            GameObject obj = new GameObject("Waypoint");
            Instantiate(obj,transform.position = new Vector3(x,0,z),Quaternion.identity);
            obj.transform.SetParent(parentObject.transform);
        }
    }
}
