using System;
using System.IO;
using UnityEngine;

public class JointBase : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    public string path;

    public Vector3[] pose_joint = new Vector3[21];

    public GameObject wrist;         // 手腕

    public GameObject Thumba;        // 大拇指
    public GameObject Thumbb;      
    public GameObject Thumbc;       
    public GameObject Thumbd;

    public GameObject Indexa;        // 食指
    public GameObject Indexb;
    public GameObject Indexc;
    public GameObject Indexd;

    public GameObject Middlea;        // 中指
    public GameObject Middleb;
    public GameObject Middlec;
    public GameObject Middled;

    public GameObject Ringa;        //无名指 
    public GameObject Ringb;
    public GameObject Ringc;
    public GameObject Ringd;

    public GameObject Pinkya;        // 小拇指
    public GameObject Pinkyb;
    public GameObject Pinkyc;
    public GameObject Pinkyd;
    protected virtual float speed { get { return 5f; } }

    protected float lerp = 0f;
    protected Vector3[] skeleton;
    private int idx = 0, max = 100;

    protected void InitData()
    {

    }


    public void Reinit()
    {
        idx = 0;
        lerp = 0;
        InitData();
    }


    protected virtual void Update()
    {
        lerp += speed * Time.deltaTime;
        LerpUpdate(lerp);
        if (lerp >= 1)
        {
            if (++idx >= max) idx = 0;
            Array.Copy(skeleton, idx * 21, pose_joint, 0, 21);
        }
    }


    protected virtual void LerpUpdate(float lerp)
    {

    }

}
