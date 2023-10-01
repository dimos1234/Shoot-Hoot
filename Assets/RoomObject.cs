using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    public Vector3 travelVector = new Vector3(-2.5f, 1f, 6.5f);
    public Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "vision")
        {
            GetComponent<Outline>().enabled = true;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "vision")
        {
            GetComponent<Outline>().enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Outline>().enabled == true)
        {
            player.position = travelVector;
        }
    }
}
