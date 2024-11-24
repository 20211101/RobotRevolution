using UnityEngine;
using UnityEngine.UI;
public class EXPUI : MonoBehaviour
{
    [SerializeField]
    PlayerBoby player;
    [SerializeField]
    Image fill;
    void Update()
    {
        fill.fillAmount = player.curEXP / player.MAX_EXP;
    }
}
