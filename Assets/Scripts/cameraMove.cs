using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector2 offset;

    public float bottomBound;
    public float topBound;

    public float leftBound;
    public float rightBound;

    // Update is called once per frame
    void FixedUpdate()
    {
        float targetX = target.position.x + offset.x;
        float targetY = target.position.y + offset.y;

        targetY = Mathf.Clamp(targetY, bottomBound, topBound);
        targetX = Mathf.Clamp(targetX, leftBound, rightBound);

        Vector3 desiredPosition = new Vector3(targetX, targetY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
