using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTreeNode
{
    public int FishCount;
    public float HalfWidth;
    public Vector3 Center;
    private OTreeParent parent;
    //保存父节点的引用
    //用来调整鱼位置时 向上为每个经过的父节点--count;然后向下寻找时顺便经过每个++count
    //以及处理鱼遍历时要父节点的其他子结点中的叶子结点
    
    public OTreeParent Parent 

    { 
        get { return parent; }
        private set { this.parent = value; }
    }
    protected OTreeNode[] childs;//叶子节点为null
   /*下层0-3 上层4-7 
    * 
    *       ↑2 3
    *  Z递增↑0 1
    *         →→
    *        X递增
    *       
    */
    public OTreeNode()
    {
        Center = Vector3.zero;
        HalfWidth = 0;
    }
    public OTreeNode(Vector3 center,float halfWidth)//初始化一个无子结点的结点
    {
        Center = center;
        HalfWidth = halfWidth;
    }
    
    public bool IsLeaf { get { return childs == null; } }
    public void Set(float halfWidth,Vector3 center,OTreeParent parent)
    {
        this.HalfWidth = halfWidth;
        this.Center = center;
        this.parent = parent;
    }
    public virtual void Reset()
    {
        parent = null;
    }
}
