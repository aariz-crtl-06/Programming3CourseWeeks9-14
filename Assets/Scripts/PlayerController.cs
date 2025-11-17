using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D rb;

    public float maxSpeed = 14f;
    public float accel = 12f;
    public float decel = 60f;
    public float airAccel = 8f;
    public float airDecel = 30f;

    public enum FacingDirection
    {
        left, right
    }

    public FacingDirection direction;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        UpdateFacingDirection();
    }

    void FixedUpdate()
    {
        MovementUpdate();
    }

  
        private void MovementUpdate()
        {
        float targetSpeed = moveInput * maxSpeed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;

        float accelRate = 0f;

        
        if (IsGrounded())
        {
            if (Mathf.Abs(targetSpeed) > 0.01f)
            {
                accelRate = accel;
            }

            else
            {
                accelRate = decel;
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

        float movement = speedDiff * accelRate;

        rb.AddForce(Vector2.right * movement);
        }


    private void UpdateFacingDirection()
    {
        if (moveInput > 0)
        {
            direction = FacingDirection.right;
        }

        if (moveInput < 0)
        {
            direction = FacingDirection.left;
        }
    }

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

    public bool IsGrounded()
    {
        return true;
    }

    public FacingDirection GetFacingDirection()
    {
        return direction;
    }
}