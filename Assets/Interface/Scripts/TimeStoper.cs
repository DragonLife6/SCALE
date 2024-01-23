using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStoper : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] TimerScript mainTimer;
    [SerializeField] PlayersLvlUp abilitiesManager;

    public void StopAllObjects()
    {
        enemyManager.StopAll(true);
        mainTimer.isStopping = true;
        abilitiesManager.StopAll(true);
    }

    public void ResumeAllObjects()
    {
        enemyManager.StopAll(false);
        mainTimer.isStopping = false;
        abilitiesManager.StopAll(false);
    }
}
