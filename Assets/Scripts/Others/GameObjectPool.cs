using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameObjectPool<T>where T:MonoBehaviour
{
    private T obj;  //生成的物体
    public int maxCount { get; }
    public int ActiveCount { get; private set; }

    private List<T> InactiveList = new List<T>();
    public GameObjectPool(T obj,int maxCount=100)
    {
        this.obj = obj;
        this.maxCount = maxCount;
        ActiveCount = 0;
    }
    public T Spawn()
    {
        if (ActiveCount >= maxCount)
        {
            Debug.LogError("pool is full");
            return default;
        }

        T item = null;
        if (InactiveList.Count > 0)
        {
            item = InactiveList[InactiveList.Count - 1];
            item.gameObject.SetActive(true);
            InactiveList.RemoveAt(InactiveList.Count - 1);
            ActiveCount++;
            return item;
        }
        //inactiveList为空，需要实例化
        item = GameObject.Instantiate(this.obj);
        ActiveCount++;
        return item;

    }

    public void Despawn(T target)
    {
        target.gameObject.SetActive(false);
        InactiveList.Add(target);
        ActiveCount--;
    }
}
