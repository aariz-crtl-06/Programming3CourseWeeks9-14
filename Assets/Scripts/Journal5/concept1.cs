using JetBrains.Annotations;
using UnityEngine;

public class concept1 : MonoBehaviour
{
    public Rigidbody2D rb;
    float torqueAmount = 10f; 
    void Start()
    {
        
    }

    void Update()
    {
        //Gets rigidbody3d component
        rb = GetComponent<Rigidbody2D>();
        //if G is pressed, give the object torque force rotation
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Apply a burst of rotational force
            rb.AddTorque(torqueAmount, ForceMode2D.Impulse);
        }
    }
}
