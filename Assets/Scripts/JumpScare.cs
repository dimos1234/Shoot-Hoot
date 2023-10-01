using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public Animator wallMoveAnim;
    public MovePlayer movePlayer;
    public AudioSource JumpScareSound;
    public GameObject Picture;
    bool PlayedEffect = false;
    bool animDone = false;

    void Start()
    {
        Picture.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (movePlayer.jumpCounter == 3 && !PlayedEffect)
        {
            JumpScareSound.Play();
            Picture.gameObject.SetActive(true);
            Invoke("DeActivateJumpScare", 2);
            PlayedEffect = true;
        }

        if (PlayedEffect)
        {
            wallMoveAnim.SetBool("isJumpscared", true);
            animDone = true;
        }
    }

    void DeActivateJumpScare()
    {
        Picture.gameObject.SetActive(false);
    }
}
