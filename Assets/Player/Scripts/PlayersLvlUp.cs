using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersLvlUp : MonoBehaviour
{
    [SerializeField] GameObject[] activeSpells;
    List<AbilityBaseScript> activeSpellsScripts = new List<AbilityBaseScript>();

    [SerializeField] Transform abilitiesManager;

    private void Start()
    {
        foreach (var spell in activeSpells)
        {
            activeSpellsScripts.Add(Instantiate(spell, abilitiesManager.transform).GetComponent<AbilityBaseScript>());
        }

        foreach (var spell in activeSpellsScripts)
        {
            spell.Activate();
        }
    }
}
