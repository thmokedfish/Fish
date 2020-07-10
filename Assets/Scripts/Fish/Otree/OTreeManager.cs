using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTreeManager
{
    public  const  int MAX_COUNT = 32;
    public float MaxWidth;
    public Vector3 CenterPoint;
    private OTreeNode root;
    private NodePool<OTreeParent> parentPool;//ObjectPool<OTreeParent> parentPool;
    private NodePool<OTreeLeaf> leafPool;
    public OTreeManager()
    {
        parentPool = new NodePool<OTreeParent>(BuildParent);// new ObjectPool<OTreeParent>(BuildParent, InitNode);
        leafPool = new NodePool<OTreeLeaf>(BulidLeaf);
        InitTree();
    }
    private void InitTree()
    {
        OTreeLeaf leaf = leafPool.Produce(MaxWidth, CenterPoint);
        root = leaf;
        CheckFull(leaf);
    }
    private OTreeParent BuildParent()
    {
        return new OTreeParent();
    }
    private OTreeLeaf BuildLeaf()
    {
        return new OTreeLeaf();
    }

    private void InitNode(OTreeNode node)
    {

    }

    //替换结点
    //为新父节点添加孩子
    //将原有的fish下发给孩子
    private OTreeParent Split(OTreeLeaf node)
    {
        float width = node.HalfWidth;
        Vector3 point = node.Center;
        OTreeParent cur = parentPool.Produce(width,point);
        List<FishMove> fishes = node.fishes;
        SetChild(cur);
        //下发
        leafPool.Despawn(node);
        return cur;
    }
    //从池中取子结点初始化
    private void SetChild(OTreeParent parent)
    {

    }
    public void InsertFish(FishMove fish)
    {

    }
    public void Check(FishMove fish)//判断是否需要转换结点，若是 进行转换
    {

    }

    //check 满则Split
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
