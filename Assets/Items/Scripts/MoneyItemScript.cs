using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyItemScript : MonoBehaviour
{
    [SerializeField] GameObject moneyPopup;
    [SerializeField] int minValue;
    [SerializeField] int maxValue;

    void OnTriggerEnter2D(Collider2D other)
    {
        int value = Random.Range(minValue, maxValue);
        
        if (other.CompareTag("Player"))
        {
            DamagePopupScript.Create(moneyPopup, transform.position, value, false);

            int currentValue = PlayerPrefs.GetInt("PlayerBalance");
            
            PlayerPrefs.SetInt("PlayerBalance", currentValue + value);

            Destroy(gameObject);
        }
    }
}
