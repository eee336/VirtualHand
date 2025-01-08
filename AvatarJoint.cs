using UnityEngine;

public class AvatarJoint : JointBase
{

    public class AvatarTree
    {
        public Transform transf;
        public AvatarTree[] childs;
        public AvatarTree parent;
        public int idx;  // pose_joint's index

        public AvatarTree(Transform tf, int count, int idx, AvatarTree parent = null)
        {
            this.transf = tf;
            this.parent = parent;
            this.idx = idx;
            if (count > 0)
            {
                childs = new AvatarTree[count];
            }
        }

        public Vector3 GetDir()
        {
            if (parent != null)
            {
                return transf.position - parent.transf.position; //计算到父节点位置
            }
            return Vector3.up;
        }
    }

    private AvatarTree tree, Thumb, Index, Middle, Ring, Pinky;
    private AvatarTree Thumb1, Thumb2, Thumb3, Index1, Index2, Index3, Middle1, Middle2, Middle3,Ring1, Ring2, Ring3,Pinky1, Pinky2, Pinky3;

    protected override float speed { get { return 5f; } }

    void Start()
    {
        InitData();
        BuildTree();
    }

    void BuildTree()
    {
        tree = new AvatarTree(wrist.transform, 5, 0);
        Thumb = tree.childs[0] = new AvatarTree(Thumba.transform, 1, 1, tree);
        Index = tree.childs[1] = new AvatarTree(Indexa.transform, 1, 5, tree);
        Middle = tree.childs[2] = new AvatarTree(Middlea.transform, 1, 9, tree);
        Ring = tree.childs[3] = new AvatarTree(Ringa.transform, 1, 13, tree);
        Pinky = tree.childs[4] = new AvatarTree(Pinkya.transform, 1, 17, tree);


        Thumb1 = Thumb.childs[0] = new AvatarTree(Thumbb.transform, 1, 2, Thumb);
        Thumb2 = Thumb1.childs[0] = new AvatarTree(Thumbc.transform, 1, 3, Thumb1);
        Thumb3 = Thumb2.childs[0] = new AvatarTree(Thumbd.transform, 1, 4, Thumb2);

        Index1 = Index.childs[0] = new AvatarTree(Indexb.transform, 1, 6, Index);
        Index2 = Index1.childs[0] = new AvatarTree(Indexc.transform, 1, 7, Index1);
        Index3 = Index2.childs[0] = new AvatarTree(Indexd.transform, 1, 8, Index2);

        Middle1 = Middle.childs[0] = new AvatarTree(Middleb.transform, 1, 10, Middle);
        Middle2 = Middle1.childs[0] = new AvatarTree(Middlec.transform, 1, 11, Middle1);
        Middle3 = Middle2.childs[0] = new AvatarTree(Middled.transform, 1, 12, Middle2);

        Ring1 = Ring.childs[0] = new AvatarTree(Ringb.transform, 1, 14, Ring);
        Ring2 = Ring1.childs[0] = new AvatarTree(Ringc.transform, 1, 15, Ring1);
        Ring3 = Ring2.childs[0] = new AvatarTree(Ringd.transform, 1, 16, Ring2);

        Pinky1 = Pinky.childs[0] = new AvatarTree(Pinkyb.transform, 1, 18, Pinky);
        Pinky2 = Pinky1.childs[0] = new AvatarTree(Pinkyc.transform, 1, 19, Pinky1);
        Pinky3 = Pinky2.childs[0] = new AvatarTree(Pinkyd.transform, 1, 20, Pinky2);
    }


    protected override void LerpUpdate(float lerp)
    {
        UpdateBone(Thumb3, lerp);
        UpdateBone(Index3, lerp);
        UpdateBone(Middle3, lerp);
        UpdateBone(Ring3, lerp);
        UpdateBone(Pinky3, lerp);
        UpdateBone(Thumb2, lerp);
        UpdateBone(Index2, lerp);
        UpdateBone(Middle2, lerp);
        UpdateBone(Ring2, lerp);
        UpdateBone(Pinky2, lerp);
        UpdateBone(Thumb1, lerp);
        UpdateBone(Index1, lerp);
        UpdateBone(Middle1, lerp);
        UpdateBone(Ring1, lerp);
        UpdateBone(Pinky1, lerp);
        UpdateBone(Thumb, lerp);
        UpdateBone(Index, lerp);
        UpdateBone(Middle, lerp);
        UpdateBone(Ring, lerp);
        UpdateBone(Pinky, lerp);
    }


    private void UpdateTree(AvatarTree tree, float lerp)
    {
        if (tree.parent != null)
        {
            UpdateBone(tree, lerp);
        }
        if (tree.childs != null)
        {
            for (int i = 0; i < tree.childs.Length; i++)
                UpdateTree(tree.childs[i], lerp);
        }
    }

    private void UpdateBone(AvatarTree tree, float lerp)  //更新骨骼位置，子到父位置变化
    {
        var dir1 = tree.GetDir();
        var dir2 = pose_joint[tree.idx] - pose_joint[tree.parent.idx];
        dir2.y = -dir2.y;
        Quaternion rot = Quaternion.FromToRotation(dir1, dir2);
        Quaternion rot1 = tree.parent.transf.rotation;
        tree.parent.transf.rotation = Quaternion.Lerp(rot1, rot * rot1, lerp);
    }

    void Update()
    {
        float lerp = Time.deltaTime * speed; // 动作平滑速度
        LerpUpdate(lerp);
    }


}