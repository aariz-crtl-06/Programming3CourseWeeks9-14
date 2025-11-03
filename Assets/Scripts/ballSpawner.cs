using System.Collections;
using UnityEngine;

public class ballSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public int ballSpawnCount = 100;
    public float ballSpawnDelay = 0.05f;
    public bool randomColours = true;
   

    IEnumerator Start()
    {
        for (int i = 0; i < ballSpawnCount; i++)
        {

            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            Rigidbody2D body2D = ball.GetComponent<Rigidbody2D>();

            //Random insideUnitCircle.Normalized because the lengths can change too and we want them to be one
            body2D.AddForce(Random.insideUnitCircle.normalized, ForceMode2D.Impulse);

            if(randomColours )
            {
                ball.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            }

            yield return new WaitForSeconds(ballSpawnDelay);


        }
    }
}
