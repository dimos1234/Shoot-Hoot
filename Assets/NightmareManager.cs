using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NightmareManager : MonoBehaviour
{
    public int currentNightmareStage = 0;
    public int prevNightmareStage = 0;
    public float timer = 0;
    public float secondsToMove = 8f;
    public GameObject[] nightmares = new GameObject[5];
    public GameObject[] nightmaresVent = new GameObject[3];
    public GameObject[] nightmaresBed = new GameObject[3];
    public Vector3[] nightmarePositions = new Vector3[5];
    public Vector3[] nightmarePositionsVent = new Vector3[3];
    public Vector3[] nightmarePositionsBed = new Vector3[3];
    public GameObject vision;
    public GameManager gameManager;
    public bool moved = false;
    public string nightmareRegion;
    public string[] possibleRegions = new string[] {"hallway", "vent", "bed"};

    void Awake()
    {
        GameObject[] nightmareManagers = GameObject.FindGameObjectsWithTag("NightmareManager");
        if(nightmareManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        GameObject[] temptempnightmares = GameObject.FindGameObjectsWithTag("nightmare");
        GameObject[] tempnightmares = new GameObject[4];
        for (int i = 0; i < tempnightmares.Length; i++)
        {
            for (int f = 0; f < temptempnightmares.Length; f++)
            {
                if (temptempnightmares[f].name == "NightmarePos" + Convert.ToString(i + 2))
                {
                    tempnightmares[i] = temptempnightmares[f];
                }
            }
        }

        GameObject[] temptempnightmaresVent = GameObject.FindGameObjectsWithTag("nightmare vent");
        GameObject[] tempnightmaresVent = new GameObject[2];
        for (int i = 0; i < tempnightmaresVent.Length; i++)
        {
            for (int f = 0; f < temptempnightmaresVent.Length; f++)
            {
                if (temptempnightmaresVent[f].name == "NightmarePos" + Convert.ToString(i + 1) + "Vent")
                {
                    tempnightmaresVent[i] = temptempnightmaresVent[f];
                }
            }
        }

        GameObject[] temptempnightmaresBed = GameObject.FindGameObjectsWithTag("nightmare bed");
        GameObject[] tempnightmaresBed = new GameObject[2];
        for (int i = 0; i < tempnightmaresBed.Length; i++)
        {
            for (int f = 0; f < temptempnightmaresBed.Length; f++)
            {
                if (temptempnightmaresBed[f].name == "NightmarePos" + Convert.ToString(i + 1) + "Bed")
                {
                    tempnightmaresBed[i] = temptempnightmaresBed[f];
                }
            }
        }

        for (int i = 0; i < nightmares.Length - 1; i++)
        {
            nightmares[i+1] = tempnightmares[i];
        }
        nightmares[0] = GameObject.FindGameObjectWithTag("og");

        for (int i = 0; i < nightmaresBed.Length - 1; i++)
        {
            nightmaresBed[i+1] = tempnightmaresBed[i];
        }
        nightmaresBed[0] = GameObject.FindGameObjectWithTag("og");

        for (int i = 0; i < nightmaresVent.Length - 1; i++)
        {
            nightmaresVent[i+1] = tempnightmaresVent[i];
        }
        nightmaresVent[0] = GameObject.FindGameObjectWithTag("og");

        

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        possibleRegions = new string[] {"hallway", "vent", "bed"};

        nightmareRegion = possibleRegions[UnityEngine.Random.Range(0, possibleRegions.Length)];

        for (int i = 0; i < nightmares.Length; i++)
        {
            nightmarePositions[i] = nightmares[i].GetComponent<Transform>().position;
            nightmares[i].GetComponent<Transform>().position = new Vector3(100, 100, 100);
        }

        for (int i = 0; i < nightmaresVent.Length; i++)
        {
            nightmarePositionsVent[i] = nightmaresVent[i].GetComponent<Transform>().position;
            nightmaresVent[i].GetComponent<Transform>().position = new Vector3(100, 100, 100);
        }

        for (int i = 0; i < nightmaresBed.Length; i++)
        {
            nightmarePositionsBed[i] = nightmaresBed[i].GetComponent<Transform>().position;
            nightmaresBed[i].GetComponent<Transform>().position = new Vector3(100, 100, 100);
        }

        StartCoroutine("MoveNightmare");
    }

    void Update()
    {
        if (!gameManager.inHoot && !moved)
        {
            if (nightmareRegion == "hallway")
            {
                GameObject[] temptempnightmares = GameObject.FindGameObjectsWithTag("nightmare");
                GameObject[] tempnightmares = new GameObject[4];
                for (int i = 0; i < tempnightmares.Length; i++)
                {
                    for (int f = 0; f < temptempnightmares.Length; f++)
                    {
                        if (temptempnightmares[f].name == "NightmarePos" + Convert.ToString(i + 2))
                        {
                            tempnightmares[i] = temptempnightmares[f];
                        }
                    }
                }
                for (int i = 0; i < nightmares.Length - 1; i++)
                {
                    nightmares[i+1] = tempnightmares[i];
                }
                nightmares[0] = GameObject.FindGameObjectWithTag("og");
                nightmares[prevNightmareStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
                nightmares[currentNightmareStage].GetComponent<Transform>().position = nightmarePositions[currentNightmareStage];
                moved = true;
            }

            else if (nightmareRegion == "vent")
            {
                GameObject[] temptempnightmaresVent = GameObject.FindGameObjectsWithTag("nightmare vent");
                GameObject[] tempnightmaresVent = new GameObject[2];
                for (int i = 0; i < tempnightmaresVent.Length; i++)
                {
                    for (int f = 0; f < temptempnightmaresVent.Length; f++)
                    {
                        if (temptempnightmaresVent[f].name == "NightmarePos" + Convert.ToString(i + 1) + "Vent")
                        {
                            tempnightmaresVent[i] = temptempnightmaresVent[f];
                        }
                    }
                }
                for (int i = 0; i < nightmaresVent.Length - 1; i++)
                {
                    nightmaresVent[i+1] = tempnightmaresVent[i];
                }
                
                nightmaresVent[0] = GameObject.FindGameObjectWithTag("og");
                nightmaresVent[prevNightmareStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
                nightmaresVent[currentNightmareStage].GetComponent<Transform>().position = nightmarePositionsVent[currentNightmareStage];
                moved = true;
            }

            else if (nightmareRegion == "bed")
            {
                GameObject[] temptempnightmaresBed = GameObject.FindGameObjectsWithTag("nightmare bed");
                GameObject[] tempnightmaresBed = new GameObject[2];
                for (int i = 0; i < tempnightmaresBed.Length; i++)
                {
                    for (int f = 0; f < temptempnightmaresBed.Length; f++)
                    {
                        if (temptempnightmaresBed[f].name == "NightmarePos" + Convert.ToString(i + 1) + "Bed")
                        {
                            tempnightmaresBed[i] = temptempnightmaresBed[f];
                        }
                    }
                }
                for (int i = 0; i < nightmaresBed.Length - 1; i++)
                {
                    nightmaresBed[i+1] = tempnightmaresBed[i];
                }
                nightmaresBed[0] = GameObject.FindGameObjectWithTag("og");
                nightmaresBed[prevNightmareStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
                nightmaresBed[currentNightmareStage].GetComponent<Transform>().position = nightmarePositionsBed[currentNightmareStage];
                moved = true;
            }
        }
    }

    IEnumerator MoveNightmare()
    {
        for (;;)
        {
            yield return new WaitForSeconds(secondsToMove);
            
            Debug.Log("WAITING DONE NIGHTMARE");

            secondsToMove = UnityEngine.Random.Range(8, 11);
            float moveOrNot = UnityEngine.Random.Range(0, 2);

            if (moveOrNot == 1)
            {
                if (nightmareRegion == "hallway")
                {
                    if (currentNightmareStage < 4)
                    {
                        prevNightmareStage = currentNightmareStage;
                        currentNightmareStage += 1;
                        Debug.Log("Moving Nightmare forward");
                        Debug.Log(prevNightmareStage);
                        Debug.Log(currentNightmareStage);

                        moved = false;
                    }
                    else if (currentNightmareStage == 4)
                    {
                        Debug.Log("Yuo ded");
                        UnityEngine.SceneManagement.SceneManager.LoadScene("death");
                        Cursor.lockState = CursorLockMode.None;
                        currentNightmareStage = 0;
                        nightmareRegion = possibleRegions[UnityEngine.Random.Range(0, possibleRegions.Length)];
                    }
                }

                else if (nightmareRegion == "vent")
                {
                    if (currentNightmareStage < 2)
                    {
                        prevNightmareStage = currentNightmareStage;
                        currentNightmareStage += 1;
                        Debug.Log("Moving Nightmare forward");
                        Debug.Log(prevNightmareStage);
                        Debug.Log(currentNightmareStage);

                        moved = false;
                    }
                    else if (currentNightmareStage == 2)
                    {
                        Debug.Log("Yuo ded");
                        UnityEngine.SceneManagement.SceneManager.LoadScene("death");
                        Cursor.lockState = CursorLockMode.None;
                        currentNightmareStage = 0;
                        nightmareRegion = possibleRegions[UnityEngine.Random.Range(0, possibleRegions.Length)];
                    }
                }

                else if (nightmareRegion == "bed")
                {
                    if (currentNightmareStage < 2)
                    {
                        prevNightmareStage = currentNightmareStage;
                        currentNightmareStage += 1;
                        Debug.Log("Moving Nightmare forward");
                        Debug.Log(prevNightmareStage);
                        Debug.Log(currentNightmareStage);

                        moved = false;
                    }
                    else if (currentNightmareStage == 2)
                    {
                        Debug.Log("Yuo ded");
                        UnityEngine.SceneManagement.SceneManager.LoadScene("death");
                        Cursor.lockState = CursorLockMode.None;
                        currentNightmareStage = 0;
                        nightmareRegion = possibleRegions[UnityEngine.Random.Range(0, possibleRegions.Length)];
                    }
                }
            }

            else if (moveOrNot == 0)
            {
                Debug.Log("Not Moving Nightmare");
            }
        }
    }
}
