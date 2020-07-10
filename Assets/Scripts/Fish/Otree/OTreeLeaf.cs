using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTreeLeaf :OTreeNode
{
    public List<FishMove> fishes;
    public OTreeLeaf Next;
    public OTreeLeaf Prev;
    public OTreeLeaf():base()
    { }
    public OTreeLeaf(Vector3 center, float halfWidth):base(center,halfWidth)
    {
        fishes = new List<FishMove>();
    }
}
