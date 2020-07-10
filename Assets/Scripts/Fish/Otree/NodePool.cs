using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePool<T> where T:OTreeNode
{
    private ObjectPool<T> pool;
    public delegate T ConstructDelegate(float halfwidth,Vector3 center);
   // public delegate void InitDelegate(T item);
    private event ConstructDelegate myConstruct;
    //private event InitDelegate myInit;
    public int maxCount { get { return pool.maxCount}; private set; }
    public int ActiveCount { get; private set; }

    private List<T> InactiveList = new List<T>();
    public NodePool(ConstructDelegate constructEvent, int maxCount = 100)
    {
        this.maxCount = maxCount;
        this.myConstruct = constructEvent;
        ActiveCount = 0;
    }
    public T Get(float halfWidth,Vector3 center)
    {
        T item=pool.Spawn();
        item.Set(halfWidth, center);
        return item;
    }

    public void Despawn(T target)
    {
        pool.Despawn(target);
    }
}