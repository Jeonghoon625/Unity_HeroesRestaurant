using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectPooling : MonoBehaviour
{
    public static MultipleObjectPooling instance;

    public List<GameObject> poolPrefabs;    // Prefabs
    public int poolingCount;            // 각각 Prefab 생성할 숫자

    private Dictionary<object, List<GameObject>> pooledObjects = new Dictionary<object, List<GameObject>>();

    //private void Start()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //    // 영웅 소환
    //    var heroList = GameManager.Instance.heroSellectManager.heroList;
    //    var heroPrefab = heroList.heroPrefab;
    //    var isSellect = heroList.isSellect;
    //    var ren = heroList.heroPrefab.Length;
    //    for (int i = 0; i < ren; i++)
    //    {
    //        if (isSellect[i] == true)
    //        {
    //            var hero = heroPrefab[i].GetComponent<Heros>();
    //            if(hero.AttackType == AttackTypes.Range)
    //            {
    //                poolPrefabs.Add(hero.shootPrefab);
    //            }
    //        }
    //    }

    //    CreateMultiplePoolObjects();
    //}
    public void Test()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CreateMultiplePoolObjects();
    }

    public void CreateMultiplePoolObjects()
    {
        for (int i = 0; i < poolPrefabs.Count; i++)
        {
            for (int j = 0; j < poolingCount; j++)
            {
                if (!pooledObjects.ContainsKey(poolPrefabs[i].name))
                {
                    List<GameObject> newList = new List<GameObject>();
                    pooledObjects.Add(poolPrefabs[i].name, newList);
                }

                GameObject newDoll = Instantiate(poolPrefabs[i], transform);
                newDoll.SetActive(false);
                pooledObjects[poolPrefabs[i].name].Add(newDoll);
            }
        }
    }

    public GameObject GetPooledObject(string _name)
    {
        if (pooledObjects.ContainsKey(_name))
        {
            for (int i = 0; i < pooledObjects[_name].Count; i++)
            {
                if (!pooledObjects[_name][i].activeSelf)
                {
                    return pooledObjects[_name][i];
                }
            }

            int beforeCreateCount = pooledObjects[_name].Count;

            return pooledObjects[_name][beforeCreateCount];
        }
        else
        {
            return null;
        }
    }
    // 출처: https://simppen-gamedev.tistory.com/6 [게임 개발 초보자들을 위한:티스토리]
}
