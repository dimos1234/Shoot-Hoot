using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class checkLevel : MonoBehaviour
{
    public GameManager gameManager;
    public int levelOnScreen;
    public GameObject[] screenPictures;
    public bool firstLoad = true;

    void Start()
    {
        firstLoad = true;
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        levelOnScreen = gameManager.levelInHoot;
        GameObject[] tempscreenPictures = GameObject.FindGameObjectsWithTag("ScreenShot");
        screenPictures = new GameObject[tempscreenPictures.Length];
        for (int i = 0; i < screenPictures.Length; i++)
        {
            for (int f = 0; f < tempscreenPictures.Length; f++)
            {
                if (tempscreenPictures[f].name == "Screenshot" + Convert.ToString(i + 1))
                {
                    screenPictures[i] = tempscreenPictures[f];
                }
            }
        }

        if (screenPictures.Length <= 0)
        {
            return;
        }

        for (int i = 0; i <= screenPictures.Length - 1; i++)
        {
            screenPictures[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        if (firstLoad)
        {
            screenPictures[0].GetComponent<SpriteRenderer>().enabled = true;
        }
        if (levelOnScreen > 0)
        {
            firstLoad = false;
            screenPictures[levelOnScreen - 1].GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
