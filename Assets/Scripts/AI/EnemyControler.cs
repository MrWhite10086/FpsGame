using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{

    //ת���ٶ�
    private float vR = 0f;//0~1 ��ֵ��ʾ
    //������
    public GameObject player;
    private Transform playerTransform;
    //AI��λ��
    private Transform tran;
    public GameObject gun;
    //������
    public float searchDistance=10;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        tran = this.GetComponent<Transform>();
    }

    //ת��Ŀ��
    void TurnToTarget()
    {
        Quaternion rotate = Quaternion.LookRotation(playerTransform.position - tran.position, new Vector3(0, 1, 0));
        tran.rotation = Quaternion.Slerp(tran.rotation, rotate, vR);
        float resultB = this.GetComponent<Align>().GetStreeing(vR);
        if (resultB == 0)
        {
            vR *= 0;
        }
        else
        {
            vR += resultB * Time.deltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //����ھ��뷶Χ������ת��Ŀ�겢��ǹ������ά�ֵ�ǰ״̬��
        if(((tran.position- playerTransform.position).magnitude)<searchDistance)
        {
            gun.GetComponent<Launcher>().enabled = true;
            this.GetComponent<Arrive>().enabled = true;
            TurnToTarget();
        }
        else
        {

        }
    }
}
