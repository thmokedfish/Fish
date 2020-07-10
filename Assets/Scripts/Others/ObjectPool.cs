using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T>
{
    public delegate T ConstructDelegate();
    public delegate void InitDelegate(T item);
    private event ConstructDelegate myConstruct;
    private event InitDelegate myInit;
    public int maxCount { get; private set; }
    public int ActiveCount { get; private set; }

    private List<T> InactiveList = new List<T>();
    public ObjectPool(ConstructDelegate constructEvent, InitDelegate initEvent, int maxCount = 100)
    {
        this.maxCount = maxCount;
        this.myConstruct = constructEvent;
        this.myInit = initEvent;
        ActiveCount = 0;
    }
    public T Spawn()
    {
        if (ActiveCount >= maxCount)
        {
            Debug.LogError("pool is full");
            return default;
        }

        T item = default(T);
        if (InactiveList.Count > 0)
        {
            item = InactiveList[InactiveList.Count - 1];
            // item.gameObject.SetActive(true);
            myInit.Invoke(item);
            InactiveList.RemoveAt(InactiveList.Count - 1);
        }
        else
        {
            //inactiveList为空，需要实例化
            item = myConstruct.Invoke();
        }
        ActiveCount++;
        return item;

    }

    public void Despawn(T target)
    {
        InactiveList.Add(target);
        ActiveCount--;
    }
}