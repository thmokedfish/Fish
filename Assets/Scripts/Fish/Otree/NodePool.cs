using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePool<T> where T:OTreeNode
{
    private ObjectPool<T> pool;
   // public delegate T ConstructDelegate();
   private event ObjectPool<T>.ConstructDelegate myConstruct;
    public int maxCount { get { return pool.maxCount; } }
    public int ActiveCount { get { return pool.ActiveCount; } }

    public NodePool(ObjectPool<T>.ConstructDelegate ctor,int maxCount = 100)
    {
        pool = new ObjectPool<T>(ctor, null, maxCount);
    }
    public T Produce(float halfWidth,Vector3 center)
    {
        T item=pool.Spawn();
        item.Set(halfWidth, center);
        return item;
    }

    public void Despawn(T target)
    {
        target.childs = null;
        pool.Despawn(target);
    }
}