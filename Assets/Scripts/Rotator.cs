using UnityEngine;
using System.Collections.Generic;
public class Rotator : MonoBehaviour
{
    private int maxChildCnt =2;
    private List<Transform> childs = new List<Transform>();
    [SerializeField]
    private float distance = 3;
    [SerializeField]
    private float rotateSpeed = 5;
    // Y 축이 커지면 우측 회전
    [SerializeField]
    private bool rotateLeft;

    public bool isFULL => maxChildCnt <= childs.Count;

    public void AddChild(Transform child)
    {
        if(isFULL)
        {
            return;
        }
        child.SetParent(transform);
        child.localScale = new Vector3(0.8f, child.localScale.y, 0.8f);

        childs.Add(child);


        Reposition();
    }

    public void UpCapacity()
    {
        maxChildCnt++;
    }

    public void SubChild(Transform child)
    {
        if(childs.Find(x => x == child) != null)
            childs.Remove(child);


        Reposition();
    }

    public void Reposition()
    {
        for (int i = 0; i < childs.Count; i++)
        {
            float angle = (360f / childs.Count) * i;
            Debug.Log($"{i} : {angle}");
            Debug.Log($"{Mathf.Cos(angle *  Mathf.Deg2Rad)} : {Mathf.Sin(angle * Mathf.Deg2Rad)}");
            // angle -> dir
            // y/x         v
            
            Vector2 pos = new Vector2(distance * Mathf.Cos(angle * Mathf.Deg2Rad), distance * Mathf.Sin(angle * Mathf.Deg2Rad));
            childs[i].position = new Vector3(pos.x, 0, pos.y) + transform.position;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime, Space.World);
    }

}
