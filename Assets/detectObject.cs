using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectObject : MonoBehaviour
{
    public Vector3 travelVectorCloset = new Vector3(-2.5f, 1f, 6.5f);
    public Vector3 travelVectorChair = new Vector3(0, 1, 0);
    public Vector3 travelVectorBed = new Vector3(-5, 0.2f, -3.5f);
    public Vector3 travelVectorVent = new Vector3(11.5f, 1f, -7f);
    public Vector3 travelVectorDoor = new Vector3(10.54f, 1f, 7.13f);

    public Quaternion travelQuaternionBed = new Quaternion(0, -180, 90, 0);
    public Quaternion travelQuaternionVent = new Quaternion(-90, -180, 90, 0);

    public float ventOpenAngle = 90f;
    public Quaternion VentCloseQuat = new Quaternion(0, 0, 0, 0);
    public float doorOpenAngle = 33f;
    public Quaternion DoorCloseQuat = new Quaternion(0, 0, 0, 0);

    public Transform player;
    public GameObject vent;
    public GameObject door;
    public GameObject obj;
    public DollManager dollman;
    public GameManager GM;
    public bool inVent = false;
    public bool inDoor = false;
    public bool inBed = false;
    public bool lookingAtDoll = false;
    public float timer = 0f;
    public float looktime = 3f;
    public bool lookingAtNightmare = false;
    public float nightTimer = 0f;
    public NightmareManager nightmareman;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dollman = GameObject.FindGameObjectWithTag("DollManager").GetComponent<DollManager>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        nightmareman = GameObject.FindGameObjectWithTag("NightmareManager").GetComponent<NightmareManager>();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "object" && collisionInfo.name == "chair")
        {
            if (player.position != travelVectorChair)
            {
                collisionInfo.GetComponent<Outline>().enabled = true;
                obj = collisionInfo.gameObject;
                Debug.Log("lookingAtThing");
            }
        }

        if (collisionInfo.tag == "object" && collisionInfo.name == "closet")
        {
            if (player.position != travelVectorCloset)
            {
                collisionInfo.GetComponent<Outline>().enabled = true;
                obj = collisionInfo.gameObject;
                Debug.Log("lookingAtThing");
            }
        }

        if (collisionInfo.tag == "object" && collisionInfo.name == "screen")
        {
            collisionInfo.GetComponent<Outline>().enabled = true;
            obj = collisionInfo.gameObject;
            Debug.Log("lookingAtThing");
        }

        if (collisionInfo.tag == "object" && collisionInfo.name == "bed")
        {
            inBed = true;
            if (player.position != travelVectorBed)
            {
                collisionInfo.GetComponent<Outline>().enabled = true;
                obj = collisionInfo.gameObject;
                Debug.Log("lookingAtThing");
            }
        }

        if (collisionInfo.tag == "object" && collisionInfo.name == "vent")
        {
            if (player.position != travelVectorVent)
            {
                collisionInfo.GetComponent<Outline>().enabled = true;
                obj = collisionInfo.gameObject;
                Debug.Log("lookingAtThing");
            }
        }

        if (collisionInfo.tag == "object" && collisionInfo.name == "door")
        {
            if (player.position != travelVectorDoor)
            {
                collisionInfo.GetComponent<Outline>().enabled = true;
                obj = collisionInfo.gameObject;
                Debug.Log("lookingAtThing");
            }
        }

        if (collisionInfo.tag == "doll")
        {
            lookingAtDoll = true;
        }

        if (collisionInfo.tag == "nightmare" || collisionInfo.tag == "nightmare vent" || collisionInfo.tag == "nightmare bed")
        {
            lookingAtNightmare = true;
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        GameObject gameObj = collisionInfo.gameObject;
        if (gameObj.tag == "object")
        {
            gameObj.GetComponent<Outline>().enabled = false;
            Debug.Log("notLookingAtThing");
        }

        else if (gameObj.tag == "doll")
        {
            lookingAtDoll = false;
        }

        else if (gameObj.tag == "nightmare" || gameObj.tag == "nightmare vent" || gameObj.tag == "nightmare bed")
        {
            lookingAtNightmare = false;
        }

        if (gameObj.tag == "bed")
        {
            inBed = false;
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && obj.GetComponent<Outline>().enabled == true)
        {
            Debug.Log("mista bombastic");
            if (obj.name == "closet")
            {
                player.position = travelVectorCloset;
                player.rotation = new Quaternion(0, player.rotation.y, 0, player.rotation.w);
                if (inVent)
                {
                    vent.GetComponent<Transform>().rotation = VentCloseQuat;
                    inVent = false;
                }
                if (inDoor)
                {
                    door.GetComponent<Transform>().rotation = DoorCloseQuat;
                    inDoor = false;
                }
            }
            if (obj.name == "chair")
            {
                player.position = travelVectorChair;
                player.rotation = new Quaternion(0, player.rotation.y, 0, player.rotation.w);
                if (inVent)
                {
                    vent.GetComponent<Transform>().rotation = VentCloseQuat;
                    inVent = false;
                }
                if (inDoor)
                {
                    door.GetComponent<Transform>().rotation = DoorCloseQuat;
                    inDoor = false;
                }
            }
            if (obj.name == "bed")
            {
                player.position = travelVectorBed;
                player.rotation = travelQuaternionBed;
                if (inVent)
                {
                    vent.GetComponent<Transform>().rotation = VentCloseQuat;
                    inVent = false;
                }
                if (inDoor)
                {
                    door.GetComponent<Transform>().rotation = DoorCloseQuat;
                    inDoor = false;
                }
            }
            if (obj.name == "vent")
            {
                obj.gameObject.GetComponent<Transform>().Rotate(Vector3.up, ventOpenAngle);
                player.position = travelVectorVent + new Vector3(1f, 0, 0);
                player.rotation = travelQuaternionVent;
                inVent = true;
                vent = obj.gameObject;
                if (inDoor)
                {
                    door.GetComponent<Transform>().rotation = DoorCloseQuat;
                    inDoor = false;
                }
            }
            if (obj.name == "door")
            {
                obj.gameObject.GetComponent<Transform>().Rotate(Vector3.up, doorOpenAngle);
                player.position = travelVectorDoor + new Vector3(10.54f - 8.15f, 1, 7.13f - 4.86f);
                player.rotation = new Quaternion(0, player.rotation.y, 0, player.rotation.w);
                inDoor = true;
                door = obj.gameObject;
                if (inVent)
                {
                    vent.GetComponent<Transform>().rotation = VentCloseQuat;
                    inVent = false;
                }
            }
        }

        if (lookingAtDoll && !GM.flashOn && player.position == travelVectorCloset)
        {
            if (timer < looktime)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }

            else if (timer >= looktime)
            {
                dollman.prevDollStage = dollman.currentDollStage;
                dollman.currentDollStage = 0;
                Debug.Log("Moving Doll back");
                Debug.Log(dollman.prevDollStage);
                Debug.Log(dollman.currentDollStage);

                dollman.dolls[dollman.prevDollStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
                dollman.dolls[dollman.currentDollStage].GetComponent<Transform>().position = dollman.dollPositions[dollman.currentDollStage];
                timer = 0f;
            }
        }
        else
        {
            timer = 0;
        }

        if (lookingAtNightmare && !GM.flashOn)
        {
            if (inDoor)
            {
                if (nightTimer < looktime)
                {
                    nightTimer += Time.deltaTime;
                    Debug.Log(nightTimer);
                }

                else if (nightTimer >= looktime)
                {
                    nightmareman.prevNightmareStage = nightmareman.currentNightmareStage;
                    nightmareman.currentNightmareStage = 0;
                    Debug.Log("Moving Nightmare Back");
                    Debug.Log(nightmareman.prevNightmareStage);
                    Debug.Log(nightmareman.currentNightmareStage);

                    if (nightmareman.nightmareRegion == "hallway")
                    {
                        nightmareman.nightmares[nightmareman.prevNightmareStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
                        nightmareman.nightmares[nightmareman.currentNightmareStage].GetComponent<Transform>().position = nightmareman.nightmarePositions[nightmareman.currentNightmareStage];
                    }
                    nightmareman.nightmareRegion = nightmareman.possibleRegions[Random.Range(0, nightmareman.possibleRegions.Length)];
                    nightTimer = 0;
                }
            }

            else if (inVent)
            {
                if (nightTimer < looktime)
                {
                    nightTimer += Time.deltaTime;
                    Debug.Log(nightTimer);
                }

                else if (nightTimer >= looktime)
                {
                    nightmareman.prevNightmareStage = nightmareman.currentNightmareStage;
                    nightmareman.currentNightmareStage = 0;
                    Debug.Log("Moving Nightmare Back");
                    Debug.Log(nightmareman.prevNightmareStage);
                    Debug.Log(nightmareman.currentNightmareStage);

                    if (nightmareman.nightmareRegion == "vent")
                    {
                        nightmareman.nightmaresVent[nightmareman.prevNightmareStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
                        nightmareman.nightmaresVent[nightmareman.currentNightmareStage].GetComponent<Transform>().position = nightmareman.nightmarePositionsVent[nightmareman.currentNightmareStage];
                    }
                    nightmareman.nightmareRegion = nightmareman.possibleRegions[Random.Range(0, nightmareman.possibleRegions.Length)];
                    nightTimer = 0;
                }
            }

            else if (inBed)
            {
                if (nightTimer < looktime)
                {
                    nightTimer += Time.deltaTime;
                    Debug.Log(nightTimer);
                }

                else if (nightTimer >= looktime)
                {
                    nightmareman.prevNightmareStage = nightmareman.currentNightmareStage;
                    nightmareman.currentNightmareStage = 0;
                    Debug.Log("Moving Nightmare Back");
                    Debug.Log(nightmareman.prevNightmareStage);
                    Debug.Log(nightmareman.currentNightmareStage);

                    if (nightmareman.nightmareRegion == "bed")
                    {
                        nightmareman.nightmaresBed[nightmareman.prevNightmareStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
                        nightmareman.nightmaresBed[nightmareman.currentNightmareStage].GetComponent<Transform>().position = nightmareman.nightmarePositionsBed[nightmareman.currentNightmareStage];
                    }
                    nightmareman.nightmareRegion = nightmareman.possibleRegions[Random.Range(0, nightmareman.possibleRegions.Length)];
                    nightTimer = 0;
                }
            }
        }
    }
}
