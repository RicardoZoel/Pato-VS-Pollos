using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerControllerX : MonoBehaviour
{
    //Variables de referencia
    private Rigidbody2D playerRB;
    private Animator anim;
    private float horizontalInput;

    [Header("Movement & Jump Stats")]
    public float speed;
    public float speedTemporal;
    public float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] bool boots;
    [SerializeField] bool haveDobleJump;
    [SerializeField] bool haveGlide;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRaduis;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Vector3 spawn;

    //[SerializeField] private float gravity;


    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        groundCheck = GameObject.Find("GroundCheck");
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speedTemporal = speed;
        //gravity = playerRB.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        Movment();
        //Jump2();
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRaduis, groundLayer); //chequer de gruand en vase al enmpty posicionado en el objeto player
        anim.SetBool("Jump", !isGrounded);
        if (!isGrounded)
        {
            speedTemporal = Mathf.Lerp(0, speed, 0.7f);
            return;
        }
        else
        {
            anim.SetBool("Glaide", false);
            playerRB.gravityScale = 1;
            speedTemporal = speed;
            if (boots)
            {
                haveDobleJump = true;
            }
        }

    }
    void Movment()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //Modificamos la X del RB que sera input* Speed
        playerRB.velocity = new Vector2(horizontalInput * speedTemporal, playerRB.velocity.y);

        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            anim.SetBool("Walk", true);
        }
        if (horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("Walk", true);
        }
        if (horizontalInput == 0)
        {
            anim.SetBool("Walk", false);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if ((context.performed && isGrounded))
        {

            AudioManager.Instance.PlaySFX(2);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }

        if (context.performed && !isGrounded && haveDobleJump)
        {
            AudioManager.Instance.PlaySFX(2);
            haveDobleJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }

        if (context.canceled && playerRB.velocity.y > 0f)
        {

            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
        }
    }

    public void Glide(InputAction.CallbackContext context)
    {
        if (haveGlide)
        {
            bool isDown = false;
            if (playerRB.velocity.y < 0) isDown = true;
            if ((context.performed && !isGrounded && isDown))
            {
                anim.SetBool("Glaide", true);
                playerRB.gravityScale = 0.2f;
            }
        }

    }
    //Sin el new sistem input
    void Jump2()
    {
        bool pressed = Input.GetKey(KeyCode.Space);
        if ((pressed && isGrounded))
        {
            AudioManager.Instance.PlaySFX(2);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && haveDobleJump)
        {
            haveDobleJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            AudioManager.Instance.PlaySFX(2);
        }

        if (Input.GetKeyUp(KeyCode.Space) && playerRB.velocity.y > 0f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
            AudioManager.Instance.PlaySFX(2);
        }
    }
    void Attack() { }
    void Flip() { }
    public void Die()
    {
        AudioManager.Instance.PlaySFX(1);
        Debug.Log("pringo");
        transform.position = spawn;
        GameObject.Find("Main Camera").GetComponent<Follow>().ResetStart();
    }

    public void dmg(int option)
    {
        if (option == 2)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);

        }
        else if (option == 1)
        {
            Die();
        }
    }
}
