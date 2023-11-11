using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlashScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] materialsToChange;
    [SerializeField] float duration;
    [SerializeField] Material flashMaterial;
    [SerializeField] Material spriteDefault;

    public void HitFlash()
    {
        foreach (var item in materialsToChange)
        {
            item.material = flashMaterial;
        }

        StartCoroutine(ResetColorAfterDelay());
    }

    private IEnumerator ResetColorAfterDelay()
    {
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < materialsToChange.Length; i++)
        {
            materialsToChange[i].material = spriteDefault;
        }
    }
}
