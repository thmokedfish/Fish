using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTreeNode
{
    public int Count;
    public float HalfWidth;
    public Vector3 Center;
    public OTreeNode[] childs;//叶子节点为null
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
    public OTreeNode GetChild(int i)
    {
        if(childs==null)
        {
            return null;
        }
        return childs[i];
    }
    public void Set(float halfWidth,Vector3 center)
    {
        this.HalfWidth = halfWidth;
        this.Center = center;
    }
}
