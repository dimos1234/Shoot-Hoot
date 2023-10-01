using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int levelInHoot = 0;
    public bool inHoot = false;
    public bool flashOn = true;
    public float batteryLife = 5f;
    public float flashtime = 0;
    public float chargeTime = 10f;
    public float currentBatteryLife;
    public Animator playerAnimator;
    public float transitionTime = 1f;
    public GameObject screen;
    public Vector3 startInVector = new Vector3(0, 1, 7);
    public DollManager dollman;
    public NightmareManager nightmareman;
    public RectTransform batteryImage;
    public bool firstLoad = true;

    void Start()
    {
        currentBatteryLife = batteryLife;
    }

    void Awake()
    {
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if(gameManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if (!inHoot)
        {
            batteryImage = GameObject.FindGameObjectWithTag("battery").GetComponent<RectTransform>();
        }

        dollman = GameObject.FindGameObjectWithTag("DollManager").GetComponent<DollManager>();
        nightmareman = GameObject.FindGameObjectWithTag("NightmareManager").GetComponent<NightmareManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inHoot)
        {
            batteryImage = GameObject.FindGameObjectWithTag("battery").GetComponent<RectTransform>();
            batteryImage.localScale = new Vector3(currentBatteryLife / 10, currentBatteryLife / 10, batteryImage.localScale.z);
        }

        if (inHoot)
        {
            batteryImage = null;
        }

        if (inHoot && !(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainLevelArea"))
        {
            levelInHoot = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        }

        if (Input.GetKeyUp(KeyCode.Tab) && !inHoot)
        {
            GameObject[] objectList = GameObject.FindGameObjectsWithTag("object");
            for (int i = 0; i < objectList.Length; i++)
            {
                if (objectList[i].name == "screen")
                {
                    screen = objectList[i];
                    Debug.Log(screen.name);
                }
            }

            if (screen.GetComponent<Outline>().enabled == true)
            {
                MoveToScreen();
            }
        }


        else if (Input.GetKeyUp(KeyCode.Tab) && inHoot)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainLevelArea");
            MoveOutScreen();
        }

        if (Input.GetKeyDown("f") && !flashOn)
        {
            flashOn = true;
            Debug.Log("On");
        }

        else if (Input.GetKeyDown("f") && flashOn)
        {
            flashOn = false;
            Debug.Log("Off");
        }

        if (!flashOn && currentBatteryLife <= 0)
        {
            flashOn = true;
        }

        else if (!flashOn && currentBatteryLife >= 0)
        {
            currentBatteryLife -= Time.deltaTime;
        }

        else if (flashOn && currentBatteryLife <= batteryLife)
        {
            currentBatteryLife += Time.deltaTime;
        }

    }

    void MoveToScreen()
    {
        StartCoroutine(LoadLevelIn());
    }

    void MoveOutScreen()
    {
        StartCoroutine(LoadLevelOut());
    }

    IEnumerator LoadLevelIn()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        Debug.Log("Done in");
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        playerAnimator.SetBool("outHootAnim", false);
        playerAnimator.SetBool("inHootAnim", true);

        yield return new WaitForSeconds(transitionTime);

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        if (firstLoad)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelInHoot + 1);
            firstLoad = false;
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelInHoot);
        }

        inHoot = true;
    }

    IEnumerator LoadLevelOut()
    {

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainLevelArea")
        {
            yield return new WaitForSeconds(0.0001f);
        }

        Debug.Log("Done out");
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerAnimator.gameObject.GetComponent<Transform>().position = startInVector;
        playerAnimator.SetBool("inHootAnim", false);
        playerAnimator.SetBool("outHootAnim", true);

        yield return new WaitForSeconds(transitionTime);

        inHoot = false;
        dollman.moved = false;
        nightmareman.moved = false;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void ded()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainLevelArea");
        levelInHoot = 0;
        inHoot = false;
        flashOn = true;
        batteryLife = 5f;
        flashtime = 0;
        chargeTime = 10f;
        transitionTime = 1f;
        startInVector = new Vector3(0, 1, 7);
        firstLoad = true;
    }
}