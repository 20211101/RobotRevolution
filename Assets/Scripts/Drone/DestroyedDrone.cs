using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class DestroyedDrone : MonoBehaviour
{
    float energyPercent = 0.0f;
    float fullEnergy = 3.0f;

    [SerializeField]
    GameObject Drone;

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
                // UI쪽에서도 반응 해주면 좋을듯
            }

            //소환
            GameObject drone = Instantiate(Drone, transform.position, Quaternion.identity);
            //주인 등록
            playerBoby.AddDrone(drone.transform);

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
