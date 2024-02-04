using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(0);
            GameObject.Find("GameManager").GetComponent<GameManager>().loadNextScene();
            //gameObject.SetActive(false);
        }
    }
}
