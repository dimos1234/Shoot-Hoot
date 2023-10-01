using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        Debug.Log(collisionInfo.tag);
        if (collisionInfo.tag == "Player")
        {
            Debug.Log("Next Level");
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
