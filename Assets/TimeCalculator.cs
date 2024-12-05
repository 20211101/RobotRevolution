using UnityEngine;
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
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    void Start()
    {
        calcTime = true;
    }
    float purposeTime = 60 * 10;
    void Update()
    {
        if (purposeTime - time < 0) Debug.Log("게임 종료~");
        if (calcTime)
            time += Time.deltaTime;
        text.text = string.Format("{0:D2}:{1:D2}", leftMinute, leftScecond);
    }
}
