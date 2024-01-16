using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class AbilitiesManagerScript : MonoBehaviour
{
    Transform target;
    public float resetProcent = 0.3f;
    [SerializeField] float transitionDuration = 1f;
    PlayersLvlUp playerLvlUpScript;
    [SerializeField] GameObject player;

    [SerializeField] GameObject levelUpCanvas;

    [SerializeField] TMP_Text[] variantsTitle;
    [SerializeField] TMP_Text[] variantsDescription;
    [SerializeField] TMP_Text[] variantsLevel;
    [SerializeField] Image[] variantsImages;

    private void Start()
    {
        levelUpCanvas.SetActive(false);
        target = player.GetComponent<Transform>();
        playerLvlUpScript = player.GetComponent<PlayersLvlUp>();
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }

    public void VariantClicked(int buttonNum)
    {
        playerLvlUpScript.SkillLevelUp(buttonNum);

        Time.timeScale = 0.1f;
        StartCoroutine(SmoothTimeScaleIncrease());
        levelUpCanvas.SetActive(false);
    }

    IEnumerator SmoothTimeScaleIncrease()
    {
        float elapsedTime = 0.1f;
        float startScale = Time.timeScale;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            float smoothedT = EaseInOutQuad(t);

            Time.timeScale = Mathf.Lerp(startScale, 1f, smoothedT);

            yield return null;
        }
        
        Time.timeScale = 1f;
    }

    float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
    }

    public void ResetLevelClicked()
    {
        playerLvlUpScript.PlayerResetLevel(resetProcent);

        Time.timeScale = 0.1f;
        StartCoroutine(SmoothTimeScaleIncrease());
        levelUpCanvas.SetActive(false);
    }


    public void ShowLevelUpMenu(AbilityBaseScript[] spells)
    {
        StopAllCoroutines();
        Time.timeScale = 0f;
        levelUpCanvas.SetActive(true);

        for (int i = 0; i < 3; i++) 
        {
            variantsTitle[i].SetText(spells[i].GetName());
            variantsLevel[i].SetText((spells[i].GetCurrentLevel() + 1).ToString());
            variantsDescription[i].SetText(spells[i].GetDescriptionOnCurrentLevel());
            variantsImages[i].sprite = spells[i].GetIcon();
        }
    }
}
