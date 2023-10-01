using UnityEngine;

public class flashlight : MonoBehaviour
{
    public GameObject GM;
    public detectObject DO;

    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
        DO = GameObject.FindGameObjectWithTag("vision").GetComponent<detectObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GM.GetComponent<GameManager>().flashOn && DO.inDoor)
        {
            GetComponentInChildren<Light>().intensity = 4.2f;
            GetComponentInChildren<Light>().range = 35f;
        }

        else if (!GM.GetComponent<GameManager>().flashOn)
        {
            GetComponentInChildren<Light>().intensity = 4.2f;
            GetComponentInChildren<Light>().range = 12f;
        }

        else if (GM.GetComponent<GameManager>().flashOn)
        {
            GetComponentInChildren<Light>().intensity = 0;
        }
    }
}
