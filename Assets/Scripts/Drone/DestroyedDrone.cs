using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class DestroyedDrone : MonoBehaviour
{
    float energyPercent = 0.0f;
    float fullEnergy = 3.0f;
    float damage = 20;
    [SerializeField]
    GameObject Drone;
    [SerializeField]
    GameObject AOE;

    [SerializeField]
    Image percentUI;

    public void GetEnergy(PlayerBoby playerBoby)
    {
        energyPercent += Time.deltaTime;
        percentUI.fillAmount = energyPercent / fullEnergy;
        if(energyPercent >= fullEnergy)
        {
            if(playerBoby.CanAddDrone == false)
            {
                energyPercent = 0f;
                StartCoroutine("CantAddAnim");
                // UI�ʿ����� ���� ���ָ� ������
            }

            if(Drone != null)
            {
                //��ȯ
                GameObject drone = Instantiate(Drone, transform.position, Quaternion.identity);
                //���� ���
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
        percentUI.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        percentUI.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        percentUI.color = Color.white;
    }
}
