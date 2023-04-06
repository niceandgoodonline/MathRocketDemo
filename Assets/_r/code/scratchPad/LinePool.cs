using UnityEngine;
using UnityEngine.Pool;

public class LinePool : MonoBehaviour
{
    public  GameObject linePrefab;
    public ObjectPool<GameObject> pool;
    private void OnEnable()
    {
        if (pool == null)
        {
            pool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(linePrefab);
            },
            line =>
            {
                line.gameObject.SetActive(true);
            },
            line =>
            {
                line.gameObject.SetActive(false);
            },
            line =>
            {
                Destroy(line.gameObject);
            }, false, 10, 50);
        }
    }
}
