using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private Animator anim;
    bool isDead;
    int HealthBoss =5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Angry());
        isDead = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y + 1.5 < collision.transform.position.y)
            {
                if (HealthBoss<=0)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    isDead = true;
                    AudioManager.Instance.PlaySFX(3);
                    anim.SetTrigger("DIE");
                    Destroy(gameObject, 1.20f);
                }
                else
                {
                    HealthBoss -= 1;
                    collision.gameObject.GetComponent<PlayerControllerX>().dmg(2);
                }
                //transform.parent.gameObject.SetActive(false);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerControllerX>().dmg(1);
            }
        }
    }
    IEnumerator Angry()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 8));
            if (isDead) { break; }
            anim.SetTrigger("Atack");
            yield return null;
        }
    }
}
