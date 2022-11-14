using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gun : MonoBehaviour
{
    //���ԣ�################################################
    //�Ƿ�����
    public bool isAutomatic = true;
    //������(�����ϴ���������ʱ��
    private float rechargeTimer = 0;
    //��������(����Ϊ��λ����������һ��
    public float rechargeTime = 0.2f;
    //����ʱ��(��������Ҫ��ʱ��
    public float reloadingTime=3f;
    //������־(��ֹ����ʱ����л�����ʼ���޷���ǹ��
    private bool reloadingFlag = true;
    //�ӵ�����
    private float bulletsNumber;
    //��������
    public float cartridgeCapacity;
    //�ӵ�����ʱ��
    public float bulletLiveTime = 0.3f;
    //���
    public float range = 100f;
    //�ӵ�ģ��
    public GameObject prefab;
    private Mesh me;
    private Material mat;
    private Vector3 scale;
    //�������
    private Vector3 face;
    private Transform tran;
    //�����˺�
    public float damage;
    //����ģ���б�
    //���ԣ�################################################

    //��Ϊ��################################################
    //������
    void shoot()
    {
        rechargeTimer += Time.deltaTime;
        if(rechargeTimer>rechargeTime)
        {
            rechargeTimer = rechargeTime;
        }
        //������
        if(isAutomatic)
        {
            //������������ ���߳��� ÿ�δﵽ�����������һ�Ρ�
            if((Input.GetKey(KeyCode.Mouse0)|| Input.GetKeyDown(KeyCode.Mouse0)) && rechargeTimer == rechargeTime)
            {
                //����ӵ���������0���˳����Զ�������
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
        //��������
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)&& rechargeTimer == rechargeTime)
            {
                //����ӵ���������0���˳����Զ�������
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
    //�������
    //����
    void Reloading()
    {
        if(reloadingFlag)
        {
            Debug.Log("���ڻ���");
            bulletsNumber = cartridgeCapacity;
            rechargeTimer -= reloadingTime;
        }
        reloadingFlag = false;
    }



    //��Ϊ��################################################
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ����
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
