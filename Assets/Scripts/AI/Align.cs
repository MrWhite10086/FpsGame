using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{

    //Ŀ������
    public GameObject targetObject;
    //Ŀ��λ��
    private Transform target;
    //��ɫλ��
    private Transform character;

    //��ɫ�ٶ�
    private Vector3 character_V = new Vector3(0, 0, 0);
    //�����ٶ�
    public float maxAcceleration = 5f;
    //����ٶ�
    public float maxSpeed = 10f;
    //����Ŀ��İ뾶
    public float targetRadius = 5f;
    //��ʼ���ٵİ뾶
    public float slowRadius = 30f;
    //����Ŀ���ٶȵ�ʱ��
    public float timeToTarget = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        target = targetObject.GetComponent<Transform>();
        character = this.GetComponent<Transform>();
        this.GetComponent<Arrive>().ar += GetStreeing;
    }
    public float GetStreeing(float character_Vr)
    {
        float result = 0;
        //��ȡĿ�굽������
        Vector3 direct = target.position - character.position;
        float rotation = Vector3.Angle(new Vector3(0, 0, 1), direct);
        //����Ѿ��ﵽ ��ת��
        if (rotation < targetRadius)
        {
            return result;
        }
        float targetRotation = 0;
        //����ڼ��ٰ뾶�� ���ٵ�����ٶ�
        if (rotation>slowRadius)
        {
            targetRotation = maxSpeed;
        }
        //�����ձ��������ٶ� 
        else
        {
            targetRotation = maxSpeed * rotation / slowRadius;
        }
        //Ŀ���ٶȽ�����ٶȺͷ���

        //���ٳ��ԴﵽĿ���ٶ�
        result =  targetRotation - character_Vr*rotation;
        result/= timeToTarget;

        //������ٶ��Ƿ����
        if (result > maxAcceleration)
        {
            result = maxAcceleration;
        }
        result /= rotation;
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
