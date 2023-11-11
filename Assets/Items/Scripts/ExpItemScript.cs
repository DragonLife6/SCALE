using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItemScript : MonoBehaviour
{
    [SerializeField] int expGained;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Player"))
        {
            PlayersLvlUp playerScript = other.GetComponent<PlayersLvlUp>();
            if (playerScript != null)
            {
                playerScript.AddExpirience(expGained);
                Destroy(gameObject);
            }
        }
    }
}
