using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] int pointsModifiers;
    [SerializeField] bool isPositive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isPositive) 
            {
                //GameManager.Instance.points += pointsModifiers;
            }
            else
            {
                //GameManager.Instance.points -= pointsModifiers;
            }
            AudioManager.Instance.PlaySFX(0);
            gameObject.SetActive(false);
        }
    }
}
