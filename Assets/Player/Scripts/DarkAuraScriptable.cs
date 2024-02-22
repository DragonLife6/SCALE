using System.Collections;
using UnityEngine;

public class DarkAuraScriptable : AbilityBaseScript
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] GameObject protectionPrefab;
    [SerializeField] GameObject wavePrefab;
    [SerializeField] GameObject backgroundPrefab;
    DarkAuraCircleBase circle;
    GameObject circleObject;
    SpriteRenderer background;

    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] delayOnLevel;
    [SerializeField] float[] sizeOnLevel;

    [SerializeField] Sprite[] bgSprites;

    public float newDamage;
    float newDelay;
    float newSize;

    public override void OnMaxLevel(int variant)
    {
        if (variant == 0)
        {
            newDelay /= 1.5f;
            newDamage *= 1.5f;
        }
        else if (variant == 1)
        {
            Destroy(circleObject);
            StartCoroutine(waveVariantRoutine());
        }
        else
        {
            Destroy(circleObject);

            circleObject = Instantiate(protectionPrefab, transform);
            circle = circleObject.GetComponent<DarkAuraCircleBase>();

            circle.SetParameters(newDamage * 0.5f, newDelay * 2f, newSize * 0.7f, critChanceMultiplier, critDamageMultiplier);
        }
    }

    IEnumerator waveVariantRoutine()
    {
        while (true)
        {
            circle = Instantiate(wavePrefab, transform).GetComponent<DarkAuraCircleBase>();
            circle.SetParameters(newDamage, newDelay, newSize, critChanceMultiplier, critDamageMultiplier);

            yield return new WaitForSeconds(newDelay * 15f);
        }
    }

    public override void Activate()
    {
        background = Instantiate(backgroundPrefab, transform).GetComponent<SpriteRenderer>();
        circleObject = Instantiate(circlePrefab, transform);
        circle = circleObject.GetComponent<DarkAuraCircleBase>();
        
        currentLevel = 1;
        UpdateAbility(currentLevel);
    }

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newDelay = delayOnLevel[lvl - 1];
        newSize = sizeOnLevel[lvl - 1] * sizeMultiplier;
        
        circle.SetParameters(newDamage, newDelay, newSize, critChanceMultiplier, critDamageMultiplier);
        
        if(bgSprites.Length >= lvl)
        {
            background.sprite = bgSprites[lvl - 1];
        }
    }
}
