using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class AbilitiesManagerScript : MonoBehaviour
{
    Transform target;
    PlayersLvlUp playerLvlUpScript;
    [SerializeField] GameObject player;

    [SerializeField] GameObject levelUpCanvas;

    [SerializeField] TMP_Text[] variantsTitle;
    [SerializeField] TMP_Text[] variantsDescription;
    [SerializeField] TMP_Text[] variantsLevel;
    [SerializeField] Image[] variantsImages;

    int[] skillsId = { 0, 0, 0 };

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

        Time.timeScale = 1f;
        levelUpCanvas.SetActive(false);
    }

    public void ShowLevelUpMenu(AbilityBaseScript[] spells)
    {
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
