using System;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionsPredator : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem explosionBig1;
    [SerializeField] private ParticleSystem explosionBig2;
    [SerializeField] private ParticleSystem explosionBig3;
    [SerializeField] private ParticleSystem explosionBig4;
    [SerializeField] private ParticleSystem explosionBig5;
    [SerializeField] private GameObject T72;
    [SerializeField] private GameObject T72Destroyed;
    [SerializeField] private Rigidbody turretT72;
    [SerializeField] private Rigidbody log;
    [SerializeField] private GameObject T72fireSmoke;
    [SerializeField] private GameObject T72DirtSmoke;
    [SerializeField] private GameObject BTR;
    [SerializeField] private GameObject BTRDestroyed;
    [SerializeField] private Rigidbody turretBTR;
    [SerializeField] private Rigidbody wheelBTR1;
    [SerializeField] private Rigidbody wheelBTR2;
    [SerializeField] private Rigidbody wheelBTR3;
    [SerializeField] private Rigidbody wheelBTR4;
    [SerializeField] private Rigidbody wheelBTR5;
    [SerializeField] private Rigidbody wheelBTR6;
    [SerializeField] private GameObject URAL;
    [SerializeField] private GameObject URALDestroyed;
    [SerializeField] private Rigidbody Bodywork;
    [SerializeField] private Rigidbody URALWheel1;
    [SerializeField] private Rigidbody URALWheel2;
    [SerializeField] private Rigidbody URALWheel3;
    [SerializeField] private Rigidbody URALDoor;
    [SerializeField] private Rigidbody URALBodyWork;
    [SerializeField] private GameObject KAMAZ;
    [SerializeField] private GameObject KAMAZDestroyed;
    [SerializeField] private Rigidbody KAMAZWheel1;
    [SerializeField] private Rigidbody KAMAZWheel2;
    [SerializeField] private Rigidbody KAMAZWheel3;
    [SerializeField] private Rigidbody KAMAZBodyWork;
    [SerializeField] private NpcMovement2 npcMovement2;
    [SerializeField] private SU57Movement sU57Movement;
    [SerializeField] private GameObject su57;
    [SerializeField] private GameObject su57Airframe;
    [SerializeField] private GameObject su57Destroyed;
    [SerializeField] private GameObject canopy;
    [SerializeField] private GameObject su57Right1;
    [SerializeField] private GameObject su57Right2;
    [SerializeField] private GameObject su57Right3;
    [SerializeField] private GameObject su57Right4;
    [SerializeField] private GameObject su57Right5;
    [SerializeField] private GameObject su57Right6;
    [SerializeField] private GameObject su57Left1;
    [SerializeField] private GameObject su57Left2;
    [SerializeField] private GameObject su57Left3;
    [SerializeField] private GameObject su57Left4;
    [SerializeField] private GameObject su57Left5;
    [SerializeField] private GameObject su57Left6;
    [SerializeField] private GameObject su57Left7;
    [SerializeField] private GameObject su57Left8;

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Ground") {
            explosion.transform.position = new Vector3(rocket.transform.position.x - 6.93f,0,rocket.transform.position.z + 2.31f);
            explosion.Play();
            rocket.SetActive(false);
        }

        if(col.gameObject.tag == "T72") {
            rocket.SetActive(false);
            T72.transform.GetChild(T72.transform.childCount-1).gameObject.SetActive(false);
            T72.transform.GetChild(T72.transform.childCount-2).gameObject.SetActive(false);
            explosionBig1.transform.position = T72.transform.position;
            explosionBig1.Play();
            
            T72.transform.position = new Vector3(T72.transform.position.x,-2,T72.transform.position.z);
            T72Destroyed.transform.position = new Vector3(T72.transform.position.x,T72.transform.position.y + 4.77f, T72.transform.position.z);
            T72Destroyed.transform.rotation = T72.transform.rotation;
            T72DirtSmoke.SetActive(false);
            T72fireSmoke.SetActive(false);
            T72.GetComponent<AudioSource>().Play();
            npcMovement2.notDestroyed[0] = false;
            
            turretT72.AddForce(transform.up * 1000);
            turretT72.AddForce(transform.right * 500);
            turretT72.GetComponent<BoxCollider>().enabled = true;
            turretT72.useGravity = true;
            

            log.GetComponent<BoxCollider>().enabled = true;
            log.AddForce(transform.up * 1000);
            log.AddForce(transform.forward * 500 * -1);
            log.useGravity = true;
        }

        if(col.gameObject.tag == "BTR") {
            rocket.SetActive(false);
            BTR.transform.GetChild(BTR.transform.childCount - 1).gameObject.SetActive(false);
            explosionBig2.transform.position = BTR.transform.position;
            explosionBig2.Play();
            BTR.GetComponent<AudioSource>().Play();
            
            rocket.SetActive(false);
            npcMovement2.notDestroyed[1] = false;
            
            turretBTR.GetComponent<BoxCollider>().enabled = true;
            rocket.SetActive(false);
            turretBTR.AddForce(transform.up * 1000);
            turretBTR.AddForce(transform.right * 500);
            turretBTR.useGravity = true;

            
            wheelBTR1.AddForce(transform.up * 1000);
            wheelBTR1.AddForce(transform.right * 500);
            wheelBTR1.AddForce(transform.forward * 500);
            wheelBTR1.GetComponent<BoxCollider>().enabled = true;
            wheelBTR1.useGravity = true;
            wheelBTR1.GetComponent<WheelRotate>().enabled = false;

            
            wheelBTR2.AddForce(transform.up * 1000);
            wheelBTR2.AddForce(transform.right * 500);
            wheelBTR2.GetComponent<BoxCollider>().enabled = true;
            wheelBTR2.useGravity = true;
            wheelBTR2.GetComponent<WheelRotate>().enabled = false;

            wheelBTR3.AddForce(transform.up * 1000);
            wheelBTR3.AddForce(transform.right * 500);
            wheelBTR3.AddForce(transform.forward * 500 * -1);
            wheelBTR3.GetComponent<BoxCollider>().enabled = true;
            wheelBTR3.useGravity = true;
            wheelBTR3.GetComponent<WheelRotate>().enabled = false;

            wheelBTR4.AddForce(transform.up * 1000);
            wheelBTR4.AddForce(transform.right * 500 * -1);
            wheelBTR4.GetComponent<BoxCollider>().enabled = true;
            wheelBTR4.useGravity = true;
            wheelBTR4.GetComponent<WheelRotate>().enabled = false;

            wheelBTR5.AddForce(transform.up * 1000);
            wheelBTR5.AddForce(transform.right * 500 * -1);
            wheelBTR5.GetComponent<BoxCollider>().enabled = true;
            wheelBTR5.useGravity = true;
            wheelBTR5.GetComponent<WheelRotate>().enabled = false;

            wheelBTR6.AddForce(transform.up * 1000);
            wheelBTR6.AddForce(transform.right * 500 * -1);
            wheelBTR6.AddForce(transform.forward * 500 * -1);
            wheelBTR6.GetComponent<BoxCollider>().enabled = true;
            wheelBTR6.useGravity = true;
            wheelBTR6.GetComponent<WheelRotate>().enabled = false;

            BTR.transform.position = new Vector3(BTR.transform.position.x,-2.5f,BTR.transform.position.z);
            BTRDestroyed.transform.position = new Vector3(BTR.transform.position.x,0,BTR.transform.position.z);
            BTRDestroyed.transform.rotation = BTR.transform.rotation;
        }

        if(col.gameObject.tag == "URAL") {
            rocket.SetActive(false);
            URAL.transform.GetChild(URAL.transform.childCount-1).gameObject.SetActive(false);
            URAL.GetComponent<AudioSource>().Play();
            explosionBig3.transform.position = URAL.transform.position;
            explosionBig3.Play();

            
            URAL.transform.position = new Vector3(URAL.transform.position.x,-4.5f,URAL.transform.position.z);
            URALDestroyed.transform.position = new Vector3(URAL.transform.position.x,0,URAL.transform.position.z);
            URALDestroyed.transform.rotation = Quaternion.Euler(URAL.transform.rotation.x + 8.833f,URAL.transform.rotation.y  - 2.342f,URAL.transform.rotation.z - 19.986f);
            
            npcMovement2.notDestroyed[2] = false;
            URALWheel1.transform.position = new Vector3(URALWheel1.transform.position.x, URALWheel1.transform.position.y + 4,URALWheel1.transform.position.z);
            URALWheel2.transform.position = new Vector3(URALWheel2.transform.position.x, URALWheel2.transform.position.y + 4,URALWheel2.transform.position.z);
            URALWheel3.transform.position = new Vector3(URALWheel3.transform.position.x, URALWheel3.transform.position.y + 4,URALWheel3.transform.position.z);
            
            URALWheel1.AddForce(transform.up * 1000);
            URALWheel1.AddForce(transform.right * 500);
            URALWheel1.AddForce(transform.forward * 500);
            URALWheel1.GetComponent<BoxCollider>().enabled = true;
            URALWheel1.useGravity = true;
            URALWheel1.GetComponent<WheelRotate>().enabled = false;

                
            URALWheel2.AddForce(transform.up * 1000);
            URALWheel2.AddForce(transform.right * 500 * -1);
            URALWheel2.GetComponent<BoxCollider>().enabled = true;
            URALWheel2.useGravity = true;
            URALWheel2.GetComponent<WheelRotate>().enabled = false;

            URALWheel3.AddForce(transform.up * 1000);
            URALWheel3.AddForce(transform.right * 500 * -1);
            URALWheel3.AddForce(transform.forward * 500 * -1);
            URALWheel3.GetComponent<BoxCollider>().enabled = true;
            URALWheel3.useGravity = true;
            URALWheel3.GetComponent<WheelRotate>().enabled = false;
           

            URALDoor.AddForce(transform.up * 1000);
            URALDoor.AddForce(transform.right * 500 * -1);
            URALDoor.GetComponent<BoxCollider>().enabled = true;
            URALDoor.useGravity = true;

        
            URALBodyWork.transform.position = new Vector3(URALBodyWork.transform.position.x, URALBodyWork.transform.position.y + 4,URALBodyWork.transform.position.z);
            URALBodyWork.AddForce(transform.up * 1000);
            URALBodyWork.AddForce(transform.right * 500);
            URALBodyWork.GetComponent<BoxCollider>().enabled = true;
            URALBodyWork.useGravity = true;
        }

        if(col.gameObject.tag == "KAMAZ") {
            rocket.SetActive(false);
            KAMAZ.transform.GetChild(KAMAZ.transform.childCount-1).gameObject.SetActive(false);
            KAMAZ.GetComponent<AudioSource>().Play();
            explosionBig4.transform.position = KAMAZ.transform.position;
            explosionBig4.Play();
            KAMAZ.GetComponent<Animator>().enabled = false;
            KAMAZBodyWork.gameObject.SetActive(true);

            KAMAZ.transform.position = new Vector3(KAMAZ.transform.position.x,-4.5f,KAMAZ.transform.position.z);
            KAMAZDestroyed.transform.position = new Vector3(KAMAZ.transform.position.x,0,KAMAZ.transform.position.z);
            KAMAZDestroyed.transform.rotation = Quaternion.Euler(KAMAZ.transform.rotation.x + 8.833f,KAMAZ.transform.rotation.y  - 2.342f,KAMAZ.transform.rotation.z - 19.986f);
            
            npcMovement2.notDestroyed[3] = false;
            KAMAZWheel1.transform.position = new Vector3(KAMAZWheel1.transform.position.x, KAMAZWheel1.transform.position.y + 4,KAMAZWheel1.transform.position.z);
            KAMAZWheel2.transform.position = new Vector3(KAMAZWheel2.transform.position.x, KAMAZWheel2.transform.position.y + 4,KAMAZWheel2.transform.position.z);
            KAMAZWheel3.transform.position = new Vector3(KAMAZWheel3.transform.position.x, KAMAZWheel3.transform.position.y + 4,KAMAZWheel3.transform.position.z);
            
            KAMAZWheel1.AddForce(transform.up * 1000);
            KAMAZWheel1.AddForce(transform.right * 500);
            KAMAZWheel1.AddForce(transform.forward * 500);
            KAMAZWheel1.GetComponent<BoxCollider>().enabled = true;
            KAMAZWheel1.useGravity = true;

                
            KAMAZWheel2.AddForce(transform.up * 1000);
            KAMAZWheel2.AddForce(transform.right * 500 * -1);
            KAMAZWheel2.GetComponent<BoxCollider>().enabled = true;
            KAMAZWheel2.useGravity = true;

            KAMAZWheel3.AddForce(transform.up * 1000);
            KAMAZWheel3.AddForce(transform.right * 500 * -1);
            KAMAZWheel3.AddForce(transform.forward * 500 * -1);
            KAMAZWheel3.GetComponent<BoxCollider>().enabled = true;
            KAMAZWheel3.useGravity = true;

        
            KAMAZBodyWork.transform.position = new Vector3(KAMAZBodyWork.transform.position.x, KAMAZBodyWork.transform.position.y + 4,KAMAZBodyWork.transform.position.z);
            KAMAZBodyWork.AddForce(transform.up * 1000);
            KAMAZBodyWork.AddForce(transform.right * 500);
            KAMAZBodyWork.GetComponent<BoxCollider>().enabled = true;
            KAMAZBodyWork.useGravity = true;
        }

        if (col.gameObject.tag == "SU57") {
            rocket.SetActive(false);
            transform.gameObject.SetActive(false);
            sU57Movement.destroyed = true;
            
            
            explosionBig5.transform.position = su57.transform.position;
            explosionBig5.Play();
            
            su57Destroyed.transform.position = new Vector3(su57.transform.position.x,su57.transform.position.y + 3,su57.transform.position.z);
            su57Destroyed.GetComponent<AudioSource>().Play();
            su57Destroyed.GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
            
            su57Destroyed.GetComponent<Rigidbody>().useGravity = true;
            su57Destroyed.transform.GetChild(0).transform.rotation = Quaternion.Euler(su57.transform.rotation.x + 8.051f,su57.transform.rotation.y+180,su57.transform.rotation.z + 10.248f);
            su57Airframe.transform.position = new Vector3(su57.transform.position.x,-20f,su57.transform.position.z);

            canopy.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            canopy.GetComponent<Rigidbody>().AddForce(transform.right * 1000);
            canopy.GetComponent<Rigidbody>().useGravity = true;


            su57Right1.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Right1.GetComponent<Rigidbody>().AddForce(transform.right * 500);
            su57Right1.GetComponent<Rigidbody>().useGravity = true;
            su57Right1.GetComponent<Rigidbody>().AddForce(transform.forward * 500);

            su57Right2.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Right2.GetComponent<Rigidbody>().AddForce(transform.right * 500);
            su57Right2.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Right2.GetComponent<Rigidbody>().useGravity =true;

            su57Right3.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Right3.GetComponent<Rigidbody>().AddForce(transform.right * 500);
            su57Right3.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Right3.GetComponent<Rigidbody>().useGravity =true;

            su57Right4.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Right4.GetComponent<Rigidbody>().AddForce(transform.right * 500);
            su57Right4.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Right4.GetComponent<Rigidbody>().useGravity =true;

            su57Right5.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Right5.GetComponent<Rigidbody>().AddForce(transform.right * 500);
            su57Right5.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Right5.GetComponent<Rigidbody>().useGravity =true;

            su57Right6.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Right6.GetComponent<Rigidbody>().AddForce(transform.right * 500);
            su57Right6.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Right6.GetComponent<Rigidbody>().useGravity = true;

            su57Left1.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left1.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left1.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left1.GetComponent<Rigidbody>().useGravity = true;

            su57Left2.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left2.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left2.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left2.GetComponent<Rigidbody>().useGravity = true;

            su57Left3.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left3.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left3.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left3.GetComponent<Rigidbody>().useGravity = true;

            su57Left4.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left4.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left4.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left4.GetComponent<Rigidbody>().useGravity = true;

            su57Left5.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left5.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left5.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left5.GetComponent<Rigidbody>().useGravity = true;

            su57Left6.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left6.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left6.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left6.GetComponent<Rigidbody>().useGravity = true;

            su57Left7.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left7.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left7.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left7.GetComponent<Rigidbody>().useGravity = true;

            su57Left8.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            su57Left8.GetComponent<Rigidbody>().AddForce(transform.right * 500 * -1);
            su57Left8.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            su57Left8.GetComponent<Rigidbody>().useGravity = true;

            canopy.GetComponent<BoxCollider>().enabled = true;

            su57Right1.GetComponent<BoxCollider>().enabled = true;
            su57Right2.GetComponent<BoxCollider>().enabled = true;
            su57Right3.GetComponent<BoxCollider>().enabled = true;
            su57Right4.GetComponent<BoxCollider>().enabled = true;
            su57Right5.GetComponent<BoxCollider>().enabled = true;
            su57Right6.GetComponent<BoxCollider>().enabled = true;

            su57Left1.GetComponent<BoxCollider>().enabled = true;
            su57Left2.GetComponent<BoxCollider>().enabled = true;
            su57Left3.GetComponent<BoxCollider>().enabled = true;
            su57Left4.GetComponent<BoxCollider>().enabled = true;
            su57Left5.GetComponent<BoxCollider>().enabled = true;
            su57Left6.GetComponent<BoxCollider>().enabled = true;
            su57Left7.GetComponent<BoxCollider>().enabled = true;
            su57Left8.GetComponent<BoxCollider>().enabled = true;

        }
    }
}

