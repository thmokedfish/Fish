using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTreeManager
{
    public  const  int MAX_COUNT = 32;
    public float MaxWidth;
    public float Center;
    private OTreeNode root;
    private ObjectPool<OTreeParent> parentPool;
    private ObjectPool<OTreeLeaf> leafPool;
    public OTreeManager()
    {
        parentPool = new ObjectPool<OTreeParent>(BuildParent, InitNode);
        InitTree();
    }
    private void InitTree()
    {
        OTreeLeaf leaf = leafPool.Spawn();
        leaf.Set()
        root = leaf;
        CheckFull(leaf);
    }
    private OTreeParent BuildParent()
    {
        return new OTreeParent();
    }

    private void InitNode(OTreeNode node)
    {

    }

    private OTreeParent Split(OTreeLeaf node)
    {
        float width = node.HalfWidth;
        Vector3 point = node.Center;
        OTreeParent cur = parentPool.Spawn();
        List<FishMove> fishes = node.fishes;
        SetChild(cur);
        
        leafPool.Despawn(node);
    }

    private void SetChild(OTreeParent parent)//交给manager从池中取子结点初始化
    {

    }
    public void InsertFish(FishMove fish)
    {

    }
    public void Check(FishMove fish)//判断是否需要转换结点，若是 进行转换
    {

    }
    private void CheckFull(OTreeLeaf leaf)
    {
        if(leaf.Count<MAX_COUNT)//不需要分裂
        {
            return;
        }
        OTreeParent parent=Split(leaf);
        if (ReferenceEquals(leaf, root))
        {
            root = parent;
        }
    }
}
