using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersLvlUp : MonoBehaviour
{
    [SerializeField] GameObject[] activeSpells;
    List<AbilityBaseScript> activeSpellsScripts = new List<AbilityBaseScript>();

    private void Start()
    {
        foreach (var spell in activeSpells)
        {
            activeSpellsScripts.Add(Instantiate(spell, transform).GetComponent<AbilityBaseScript>());
        }

        foreach (var spell in activeSpellsScripts)
        {
            spell.Activate();
        }
    }
}
