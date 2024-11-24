using UnityEngine;
using UnityEngine.UI;
public class EnemyHPUI : MonoBehaviour
{
    [SerializeField]
    EnemyBase target;
    [SerializeField]
    Image fill;
    private void Update()
    {
         fill.fillAmount = target.HP / target.MAXHP;
    }
}
