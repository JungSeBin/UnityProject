using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraTransform;

    public AudioClip clip;

    public float moveSpeed = 100.0f;
    public float jumpSpeed = 100.0f;
    public float gravity = -20.0f;

    CharacterController characterController = null;
    float yVelocity = 0.0f;

    bool isWalking = false;
    bool isRunning = false;
    bool isJumping = false;

    Animation anim = null;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
        StartCoroutine(WalkSound(0.3f));
    }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x == 0 && z == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && z > 0)
        {
            moveSpeed = 200.0f;
            isRunning = true;
        }
        else
        {
            moveSpeed = 100.0f;
            isRunning = false;
        }

        Vector3 moveDirection = new Vector3(x, 0, z);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        if (!isJumping)
        {
            if (!isWalking)
            {
                anim.Play("idle");
            }
            else if (isRunning)
            {
                anim.Play("run");
            }
            else if (x > 0)
            {
                anim.Play("strafeRight");
            }
            else if (x < 0)
            {
                anim.Play("strafeLeft");
            }
            else
            {
                anim.Play("walk");
            }
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            yVelocity = jumpSpeed;
            anim.Play("jump");
        }

        yVelocity += (gravity * Time.deltaTime);
        moveDirection.y = yVelocity;

        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVelocity = 0.0f;
        }
    }

    IEnumerator WalkSound(float waitTime)
    {
        while (true)
        {
            if (isRunning)
                yield return new WaitForSeconds(0.2f);
            else
                yield return new WaitForSeconds(waitTime);
            if (isWalking && !isJumping)
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
            }
            yield return null;
        }
    }
}
