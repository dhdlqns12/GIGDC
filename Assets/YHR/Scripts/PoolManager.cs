using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;


    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

    }


    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (item == null)
            {
                select.SetActive(false);
            }
            else if (!item.activeSelf && item != null)
            {
                if (item == null)
                    break;
                select = item;
                if (select != null)
                    select.SetActive(true);
                break;
            }

        }
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}
