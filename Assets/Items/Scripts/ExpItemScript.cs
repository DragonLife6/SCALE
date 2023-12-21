using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItemScript : MonoBehaviour
{
    [SerializeField] int expGained;
    public float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExpCollector"))
        {
            ExpirienceCollectorScriptable expScript = other.GetComponent<ExpirienceCollectorScriptable>();
            if (expScript != null)
            {
                expScript.AddExpirience(expGained);
                Destroy(gameObject);
            }
        }
    }
}
