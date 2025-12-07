using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D rb;

    //Values for the different movement types
    public float speed = 14f;
    public float acceleration = 12f;
    public float deceleration = 60f;
    public float airAccel = 8f;
    public float airDecel = 30f;

    //Apex height and time to create accurate gravity and jump velocity
    public float apexHeight = 5f;
    public float apexTime = 0.55f;

    public float gravity = 0f;
    public float jumpVelocity = 0f;

    public float terminalSpeed = 20f;

    public float coyoteTime = 0.5f;
    private float coyoteCounter;

    public GameObject dashEffect;
    public GameObject hoverEffect;

    bool canDash = true;
    public bool isDashed = false;

    public float downStrikeSpeed = 60f;
    public bool isLaunched = false;

    //Ground check to see if player is in contact with any ground objects
    int groundContacts = 0;

    //Facig direction enum
    public enum FacingDirection
    {
        left, right
    }

    public FacingDirection direction;
    private float moveInput;

    //References the rigidbody component when game starts
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Set gravity scale to 0 to disable built in gravity
        rb.gravityScale = 0f;

        //Manually calculate gravity
        gravity = (2f * apexHeight) / (apexTime * apexTime);

        // Calculate how fast the player must move upwards initially
        jumpVelocity = gravity * apexTime;
    }

    //Get axis to access movement input
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        UpdateFacingDirection();

        //When grounded, reset the coyote counter. When in the air, then this counter depletes, giving the player some time to jump after leaving a platform
        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && coyoteCounter > 0f)
        {
            Jump();
        }

        Dash();
        StartCoroutine(Hover());
    }

    //Runs in fixed update to use physics
    void FixedUpdate()
    {
        MovementUpdate();

        rb.AddForce(Vector2.down * gravity * rb.mass);

        //terminal velocity to limit falling speed from going too much
        if (rb.linearVelocity.y < -terminalSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -terminalSpeed);
        }

    }

    //Jump function that applies a a jump velocity when the jump button is pressed and the player is grounded
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        }
    }

    void Dash()
    {
        if (Input.GetButtonDown("Jump") && !IsGrounded() && canDash == true)
        {
            float x = 0f;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                x = 1f;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                x = -1f;
            }

            // vertical dash
            if (x == 0f)
            {
                rb.linearVelocity = new Vector2(0f, jumpVelocity * 1.2f);
                dashEffect.SetActive(true);
                isDashed = true;

            }

            // horizontal dash
            else
            {
                Vector2 dashDir = new Vector2(x, 0f);
                dashDir = dashDir.normalized;
                rb.linearVelocity = dashDir * jumpVelocity * 2f;
                dashEffect.SetActive(true);
                isDashed = true;
            }

            canDash = false;
        }

        if (IsGrounded())
        {
            canDash = true;
            dashEffect.SetActive(false);
            isDashed = false;
        }

    }

    IEnumerator Hover()
    {
        if(!IsGrounded() && Input.GetKeyDown(KeyCode.S))
        {
            float originalGravity =gravity;
            gravity = 0.5f;
            yield return new WaitForSeconds(0.5f);
            isLaunched = true;
            hoverEffect.SetActive(true);

            rb.linearVelocity=Vector2.down * downStrikeSpeed;
            gravity = originalGravity;

            yield return new WaitForSeconds(0.3f);

            isLaunched = false;
            isDashed = false;
            hoverEffect.SetActive(false);

        }

    }

        private void MovementUpdate()
        {
        //Start moving at the target speed
        float targetSpeed = moveInput * speed;

        //Difference of target speed by current speed
        float speedDiff = targetSpeed - rb.linearVelocity.x;

        //Acceleration rate to use
        float accelRate = 0f;


        //if grounded, use ground accel/decel or else use air accel/decel
        if (IsGrounded())
        {
            if (Mathf.Abs(targetSpeed) > 0.01f)
            {
                accelRate = acceleration;
            }

            else
            {
                accelRate = deceleration;
            }
        }

        else
        {
            if (Mathf.Abs(targetSpeed) > 0.01f)
            {
                accelRate = airAccel;
            }
            else
            {
                accelRate = airDecel;
            }
        }
        //Mvement amount
        float movement = speedDiff * accelRate;

        //Apply movement force to rigidbody
        rb.AddForce(Vector2.right * movement);
        }


    //If player moves more than zero update facing direction to right
    private void UpdateFacingDirection()
    {
        if (moveInput > 0)
        {
            direction = FacingDirection.right;
        }

        //If player moves less than zero update facing direction to left
        if (moveInput < 0)
        {
            direction = FacingDirection.left;
        }
    }

    //If moving then record movement
    public bool IsWalking()
    {
        if (moveInput != 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    //Uses ground contact to determine if grounded
    public bool IsGrounded()
    {
        return groundContacts > 0;
    }

    

    //Get facing direction
    public FacingDirection GetFacingDirection()
    {
        return direction;
    }


    //Checks to see if the stay trigger is activated by the ground, and if so, determines if the player is grounded
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            groundContacts = 1;  // touching ground
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            groundContacts = 0;  // no longer touching ground
        }
    }
}