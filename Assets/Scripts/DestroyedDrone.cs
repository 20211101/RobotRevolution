using UnityEngine;
using UnityEngine.UI;
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
            //��ȯ
            GameObject drone = Instantiate(Drone, transform.position, Quaternion.identity);
            //���� ���
            playerBoby.AddDrone(drone.transform);

            Destroy(gameObject);
        }
    }

}
