using UnityEngine;

public class UpgradeInfoViewer : MonoBehaviour
{
    [SerializeField]
    PlayerBoby playerBase;
    [SerializeField]
    PlayerUpgradeInfoViewer playerUI;
    [SerializeField]
    DroneUpgradeInfoHub droneUI;
    public void PrintInfo()
    {
        playerUI.PrintUgradeInfo(playerBase);
        droneUI.PrintUgradeInfo(playerBase.drons, playerBase.MAX_CHILD_CNT);
    }
}
