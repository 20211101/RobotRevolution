using UnityEngine;
using System.Collections;
public class FireAOEBullt : MonoBehaviour
{
    [SerializeField]
    GameObject FireAOE;

    Vector3 startP;
    Vector3 endP;
    float moveTime = 1;
    float duration, damage, size;

    FireAOEBulltPool pool;

    public void Setup(FireAOEBulltPool pool, Vector3 start, Vector3 end, float duration, float damage, float size)
    {
        this.pool = pool;
        Debug.Log($"S:{start} E:{end}");
        startP = start;
        endP= end;

        this.duration = duration;
        this.damage = damage;
        this.size = size;

        StartCoroutine(nameof(Move));
    }

    IEnumerator Move()
    {
        float percent = 0;
        while(percent < 1)
        {
            percent += Time.deltaTime / moveTime;


            Vector2 xz = Vector2.Lerp(new Vector2(startP.x, startP.z), new Vector2(endP.x, endP.z), percent);
            float angle = Mathf.Lerp(180, 270, percent > 0.1f ? percent : 0);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Abs(startP.y - endP.y) + startP.y;
            Vector3 where = new Vector3(xz.x, y, xz.y);

            transform.position = where;

            yield return null;
        }
        GameObject g =Instantiate(FireAOE, endP, Quaternion.identity);
        g.GetComponent<FireAOE>().Setup(duration, damage, size);
        pool.BulletPool.DeactivatePoolItem(gameObject);
    }
}
