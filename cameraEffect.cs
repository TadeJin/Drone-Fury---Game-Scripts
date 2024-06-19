using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class cameraEffect : MonoBehaviour
{
    [SerializeField] private Volume postproc;
    private FilmGrain grain;
    private ColorAdjustments colorAdj;
    private Vector3 pocatecni;
    public float vzdal;
    private float vzdalY;
    private bool nizko = false;
    private RaycastHit hit;
    public bool far = false;
    public bool signalLost = false;
    public Collisions col;
    [SerializeField] private RawImage signal100;
    [SerializeField] private RawImage signal75;
    [SerializeField] private RawImage signal50;
    [SerializeField] private RawImage signal25;



    // Start is called before the first frame update
    void Start()
    {
        pocatecni = new Vector3(500f,10f,500f);
        postproc.profile.TryGet(out colorAdj);
        postproc.profile.TryGet(out grain);
        grainIntenAdjustM(10f);
        grainRespAdjustM(0f);
        colorAdjuContrastM(0f);
        colorAdjuSaturationM(0f);
        signal100.enabled = true;
        signal75.enabled = false;
        signal50.enabled = false;
        signal25.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
    Ray ray1 = new Ray(transform.position, Vector3.down);
    
    if(Physics.Raycast(ray1, out hit)) {
        if (hit.collider.tag == "Ground") {
            vzdalY = hit.distance;
        }
    }

    vzdal = Vector2.Distance(xz(pocatecni),xz(postproc.transform.position));
     if (vzdalY <= 10){
        nizko = true;
    }
    else{
        nizko = false;
    }

    if (!signalLost && !col.hit) {
        if (nizko && vzdal < 400) {
            signal100.enabled = false;
            signal75.enabled = true;
        } else if (vzdal >= 400 && vzdal < 450) {
            signal100.enabled = false;
            signal75.enabled = true;
        } else if (vzdal >= 450 && vzdal < 500) {
            signal75.enabled = false;
            signal50.enabled = true;
        } else if (vzdal >= 500) {
            signal50.enabled = false;
            signal25.enabled = true;
        } else {
            signal100.enabled = true;
            signal75.enabled = false;
            signal50.enabled = false;
            signal25.enabled = false;
        }
    } else {
        signal100.enabled = false;
        signal75.enabled = false;
        signal50.enabled = false;
        signal25.enabled = false;
        if (vzdal >= 520) {
            Invoke("missionFailed",3);
        }
    }

    if (col.destruction) {
        colorAdjuContrastM(-100f);
        colorAdjuSaturationM(-100f);
        grainIntenAdjustM(1000f);
        grainRespAdjustM(1000f);
        signalLost = true;
    } else {
        if (vzdal > 400f && vzdal < 520f){
            far = true;
            signalLost = false;
            if (nizko) {
                grainIntenAdjustM((vzdal-400)*10f * (10 - vzdalY));
                grainRespAdjustM((vzdal-400)*10f * (10 - vzdalY));
                colorAdjuContrastM((400 - vzdal) * 0.25f *(10 - vzdalY));
                colorAdjuSaturationM((400 - vzdal) * 0.25f *(10 - vzdalY));
            } else {
                grainIntenAdjustM((vzdal-400)*10f);
                grainRespAdjustM((vzdal-400)*10f);
                colorAdjuContrastM((400 - vzdal) * 0.25f);
                colorAdjuSaturationM((400 - vzdal) * 0.25f);
            }

    
        } else if (vzdal > 510f){
            colorAdjuContrastM(-50f);
            colorAdjuSaturationM(-50f);
            grainIntenAdjustM((vzdal-400)*50f);
            grainRespAdjustM((vzdal-400)*50f);
            signalLost = true;
        } else {
            far = false;
            signalLost = false;
        }

        if (postproc.transform.position.y > 60f && postproc.transform.position.y < 150f) {
            far = true;
            signalLost = false;
            signal100.enabled = false;
            signal50.enabled = true;
            grainIntenAdjustM((vzdalY-60)*10f);
            grainRespAdjustM((vzdalY-60)*10f);
            colorAdjuContrastM(-(vzdalY - 60) * 0.25f);
            colorAdjuSaturationM(-( vzdalY - 60) * 0.25f);
        } else if (postproc.transform.position.y > 150f) {
            colorAdjuContrastM(-50f);
            colorAdjuSaturationM(-50f);
            grainIntenAdjustM((vzdalY)*50f);
            grainRespAdjustM((vzdalY)*50f);
            signalLost = true;
        }

        if (nizko && !far){
                colorAdjuContrastM((vzdalY -10)*5f);
                colorAdjuSaturationM((vzdalY-10)*5f);
                grainIntenAdjustM((10 - vzdalY)*50f);
                grainRespAdjustM((10 -vzdalY)*50f);
            }
            else if (!nizko && !far && !signalLost){
                colorAdjuContrastM(-10);
                colorAdjuSaturationM(-10);
                grainIntenAdjustM(75);
                grainRespAdjustM(75);
            }
        }
    }

    public void grainIntenAdjustM(float proc){
        // proc/100 (0:100)
        grain.intensity.Override(proc/100);
    }

    public void grainRespAdjustM(float proc){
        // proc/100 (0:100)
        grain.response.Override(proc/100);
    }



    public void colorAdjuContrastM(float proc){
        // proc (-100:100)
        colorAdj.contrast.Override(proc);
    }

    public void colorAdjuSaturationM(float proc){
        // proc (-100:100)
        colorAdj.saturation.Override(proc);
    }

    public static Vector2 xz(Vector3 vv)
		{
		return new Vector2(vv.x, vv.z);
		}
    private void missionFailed() {
        SceneManager.LoadScene("DroneDestroyed");
    }
}
