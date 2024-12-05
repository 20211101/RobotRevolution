using UnityEngine;
using System.Collections;
using TMPro;
public class TimeCalculator : MonoBehaviour
{
    private static TimeCalculator _instance;
    public static TimeCalculator instance { get => _instance; }
    public int minute => (int)time / (int)60;
    public int leftMinute => (int)(purposeTime - time) / (int)60;
    public int leftScecond => (int)(purposeTime - time) % (int)60;
    public int scecond => (int)time % (int)60;
    [SerializeField]
    TextMeshProUGUI text;
    public float time;
    bool calcTime = false;

    public GameObject GameEndUI;
    public GameObject GameEndEffect;
    public GameObject Cam;
    bool gameEnd = false;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    void Start()
    {
        calcTime = true;
    }
    public float purposeTime = 60 * 10;
    //float purposeTime = 5;
    public float leftT => purposeTime - time;
    void Update()
    {
        if (gameEnd == true) return;
        if (purposeTime - time < 0)
        {
            StartCoroutine("GameEndCo");
        }
        if (calcTime)
            time += Time.deltaTime;
        text.text = string.Format("{0:D2}:{1:D2}", leftMinute, leftScecond);
    }

    IEnumerator GameEndCo()
    {
        yield return new WaitForSeconds(1);
        GameEndEffect.SetActive(true);
        yield return new WaitForSeconds(2);
        GameEndUI.SetActive(true);
    }
}
