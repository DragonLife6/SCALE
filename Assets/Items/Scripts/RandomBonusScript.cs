using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBonusScript : MonoBehaviour
{
    [SerializeField] GameObject[] bonusPrefabs;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add animation
            SpawnRandomBonus();
        }
    }


    private void SpawnRandomBonus()
    {
        int randomItem;
        int randomItemPick = Random.Range(0, 100);

        if (randomItemPick < 60) {
            randomItem = 0;
        } else if (randomItemPick < 80)
        {
            randomItem = 1;
        } else if (randomItemPick < 95)
        {
            randomItem = 2;
        } else
        {
            randomItem = 3;
        }

        if (randomItem >= bonusPrefabs.Length)
        {
            randomItem = 0;
        }

        Instantiate(bonusPrefabs[randomItem], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
