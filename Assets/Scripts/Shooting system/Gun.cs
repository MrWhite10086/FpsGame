using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gun : MonoBehaviour
{
    //属性：################################################
    //是否连发
    public bool isAutomatic = true;
    //射击间隔(距离上次射击间隔的时间
    private float rechargeTimer = 0;
    //射速限制(以秒为单位，即多少秒一发
    public float rechargeTime = 0.2f;
    //换弹时间(换弹夹需要的时间
    public float reloadingTime=3f;
    //换弹标志(防止换弹时候进行换弹而始终无法开枪，
    private bool reloadingFlag = true;
    //子弹数量
    private float bulletsNumber;
    //弹夹容量
    public float cartridgeCapacity;
    //子弹存在时间
    public float bulletLiveTime = 0.3f;
    //射程
    public float range = 100f;
    //子弹模型
    public GameObject prefab;
    private Mesh me;
    private Material mat;
    private Vector3 scale;
    //射击方向
    private Vector3 face;
    private Transform tran;
    //武器伤害
    public float damage;
    //加载模组列表
    //属性：################################################

    //行为：################################################
    //点击射击
    void shoot()
    {
        rechargeTimer += Time.deltaTime;
        if(rechargeTimer>rechargeTime)
        {
            rechargeTimer = rechargeTime;
        }
        //连发：
        if(isAutomatic)
        {
            //当按下鼠标左键 或者长按 每次达到射击间隔即射击一次。
            if((Input.GetKey(KeyCode.Mouse0)|| Input.GetKeyDown(KeyCode.Mouse0)) && rechargeTimer == rechargeTime)
            {
                //如果子弹数量等于0则退出并自动换弹。
                if(bulletsNumber==0)
                {
                    Reloading();
                    return;
                }
                reloadingFlag = true;
               bulletsNumber--;
                rechargeTimer = 0f;
                face = tran.forward;
                GameObject obj = new GameObject("bullet");
                obj.transform.position = tran.position + face * range / 2;
                obj.transform.transform.rotation = tran.transform.rotation;
                obj.transform.localScale = new Vector3(scale.x, scale.y, range);
                obj.AddComponent<MeshRenderer>();
                obj.AddComponent<MeshFilter>();
                obj.AddComponent<Bullet>();
                obj.AddComponent<BoxCollider>();
                obj.GetComponent<BoxCollider>().isTrigger = true;
                Bullet n = obj.GetComponent<Bullet>();
                n.setLiveTime(bulletLiveTime);
                n.setPosition(tran.position);
                obj.GetComponent<MeshRenderer>().sharedMaterial = mat;
                obj.GetComponent<MeshFilter>().mesh = me;
            }
        }
        //非连发：
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)&& rechargeTimer == rechargeTime)
            {
                //如果子弹数量等于0则退出并自动换弹。
                if (bulletsNumber == 0)
                {
                    Reloading();
                    return;
                }
                reloadingFlag = true;
                bulletsNumber--; 
                rechargeTimer = 0f;
                face = tran.forward;
                GameObject obj = new GameObject("bullet");
                obj.transform.position = tran.position + face * range / 2;
                obj.transform.transform.rotation = tran.transform.rotation;
                obj.transform.localScale = new Vector3(scale.x, scale.y, range);
                obj.AddComponent<MeshRenderer>();
                obj.AddComponent<MeshFilter>();
                obj.AddComponent<Bullet>();
                obj.AddComponent<BoxCollider>();
                obj.GetComponent<BoxCollider>().isTrigger = true;
                Bullet n = obj.GetComponent<Bullet>();
                n.setLiveTime(bulletLiveTime);
                n.setPosition(tran.position);
                obj.GetComponent<MeshRenderer>().sharedMaterial = mat;
                obj.GetComponent<MeshFilter>().mesh = me;
            }
        }
    }
    //点击开镜
    //换弹
    void Reloading()
    {
        if(reloadingFlag)
        {
            Debug.Log("正在换弹");
            bulletsNumber = cartridgeCapacity;
            rechargeTimer -= reloadingTime;
        }
        reloadingFlag = false;
    }



    //行为：################################################
    // Start is called before the first frame update
    void Start()
    {
        //初始化：
        tran = this.GetComponent<Transform>();
        face = tran.forward;
        me = prefab.GetComponent<MeshFilter>().mesh;
        mat = prefab.GetComponent<MeshRenderer>().material;
        scale = prefab.GetComponent<Transform>().localScale;
        ShortcutKeys.reLoading += Reloading;
        bulletsNumber = cartridgeCapacity;
    }


    // Update is called once per frame
    void Update()
    {
        shoot();
    }
}
