using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private float now;
    //射击间隔
    public float interval = 0.1f;
    //射击方向
    private Vector3 face;
    //存续时间
    public float liveTime;
    //射程
    public float range = 100f;
    //克隆对象
    public GameObject prefab;
    private Mesh me;
    private Material mat;
    private Vector3 scale;

    private Transform tran;
    // Start is called before the first frame update
    void Start()
    {
        now = Time.time;
        tran = this.GetComponent<Transform>();
        face = tran.forward;
        me = prefab.GetComponent<MeshFilter>().mesh;
        mat = prefab.GetComponent<MeshRenderer>().material;
        scale = prefab.GetComponent<Transform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float a = Time.time;
        if(a-now>interval)
        {
            face = tran.forward;
            now = a;
            GameObject obj = new GameObject("bullet");
            obj.transform.position = tran.position+face*range/2;
            obj.transform.transform.rotation = tran.transform.rotation;
            obj.transform.localScale = new Vector3(scale.x,scale.y,range);
            obj.AddComponent<MeshRenderer>();
            obj.AddComponent<MeshFilter>();
            obj.AddComponent<Bullet>();
            obj.AddComponent<BoxCollider>();
            obj.GetComponent<BoxCollider>().isTrigger = true;
            Bullet n=obj.GetComponent<Bullet>();
            n.setLiveTime(liveTime);
            n.setPosition(tran.position);
            obj.GetComponent<MeshRenderer>().sharedMaterial = mat;
            obj.GetComponent<MeshFilter>().mesh = me;

        }
    }
}
