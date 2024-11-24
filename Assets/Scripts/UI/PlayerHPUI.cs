using UnityEngine;
using UnityEngine.UI;
public class PlayerHPUI : MonoBehaviour
{
    [SerializeField]
    PlayerBoby target;
    [SerializeField]
    Image fill;
    private void Update()
    {
        fill.fillAmount = target.Hp / target.MAXHP;
    }
}
