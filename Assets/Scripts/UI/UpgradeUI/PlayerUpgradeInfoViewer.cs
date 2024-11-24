using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUpgradeInfoViewer : MonoBehaviour
{
    Color enableTxtColor = Color.black;
    Color disableTxtColor = Color.red;

    [SerializeField]
    Button SpeedUpgradeBtn;
    [SerializeField]
    TextMeshProUGUI SpeedUpgradeTxt;
    [SerializeField]
    Button ExpUpgradeBtn;
    [SerializeField]
    TextMeshProUGUI ExpUpgradeTxt;
    [SerializeField]
    Button DorneCntUpgradeBtn;
    [SerializeField]
    TextMeshProUGUI DorneCntUpgradeTxt;
    [SerializeField]
    Button HealthUpgradeBtn;
    [SerializeField]
    TextMeshProUGUI HealthUpgradeTxt;

    public void PrintUgradeInfo(PlayerBoby player)
    {
        SpeedUpgradeTxt.text = $"�̵��ӵ� ���� : {player.SpeedUp.CurUpgrade} / {player.SpeedUp.MaxUpgrade}";
        if(player.SpeedUp.CanUpgrade)
        {
            SpeedUpgradeBtn.interactable = true;
            SpeedUpgradeTxt.color = enableTxtColor;
        }
        else
        {
            SpeedUpgradeBtn.interactable = false;
            SpeedUpgradeTxt.color = disableTxtColor;
        }

        ExpUpgradeTxt.text = $"����ġ ȹ�淮 ���� : {player.EXPUp.CurUpgrade} / {player.EXPUp.MaxUpgrade}";
        if(player.EXPUp.CanUpgrade)
        {
            ExpUpgradeBtn.interactable = true;
            ExpUpgradeTxt.color = enableTxtColor;
        }
        else
        {
            ExpUpgradeBtn.interactable = false;
            ExpUpgradeTxt.color = disableTxtColor;
        }

        DorneCntUpgradeTxt.text = $"�ִ� ���� ��� �� ���� : {player.DroneCntUp.CurUpgrade} / {player.DroneCntUp.MaxUpgrade}";
        if(player.DroneCntUp.CanUpgrade)
        {
            DorneCntUpgradeBtn.interactable = true;
            DorneCntUpgradeTxt.color = enableTxtColor;
        }
        else
        {
            DorneCntUpgradeBtn.interactable = false;
            DorneCntUpgradeTxt.color = disableTxtColor;
        }

        HealthUpgradeTxt.text = $"ü�·� ���� : {player.HealthUp.CurUpgrade} / {player.HealthUp.MaxUpgrade}";
        if(player.HealthUp.CanUpgrade)
        {
            HealthUpgradeBtn.interactable = true;
            HealthUpgradeTxt.color = enableTxtColor;
        }
        else
        {
            HealthUpgradeBtn.interactable = false;
            HealthUpgradeTxt.color = disableTxtColor;
        }    
    }
}
