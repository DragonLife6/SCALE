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
        int randomItem = Random.Range(0, bonusPrefabs.Length);
        Instantiate(bonusPrefabs[randomItem], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
