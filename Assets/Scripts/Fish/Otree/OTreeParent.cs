using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTreeParent :OTreeNode
{
    public OTreeParent() : base(){ }
    public OTreeParent(Vector3 center, float halfWidth):base(center,halfWidth)
    {
        childs = new OTreeNode[8];
        float width = HalfWidth / 2;
      //  childs[0] = new OTreeLeaf()
    }
}
