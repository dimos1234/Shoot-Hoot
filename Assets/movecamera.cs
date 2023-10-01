using UnityEngine;

public class movecamera : MonoBehaviour
{
    public Transform cam;
    public float LMAOOOHAHAHHAHA = 10f;
    public bool western_rolling_landscape_flat_plains = true;
    public bool isInside = false;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.tag == "Player" && isInside == false)
        {
            isInside = true;
            if (western_rolling_landscape_flat_plains == true)
            {
                Debug.Log("DNODDLE");
                cam.position = new Vector3(cam.position.x + LMAOOOHAHAHHAHA, cam.position.y, cam.position.z); //western rolling landscape flat plains
                western_rolling_landscape_flat_plains = false;
            }
            
            else if (western_rolling_landscape_flat_plains == false)
            {
                cam.position = new Vector3(cam.position.x - LMAOOOHAHAHHAHA, cam.position.y, cam.position.z); //western rolling landscape flat plains
                western_rolling_landscape_flat_plains = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collisionInfo)
    {
        if (collisionInfo.tag == "Player")
        {
            isInside = false;
        }
    }
}
