using System.Collections.Generic;
using UnityEngine;

public class OTreeManager
{
    //首先通过链表遍历叶子 检查每条鱼并将鱼放入正确的结点
    //完成后再动态调整树的结构
    //不要一边遍历鱼一遍调整 变动的结构会导致出错
    //初始化也是 先插入所有鱼 再调整树结构
    public const int MAX_COUNT = 8;
    public float MaxWidth;
    public Vector3 CenterPoint;
    private OTreeNode root;
    private OTreeLeaf leafsHead;//leaf链表头 
    private NodePool<OTreeParent> parentPool;//ObjectPool<OTreeParent> parentPool;
    private NodePool<OTreeLeaf> leafPool;
    public OTreeManager()
    {
        parentPool = new NodePool<OTreeParent>(BuildParent);// new ObjectPool<OTreeParent>(BuildParent, InitNode);
        leafPool = new NodePool<OTreeLeaf>(BuildLeaf);
        InitTree();
    }
    private void InitTree()
    {
        root = leafPool.Produce(MaxWidth, CenterPoint,null);
        CheckTreeSplit(root);
    }
    private OTreeParent BuildParent()
    {
        return new OTreeParent();
    }
    private OTreeLeaf BuildLeaf()
    {
        return new OTreeLeaf();
    }

    public void UpdateTree()
    {
        // CheckFishesPos();//通过链表遍历鱼
        CheckTreeSplit(root);
    }

    //替换结点
    //为新父节点添加孩子
    //将原有的fish下发给孩子
    private OTreeParent Split(OTreeLeaf leaf)
    {
        float width = leaf.HalfWidth;
        Vector3 point = leaf.Center;
        List<FishMove> fishes = leaf.fishes;
        //记录原叶子中的前后指针
        OTreeLeaf prev = leaf.Prev;
        OTreeLeaf next = leaf.Next;
        OTreeParent parent = leaf.Parent;
        leafPool.Despawn(leaf);

        OTreeParent cur = parentPool.Produce(width,point,parent);
        if(ReferenceEquals(leafsHead,leaf))
        {
            //转交leaf头至leaf分裂后的一个子结点
        }
        SetChild(cur,prev,next);
        //下发fishes————

        return cur;
    }
    //从池中取子结点初始化————
    //用指针将取出的叶子串起来 顺序无所谓
    private void SetChild(OTreeParent parent,OTreeLeaf prev,OTreeLeaf next)
    {
    }



    //插入鱼
    public bool FindAndInsert(FishMove fish,OTreeNode start)
    {
        Vector3 pos=fish.position;
        Vector3 center;
        OTreeNode node = start;
        if(!inRange(pos,node))
        {
            Debug.Log("out of start");
            return false;
        }
        //ok 在根节点范围内
        while(true)
        {
            ++node.FishCount; 
            if (node.IsLeaf) //找到叶子结点
            {
                Insert(fish, (OTreeLeaf)node);
                return true;
            }
            //不是叶子 检查在哪个子结点
            int index = 0;//参考OTreeNode注明的index顺序
            center = node.Center;
            index += pos.x > center.x ? 1 : 0;
            index += pos.z > center.z ? 2 : 0;
            index += pos.y > center.y ? 4 : 0;
            node = ((OTreeParent)node).GetChild(index);
        }
    }
    private void Insert(FishMove fish,OTreeLeaf leaf)
    {
        if(leaf.fishes.Find((FishMove f) => { return ReferenceEquals(f,fish); })) { return; }
        
    }
    private bool inRange(Vector3 pos,OTreeNode node)
    {
        Vector3 center = node.Center;
        float halfWidth = node.HalfWidth;
        if (pos.x <center.x + halfWidth && pos.x > center.x - halfWidth)
        {
            if(pos.y < center.y + halfWidth && pos.y > center.y - halfWidth)
            {
                if (pos.z < center.z + halfWidth && pos.z > center.z - halfWidth)
                {
                    return true;
                }
            }
        }
         return false; 
    }
    //通过leaf链表调用 
    //判断是否需要转换结点。若是，向上寻找直到确认在某个父节点范围内，然后再向下寻找
    public void CheckFishPos(OTreeNode node,FishMove fish)
    {
        if(inRange(fish.position,node))
        { return; }
        node.FishCount--;
        OTreeParent cur = node.Parent;
        while (true)
        {
            //cur为空 则上个cur为root 也就是在root的范围内没找到
            if(cur==null)
            {
                Debug.LogError("not find from root");
            }
            --cur.FishCount;//不管在不在里面都要减 因为FindAndInsert从start开始++，而如果cur是start，为了让FishCount保持不变，要先--
            if (inRange(fish.position, cur))
            {
                FindAndInsert(fish, cur);
            }
            else
            {
                cur = cur.Parent;
            }
            
        }
    }

    /*
    //check 满则Split
    private void CheckFull(OTreeLeaf leaf)
    {
        if(leaf.FishCount<MAX_COUNT)//不需要分裂
        {
            return;
        }
        OTreeParent parent=Split(leaf);
        if (ReferenceEquals(leaf, root))
        {
            root = parent;
        }
        int count = parent.childs.Length;
        for(int i=0;i<count;i++)
        {
            CheckFull((OTreeLeaf)parent.childs[i]);
        }
    }
    */

    private void CheckTreeSplit(OTreeNode start)
    {
        OTreeParent parent;
        if (start.IsLeaf)
        {
            if (start.FishCount < MAX_COUNT)//不需要分裂
            {
                return;
            }
            parent = Split((OTreeLeaf)start);
            if (ReferenceEquals(start, root))
            {
                root = parent;
            }
        }
        else
        {
            parent = start as OTreeParent;
        }
        for (int i = 0; i < MAX_COUNT; i++)
        {
            CheckTreeSplit(parent.GetChild(i));
        }
    }
}
