using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class DestroyedDrone : MonoBehaviour
{
    public static List<DestroyedDrone> lists = new List<DestroyedDrone>();

    float energyPercent = 0.0f;
    float fullEnergy = 3.0f;
    float damage = 20;
    [SerializeField]
    GameObject Drone;
    [SerializeField]
    GameObject AOE;
    [SerializeField]
    GameObject guideText;

    [SerializeField]
    Image percentUI;
    Color good;
    private void Awake()
    {
        lists.Add(this);
        good = percentUI.color;
    }

    public void UIoff()
    {
        if(guideText != null)
        guideText.SetActive(false);
    }

    public void GetEnergy(PlayerBoby playerBoby)
    {
        energyPercent += Time.deltaTime;
        percentUI.fillAmount = energyPercent / fullEnergy;
        if(energyPercent >= fullEnergy)
        {

            if(Drone != null)
            {
                if(playerBoby.CanAddDrone == false)
                {
                    energyPercent = 0f;
                    StartCoroutine("CantAddAnim");
                    return;
                    // UI쪽에서도 반응 해주면 좋을듯
                }

                foreach(DestroyedDrone d in lists)
                {
                    d.UIoff();
                }

                //소환
                GameObject drone = Instantiate(Drone, transform.position, Quaternion.identity);
                //주인 등록
                playerBoby.AddDrone(drone.transform);
            }
            else if(AOE != null)
            {
                Vector3 spawpPos = transform.position;
                spawpPos.y = 0.5f;
                GameObject g = Instantiate(AOE, spawpPos, Quaternion.identity);
                g.GetComponent<FireAOE>().Setup(10, damage, 5);
            }
            Destroy(gameObject);
        }
    }


    IEnumerator CantAddAnim ()
    {
        percentUI.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        percentUI.color = good;
        yield return new WaitForSeconds(0.2f);
        percentUI.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        percentUI.color = good;
    }
}
