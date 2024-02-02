using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{
    [SerializeField] float speed;
    float temporalSpeed;
    [SerializeField] int startingPonit;
    private Animator anim;
    private SpriteRenderer sprite;
    bool isDead;
    [SerializeField] Transform[] rutePoints;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Angry());
        temporalSpeed = speed;
        isDead = false;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        transform.position = rutePoints[startingPonit].position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if (!isDead)
        {
            if (Vector2.Distance(transform.position, rutePoints[i].position) < 0.02f)
            {
                i++;
                if (i == rutePoints.Length)
                {
                    i = 0;
                }

                if (rutePoints[i].position.x > transform.position.x)
                { sprite.flipX = true; }
                else { sprite.flipX = false; }
            }
            transform.position = Vector2.MoveTowards(transform.position, rutePoints[i].position, temporalSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y + 0.5 < collision.transform.position.y)
            {
                isDead=true;
                anim.SetTrigger("DIE");
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject,0.22f);
                transform.parent.gameObject.SetActive(false);
            }
            else 
            {
                collision.gameObject.GetComponent<PlayerControllerX>().Die();
            }
        }
    }

    IEnumerator Angry()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 8));
            temporalSpeed *= 2;
            anim.SetBool("Walk2", true);
            yield return new WaitForSeconds(5);
            temporalSpeed = speed;
            anim.SetBool("Walk2", false);
        }
    }
}
