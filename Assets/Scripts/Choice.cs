using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    public enum EChoosingIntensity : ushort
    {
        eSilver = 0,
        eGold = 1,
        eRubis = 2,
        eDiamond = 3,
        eIntensityMax = 4
    }
    public enum EChoosingAugment : ushort
    {
        eSkip = 0,
        ePlayerSpeed ,
        ePlayerHealth,
        eSpell1CD,
        eSpell1Damage,
        eSpell1LivingDuration,
        eSpell1Size,
        eSpell2CD,
        eSpell2Damage,
        eSpell2LivingDuration,
        eSpell2Size,
        eChoiceMax
    }
    public EChoosingIntensity intensity;
    public EChoosingAugment augment;
    public int position;
    [SerializeField] private string headerText;
    [SerializeField] private Image headerImage;
    [SerializeField] private TextMeshProUGUI headerTMPro;
    [SerializeField] private Sprite imageSprite;
    [SerializeField] private Image augmentImage;
    [SerializeField] private string descriptionText;
    [SerializeField] private TextMeshProUGUI descriptionTMPro;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite fireBallSprite;
    [SerializeField] private Sprite fireSlashSprite;
    private int xDistancePosition = 580;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void GenerateTextAndImage()
    {
        switch (augment)
        {
            case EChoosingAugment.eSkip:
                headerText = "Next Wave";
                descriptionText = "No augment";
                imageSprite = playerSprite;
                break;
            case EChoosingAugment.ePlayerSpeed:
                headerText = "Speed";
                descriptionText = "Want to run faster ?\nHere is what you need !";
                imageSprite = playerSprite;
                break;
            case EChoosingAugment.ePlayerHealth:
                headerText = "Health";
                descriptionText = "The more HP\nThe tankier !";
                imageSprite = playerSprite;
                break;
            case EChoosingAugment.eSpell1CD:
                headerText = "Fire Ball CD";
                descriptionText = "Fire Ball will be faster to cast";
                imageSprite = fireBallSprite;
                break;
            case EChoosingAugment.eSpell1Damage:
                headerText = "Fire Ball Damage";
                descriptionText = "";
                imageSprite = fireBallSprite;
                break;
            case EChoosingAugment.eSpell1LivingDuration:
                headerText = "Fire Ball range";
                descriptionText = "";
                imageSprite = fireBallSprite;
                break;
            case EChoosingAugment.eSpell1Size:
                headerText = "Fire Ball Size";
                descriptionText = "";
                imageSprite = fireBallSprite;
                break;
            case EChoosingAugment.eSpell2CD:
                headerText = "Fire Slash CD";
                descriptionText = "Fire Slash will be faster to cast";
                imageSprite = fireSlashSprite;
                break;
            case EChoosingAugment.eSpell2Damage:
                headerText = "Fire Slash Damage";
                descriptionText = "";
                imageSprite = fireSlashSprite;
                break;
            case EChoosingAugment.eSpell2LivingDuration:
                headerText = "Fire Slash Range";
                descriptionText = "";
                imageSprite = fireSlashSprite;
                break;
            case EChoosingAugment.eSpell2Size:
                headerText = "Fire Slash Size";
                descriptionText = "";
                imageSprite = fireSlashSprite;
                break;
            default :
                headerText = "Unknown augment";
                descriptionText = "???";
                break;
        }
    }
    public void UpdateChoiceUI()
    {
        PositionIt();
        ColorHeader();
        GenerateTextAndImage();
        headerTMPro.text = headerText;
        descriptionTMPro.text = descriptionText + "\n " + (GetPercentage()- 1) * 100 + " %";
        augmentImage.sprite = imageSprite;
    }

    private void PositionIt()
    {
        Vector2 relativePos = Vector2.zero;
        switch (position)
        {
            case 0:
                relativePos.x = -xDistancePosition;
                break;
            case 2:
                relativePos.x = xDistancePosition;
                break;
            default:
                relativePos.x = 0;
                break;
        }
        gameObject.GetComponent<RectTransform>().localPosition = relativePos;
    }

    private void ColorHeader()
    {
        Color color =Color.white;
        switch (intensity)
        {
            case EChoosingIntensity.eSilver:
                color = Color.white;
                break;
            case EChoosingIntensity.eGold:
                color = Color.yellow;
                break;
            case EChoosingIntensity.eRubis:
                color = Color.red;
                break;
            case EChoosingIntensity.eDiamond:
                color = Color.cyan;
                break;
        }
        headerImage.color = color;
    }

    private float GetPercentage()
    {
        float percentage = 1.0f;
        switch (intensity)
        {
            case (EChoosingIntensity.eSilver): // 10%
                percentage = 1.1f; break;
            case (EChoosingIntensity.eGold): // 20%
                percentage = 1.2f; break;
            case (EChoosingIntensity.eRubis): // 30%
                percentage = 1.3f; break;
            case (EChoosingIntensity.eDiamond): // 50%
                percentage = 1.5f; break;
        }
        return percentage;

    }
    public void ApplyChoice()
    {
        Debug.Log("Choice : " + headerText);
        switch (augment)
        {
            case EChoosingAugment.ePlayerSpeed:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeSpeed(GetPercentage()); break;
            case EChoosingAugment.ePlayerHealth:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeHP(GetPercentage()); break;
            case EChoosingAugment.eSpell1CD:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeCDSpell1(GetPercentage()); break;
            case EChoosingAugment.eSpell2CD:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeCDSpell2(GetPercentage()); break;
            case EChoosingAugment.eSpell1Damage:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeDmgSpell1(GetPercentage()); break;
            case EChoosingAugment.eSpell2Damage:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeDmgSpell2(GetPercentage()); break;
            case EChoosingAugment.eSpell1LivingDuration:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradelivingDurationSpell1(GetPercentage()); break;
            case EChoosingAugment.eSpell2LivingDuration:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradelivingDurationSpell2(GetPercentage()); break;
            case EChoosingAugment.eSpell1Size:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeSizeSpell1(GetPercentage()); break;
            case EChoosingAugment.eSpell2Size:
                GameObject.Find("Player").GetComponent<PlayerController>().UpgradeSizeSpell2(GetPercentage()); break;
            default:
                Debug.Log("Not attributed augment");
                break;
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().ChoiceUpgradeMade();
    }
}
