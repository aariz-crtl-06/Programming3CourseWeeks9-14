using UnityEngine;

public class concept4 : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;

    public LayerMask obstacleLayers;

    void Start()
    {
        
    }

    void Update()
    {
        //Allows the detector object to move
        float move = Input.GetAxis("Horizontal");
        transform.position += new Vector3(move * moveSpeed * Time.deltaTime, 0f, 0f);

        //Draws line from detector to triangle
        Debug.DrawLine(transform.position, target.position, Color.green);

        //Creates the raycast, keeping in mind a layer of the other obstacle shapes
        RaycastHit2D hit = Physics2D.Linecast(transform.position, target.position, obstacleLayers);

        //If an obstacle is hit along the way, it'll say which game object it is
        if (hit.collider != null)
        {
            Debug.Log("Object Hit: " + hit.collider.name);
        }

        //If nothing else is in line of sight, then it says there's a hit between the detector and the game object
        else
        {
            Debug.Log("LineCast hit detected between " + gameObject.name + " and " + target.name);
        }
    }
}
