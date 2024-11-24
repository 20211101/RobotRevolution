using UnityEngine;
using System.Collections.Generic;
public class MemoryPool : MonoBehaviour
{

    private int increaseCount = 5;

    private GameObject poolObject;
    private Stack<GameObject> poolItemList;


    private Vector3 tempPosition = new Vector3(float.MaxValue, float.MinValue, float.MaxValue);

    public MemoryPool(GameObject poolObject)
    {
        this.poolObject = poolObject;

        poolItemList = new Stack<GameObject>();

        InstantiateObjects();
    }

    public void InstantiateObjects()
    {
        for (int i = 0; i < increaseCount; ++i)
        {
            GameObject poolItem;

            poolItem = GameObject.Instantiate(poolObject);
            poolItem.transform.position = tempPosition;
            poolItem.SetActive(false);

            poolItemList.Push(poolItem);
        }
    }

    public void DestroyObjects()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            GameObject.Destroy(poolItemList.Pop().gameObject);
        }

        poolItemList.Clear();
    }

    public GameObject ActivatePoolItem(Vector3 position)
    {
        if (poolItemList == null) return null;

        if (poolItemList.Count == 0)
        {
            InstantiateObjects();
        }
        GameObject item = poolItemList.Pop();
        {

            item.gameObject.transform.position = position;
            item.SetActive(true);
            return item;
            
        }
    }

    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (removeObject.activeSelf == true)
            removeObject.SetActive(false);
        poolItemList.Push(removeObject);
    }

}
