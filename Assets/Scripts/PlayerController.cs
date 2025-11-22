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
    }

    //Get axis to access movement input
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        UpdateFacingDirection();
    }

    //Runs in fixed update to use physics
    void FixedUpdate()
    {
        MovementUpdate();
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