using UnityEngine;
using System.Collections.Generic;
public class DroneUpgradeInfoHub : MonoBehaviour
{
    [SerializeField]
    DroneUpgradeInfoViewer[] views = new DroneUpgradeInfoViewer[6];
    public void PrintUgradeInfo(List<DroneBase> drons, int capacity)
    {
        int i = 0;
        for (i = 0; i < capacity; i++)
            views[i].Unlock();
        for (; i < views.Length; i++)
            views[i].Lock();

        for(i = 0; i < drons.Count; i++)
        {
            views[i].PrintInfo(drons[i]);
        }
    }
}
