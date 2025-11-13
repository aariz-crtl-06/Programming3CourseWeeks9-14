using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public enum FacingDirection
    {
        left, right
    }

    public FacingDirection direction;
    private float moveInput;

    void Start()
    {
        
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        MovementUpdate();
        UpdateFacingDirection();
    }

    private void MovementUpdate()
    {
        transform.position += new Vector3(moveInput * moveSpeed * Time.deltaTime, 0f, 0f);
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