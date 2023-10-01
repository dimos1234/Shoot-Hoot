using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public AudioSource jumpSound;
    public CharacterController2D controller;
    public float runspeed = 40f;
    float horizontal = 0f;
    bool isJump = false;
    bool isJumped = false;
    public int jumpCounter = 0;
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * runspeed;
        if (Input.GetKey(KeyCode.Space) && !isJumped)
        {
            jumpSound.Play();
            isJump = true;
            isJumped = true;
            jumpCounter += 1;
        }
    }
    private void FixedUpdate()
    {
        controller.Move(horizontal * Time.fixedDeltaTime, false, isJump);
        isJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumped = false;
    }
}
