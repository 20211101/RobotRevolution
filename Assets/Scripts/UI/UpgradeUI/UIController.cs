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
        Time.timeScale = 1;
        upgradeInfoViewer.gameObject.SetActive(false);
    }
    public void OpenUpgradeUI()
    {
        Time.timeScale = 0;
        upgradeInfoViewer.PrintInfo();
        upgradeInfoViewer.gameObject.SetActive(true);
    }
}
