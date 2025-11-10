using UnityEngine;

public class concept2 : MonoBehaviour
{
    public GameObject fallSqr;


    void Start()
    {
        
    }

    void Update()
    {
        //Ignore the collision
        Physics2D.IgnoreCollision(fallSqr.GetComponent<Collider2D>(),GetComponent<Collider2D>());

        //Bool and log to show if the ignored collision works
        bool areIgnoring = Physics2D.GetIgnoreCollision(fallSqr.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Debug.Log("Ignored Collisions " + areIgnoring);
    }
}
