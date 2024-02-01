using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRaduis;
    [SerializeField] GameObject groundCheck;


    // Start is called before the first frame update
    void Start()
    {
        groundCheck = GameObject.Find("GroundCheck");
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speedTemporal = speed;

    }

    // Update is called once per frame
    void Update()
    {
        Movment();
        Jump();
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRaduis, groundLayer); //chequer de gruand en vase al enmpty posicionado en el objeto player
        anim.SetBool("Jump", !isGrounded);
        if (!isGrounded)
        {
            speedTemporal = Mathf.Lerp(0, speed, 0.7f);
            return;
        }
        else
        {

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
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded)
            {

                if (!boots) { return; }
                else
                {
                    if (haveDobleJump)
                    {
                        haveDobleJump = false;
                        playerRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                        
                    }
                }
            }
            else
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    void Attack() { }
    void Flip() { }
}
