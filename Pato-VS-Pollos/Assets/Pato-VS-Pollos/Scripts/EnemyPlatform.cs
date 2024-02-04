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

    [Header("Modes")]
    [SerializeField] bool SpecialDie;
    [SerializeField] bool AngryMode;
    [SerializeField] bool haveAttack;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        if (AngryMode) StartCoroutine(Angry());
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
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                if (SpecialDie && Random.Range(0, 10)==1)
                {
                    AudioManager.Instance.PlaySFX(3);
                    anim.SetTrigger("SpecialDie");
                    Destroy(gameObject, 0.50f);
                }
                else
                {
                    AudioManager.Instance.PlaySFX(3);
                    anim.SetTrigger("DIE");
                    Destroy(gameObject, 0.27f);
                }
                //transform.parent.gameObject.SetActive(false);
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
