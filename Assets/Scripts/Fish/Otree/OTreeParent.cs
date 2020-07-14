using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTreeParent :OTreeNode
{
    //Parent的childs在任何情况下不能为null 以避免isLeaf返回true
    public OTreeParent() : base(){
        childs = new OTreeNode[8];
    }
    public OTreeNode GetChild(int i)
    {
        if (childs == null)
        {
            return null;
        }
        return childs[i];
    }
}
