using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public CharacterController controller;

    public float speed;
    public float gravity;

    Vector3 moveDirection;

    public AudioSource runAudio;

    public Animator anim;

    void Update()
    {
        Vector2 direction = joystick.direction;

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(-direction.x, 0, -direction.y);

            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            transform.rotation = targetRotation;

            moveDirection = moveDirection * speed;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        if (direction != Vector2.zero)
        {
            if(!runAudio.isPlaying)
                runAudio.Play();
            anim.SetBool("Run", true);

        }
        else
        {
            runAudio.Stop();

            anim.SetBool("Run", false);
        }
    }
}
