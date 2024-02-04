using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int startingPonit;

    [SerializeField] Transform[] rutePoints;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = rutePoints[startingPonit].position;
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMove();
    }

    void PlatformMove() 
    {
        if(Vector2.Distance(transform.position, rutePoints[i].position) < 0.02f) { 
            i++; 
            if(i == rutePoints.Length) 
            {
                i = 0; 
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, rutePoints[i].position, speed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (transform.position.y<collision.transform.position.y)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
