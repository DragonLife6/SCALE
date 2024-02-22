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
    [SerializeField] GameObject maxLevelPanel;

    [SerializeField] Color passiveSkillIconColor;
    [SerializeField] Color activeSkillIconColor;

    [SerializeField] TMP_Text maxLevelPanelTitle;
    [SerializeField] Image[] maxLevelPanelImages;
    [SerializeField] TMP_Text maxLevelPanelVariantTitle;
    [SerializeField] TMP_Text maxLevelPanelVariantDescription;

    [SerializeField] TMP_Text[] variantsTitle;
    [SerializeField] TMP_Text[] variantsDescription;
    [SerializeField] TMP_Text[] variantsLevel;
    [SerializeField] Image[] variantsImages;

    int varianButtonNum = 0;
    int maxVariantButtonNum = 0;
    bool[] variantMaxLevel = new bool[] { false, false, false };
    AbilityBaseScript[] currentAbilities;


    private void Start()
    {
        maxLevelPanel.SetActive(false);
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
        varianButtonNum = buttonNum;

        if (variantMaxLevel[buttonNum])
        {
            maxLevelPanelTitle.text = variantsTitle[buttonNum].text;
            maxLevelPanelVariantTitle.text = variantsTitle[buttonNum].text;
            maxLevelPanelVariantDescription.text = variantsDescription[buttonNum].text;
            foreach (var image in maxLevelPanelImages)
            {
                image.sprite = variantsImages[buttonNum].sprite;
            }
            
            maxLevelPanel.SetActive(true);
        } else
        {
            playerLvlUpScript.SkillLevelUp(buttonNum); 
            Time.timeScale = 0.1f;
            StartCoroutine(SmoothTimeScaleIncrease());
            levelUpCanvas.SetActive(false);
        }
    }

    public void MaxLevelVariantClicked(int buttonNum)
    {
        maxVariantButtonNum = buttonNum;

        maxLevelPanelVariantTitle.text = currentAbilities[varianButtonNum].GetVariantName(buttonNum);
        maxLevelPanelVariantDescription.text = currentAbilities[varianButtonNum].GetVariantDescription(buttonNum);
    }

    public void ConfirmMaxLevelClicked()
    {
        maxLevelPanel.SetActive(false);
        playerLvlUpScript.SkillMaxLevelUp(varianButtonNum, maxVariantButtonNum);
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
        currentAbilities = spells;
        for (int i = 0; i < 3; i++) 
        {
            if (spells[i].isPassive)
            {
                variantMaxLevel[i] = false;
                variantsImages[i].color = passiveSkillIconColor;
            } else
            {
                variantMaxLevel[i] = spells[i].GetIsMaxLevel();
                variantsImages[i].color = activeSkillIconColor;
            }
            if (variantMaxLevel[i])
            { 
                variantsLevel[i].SetText("Max");
                variantsDescription[i].SetText("");
            }
            else
            {
                variantsLevel[i].SetText((spells[i].GetCurrentLevel() + 1).ToString());
                variantsDescription[i].SetText(spells[i].GetDescriptionOnCurrentLevel());
            }
            variantsTitle[i].SetText(spells[i].GetName());
            variantsImages[i].sprite = spells[i].GetIcon();
        }
    }
}
