using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class textMove : MonoBehaviour
{
    TextMeshProUGUI text;
    float scale = 1;
    float maxSize = 150f;
    float minSize = 120f;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        scale += Time.deltaTime * 100;
        
        text.fontSize = minSize + Mathf.PingPong(scale, 30);
    }
}
