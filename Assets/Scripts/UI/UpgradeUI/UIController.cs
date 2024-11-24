using UnityEngine;
using System.Collections;
public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController instance
    {
        get => _instance;
    }
    [SerializeField]
    UpgradeInfoViewer upgradeInfoViewer;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OpenUpgradeUI();
        }
    }

    public void CloseUpgradeUI()
    {
        upgradeInfoViewer.gameObject.SetActive(false);
    }
    public void OpenUpgradeUI()
    {
        upgradeInfoViewer.PrintInfo();
        upgradeInfoViewer.gameObject.SetActive(true);
    }
}
