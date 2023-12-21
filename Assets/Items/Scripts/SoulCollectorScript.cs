using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollectorScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ExpCollector"))
        {
            collision.gameObject.GetComponent<ExpirienceCollectorScriptable>().StartMovingAll();
            Destroy(gameObject);
        }
    }
}
