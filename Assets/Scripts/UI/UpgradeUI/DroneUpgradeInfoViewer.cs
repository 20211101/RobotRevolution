using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DroneUpgradeInfoViewer : MonoBehaviour
{
    Color enableTxtColor = Color.black;
    Color disableTxtColor = Color.red;
    [SerializeField]
    Image lockImg;

    [SerializeField]
    Image droneImg;
    [SerializeField]
    Sprite[] droneImgs = new Sprite[4];



    [SerializeField]
    Button upgradeBtn_Damage;
    [SerializeField]
    TextMeshProUGUI upgradeTxt_Damage;
    [SerializeField]
    Button upgradeBtn_AddChild;
    [SerializeField]
    TextMeshProUGUI upgradeTxt_AddChild;
    [SerializeField]
    Button upgradeBtn_AtkRate;
    [SerializeField]
    TextMeshProUGUI upgradeTxt_AtkRate;

    public void Resett()
    {
        lockImg.gameObject.SetActive(true);
        droneImg.sprite = droneImgs[0];
        upgradeBtn_Damage.gameObject.SetActive(false);
        upgradeBtn_AddChild.gameObject.SetActive(false);
        upgradeBtn_AtkRate.gameObject.SetActive(false);
    }
    public void Unlock()
    {
        lockImg.gameObject.SetActive(false);
        droneImg.gameObject.SetActive(true);
        upgradeBtn_Damage.gameObject.SetActive(false);
        upgradeBtn_AddChild.gameObject.SetActive(false);
        upgradeBtn_AtkRate.gameObject.SetActive(false);
    }
    public void Lock()
    {
        lockImg.gameObject.SetActive(true);
        droneImg.gameObject.SetActive(false);
        upgradeBtn_Damage.  gameObject.SetActive(false);
        upgradeBtn_AddChild.gameObject.SetActive(false);
        upgradeBtn_AtkRate. gameObject.SetActive(false);
    }
    bool isFirstPrintInfo = true;
    public void PrintInfo(DroneBase droneInfo)
    {
        lockImg.gameObject.SetActive(false);
        droneImg.gameObject.SetActive(true);
        upgradeBtn_Damage.gameObject.SetActive(true);
        upgradeBtn_AddChild.gameObject.SetActive(true);
        upgradeBtn_AtkRate.gameObject.SetActive(true);

        if (droneInfo == null) Debug.Log("드론 정보 주작은 뭐야");

        switch (droneInfo.type)
        {
            case DroneT.perpendicular:
                droneImg.sprite = droneImgs[1];
                break;
            case DroneT.range:
                droneImg.sprite = droneImgs[2];
                break;
            case DroneT.throwgh:
                droneImg.sprite = droneImgs[3];
                break;
        }

        upgradeTxt_Damage.text = $"피해량 증가 : {droneInfo.DamageUpgrade} / {droneInfo.MaxDamageUpgrade}";
        if (droneInfo.CanDamageUpgrade)
        {
            upgradeBtn_Damage.interactable = true;
            upgradeTxt_Damage.color = enableTxtColor;
        }
        else
        {
            upgradeBtn_Damage.interactable = false;
            upgradeTxt_Damage.color = disableTxtColor;
        }

        upgradeTxt_AddChild.text = $"미니 드론 추가 : {droneInfo.ChildUpgrade} / {droneInfo.MaxChildUpgrade}";
        if (droneInfo.CanChildUp)
        {
            upgradeBtn_AddChild.interactable = true;
            upgradeTxt_AddChild.color = enableTxtColor;
        }
        else
        {
            upgradeBtn_AddChild.interactable = false;
            upgradeTxt_AddChild.color = disableTxtColor;
        }

        upgradeTxt_AtkRate.text = $"공격속도 증가 : {droneInfo.AtkRateUpgrade} / {droneInfo.MaxAtkRateUpgrade}";
        if (droneInfo.CanAtkRateUpgrade)
        {
            upgradeBtn_AtkRate.interactable = true;
            upgradeTxt_AtkRate.color = enableTxtColor;
        }
        else
        {
            upgradeBtn_AtkRate.interactable = false;
            upgradeTxt_AtkRate.color = disableTxtColor;
        }



        if(isFirstPrintInfo)
        {
        upgradeBtn_Damage.onClick.AddListener(droneInfo.Upgrade_Dmg);
        upgradeBtn_AddChild.onClick.AddListener(droneInfo.Upgrade_Child);
        upgradeBtn_AtkRate.onClick.AddListener(droneInfo.Upgrade_AtkRate);
            isFirstPrintInfo = false;
        }
    }
}