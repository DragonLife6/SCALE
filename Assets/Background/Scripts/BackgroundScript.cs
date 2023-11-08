using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] GameObject[] platformPrefabs;
    [SerializeField] Transform player;
    int n = 0, m = 0;
    GameObject[] platforms = new GameObject[9];
    int imageSpacing = 10;

    GameObject getPlatform()
    {
        return platformPrefabs[Random.Range(0, platformPrefabs.Length)];
    }

    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            int x = (i % 3 - 1) * imageSpacing;
            int y = (i / 3 - 1) * imageSpacing;
            platforms[i] = Instantiate(getPlatform(), new Vector3(x, y, 2), Quaternion.Euler(0, 0, 0));
        }
    }

    void Update()
    {
        int playerGridX = Mathf.RoundToInt(player.position.x / imageSpacing);
        int playerGridY = Mathf.RoundToInt(player.position.y / imageSpacing);

        if (playerGridX != n || playerGridY != m)
        {
            n = playerGridX;
            m = playerGridY;

            for (int i = 0; i < 9; i++)
            {
                int x = (i % 3 - 1) * imageSpacing + n * imageSpacing;
                int y = (i / 3 - 1) * imageSpacing + m * imageSpacing;
                Destroy(platforms[i]);
                platforms[i] = Instantiate(getPlatform(), new Vector3(x, y, 2), Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
