using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DollManager : MonoBehaviour
{
    public int currentDollStage = 0;
    public int prevDollStage = 0;
    public float timer = 0;
    public float secondsToMove = 13f;
    public GameObject[] dolls;
    public Vector3[] dollPositions = new Vector3[3];
    public GameObject vision;
    public GameManager gameManager;
    public bool moved = false;

    void Awake()
    {
        GameObject[] dollManagers = GameObject.FindGameObjectsWithTag("DollManager");
        if(dollManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        dolls = new GameObject[3];
        GameObject[] tempDolls = GameObject.FindGameObjectsWithTag("doll");
        for (int i = 0; i < dolls.Length; i++)
        {
            for (int f = 0; f < tempDolls.Length; f++)
            {
                if (tempDolls[f].name == "DollPos" + Convert.ToString(i + 1))
                {
                    dolls[i] = tempDolls[f];
                }
            }
        }

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < dolls.Length; i++)
        {
            dollPositions[i] = dolls[i].GetComponent<Transform>().position;
            if (!(dolls[i].name == "DollPos1"))
            {
                dolls[i].GetComponent<Transform>().position = new Vector3(100, 100, 100);
            }
        }

        StartCoroutine("MoveDoll");
    }

    void Update()
    {
        if (!gameManager.inHoot && !moved)
        {
            GameObject[] tempDolls = GameObject.FindGameObjectsWithTag("doll");
            for (int i = 0; i < dolls.Length; i++)
            {
                for (int f = 0; f < tempDolls.Length; f++)
                {
                    if (tempDolls[f].name == "DollPos" + Convert.ToString(i + 1))
                    {
                        dolls[i] = tempDolls[f];
                    }
                }
            }
            dolls[prevDollStage].GetComponent<Transform>().position = new Vector3(100, 100, 100);
            dolls[currentDollStage].GetComponent<Transform>().position = dollPositions[currentDollStage];
            moved = true;
        }
    }

    IEnumerator MoveDoll()
    {
        for (;;)
        {
            yield return new WaitForSeconds(secondsToMove);
            
            Debug.Log("WAITING DONE");

            secondsToMove = UnityEngine.Random.Range(13, 17);
            float moveOrNot = UnityEngine.Random.Range(0, 2);

            if (moveOrNot == 1)
            {
                if (currentDollStage < 2)
                {
                    prevDollStage = currentDollStage;
                    currentDollStage += 1;
                    Debug.Log("Moving Doll forward");
                    Debug.Log(prevDollStage);
                    Debug.Log(currentDollStage);

                    moved = false;
                }
                else if (currentDollStage == 2)
                {
                    Debug.Log("Yuo ded");
                    UnityEngine.SceneManagement.SceneManager.LoadScene("death");
                    Cursor.lockState = CursorLockMode.None;
                    currentDollStage = 0;
                }
            }

            else if (moveOrNot == 0)
            {
                Debug.Log("Not Moving Doll");
            }
        }
    }
}
