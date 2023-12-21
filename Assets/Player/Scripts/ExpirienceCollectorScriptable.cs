using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirienceCollectorScriptable : AbilityBaseScript
{
    PlayersLvlUp player;
    CircleCollider2D collider2d;
    [SerializeField] float[] colliderRadiusOnLevel;
    float currentRadius = 0.5f;

    string targetTag = "ExpItem"; // “ег ц≥льових об'Їкт≥в
    float moveSpeed = 10f; // Ўвидк≥сть руху
    bool soulCollectorPicked = false;
    float soulCollectorStartTime;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayersLvlUp>();
        collider2d = GetComponent<CircleCollider2D>();   
    }

    public override void Activate()
    {
        currentLevel = 1;
        UpdateAbility(currentLevel);
    }

    public override void UpdateAbility(int lvl)
    {
        currentRadius = colliderRadiusOnLevel[currentLevel];
        collider2d.radius = currentRadius;
    }

    public void AddExpirience(float amount)
    {
        player.AddExpirience(amount);
    }


    private void Update()
    {
        if(soulCollectorPicked)
        {
            MoveObjectsTowardsPlayer();
        }
    }

    public void StartMovingAll()
    {
        soulCollectorPicked = true;
        soulCollectorStartTime = Time.time;
    }

    void MoveObjectsTowardsPlayer()
    {
        // «находженн€ ус≥х об'Їкт≥в з вказаним тегом
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        bool soulsLeft = false;

        // –ух ус≥х об'Їкт≥в до гравц€
        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<ExpItemScript>().spawnTime <= soulCollectorStartTime)
            {
                soulsLeft = true;
                Vector3 moveDirection = (playerPosition - obj.transform.position).normalized;
                obj.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            }
        }

        if(soulsLeft == false)
        {
            soulCollectorPicked = false;
        }
    }
}
