using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrive : MonoBehaviour
{
    //����ί��
    public delegate float Ar(float vr);
    public delegate void IAmReady();

    //�½�ί��
    public Ar ar;
    public IAmReady iAmReady;

    //Ŀ������
    public GameObject targetObject;
    //Ŀ��λ��
    private Transform target;
    //��ɫλ��
    private Transform character;

    //��ɫ�ٶ�
    private Vector3 character_V = new Vector3(0,0,0);
    //��ɫ���ٶ�
    private float character_Vr = 0f;//0~1 ��ֵ��ʾ

    //�����ٶ�
    public float maxAcceleration=5f;
    //����ٶ�
    public float maxSpeed=10f;
    //�����ٶ�
    public float maxVr = 30f;
    //����Ŀ��İ뾶
    public float targetRadius=5f;
    //��ʼ���ٵİ뾶
    public float slowRadius=50f;
    //����Ŀ���ٶȵ�ʱ��
    public float timeToTarget = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        target = targetObject.GetComponent<Transform>();
        character = this.GetComponent<Transform>();
        iAmReady += empty;
    }
    void empty()
    {

    }
    public Vector3 getStreeing()
    {
        Vector3 result = new Vector3();
        //��ȡ����
        Vector3 direction = target.position - character.position;
        float distance = direction.magnitude;
        //����Ѿ��ﵽ ��ת��
        if (distance < targetRadius)
        {
            iAmReady();
            return result;
        }
        float targetSpeed;
        //����ڼ��ٰ뾶�� ���ٵ�����ٶ�
        if(distance>slowRadius)
        {
            targetSpeed = maxSpeed;
        }
        //�����ձ��������ٶ�
        else
        {
            targetSpeed = maxSpeed * distance / slowRadius;
        }
        //Ŀ���ٶȽ�����ٶȺͷ���
        Vector3 targetVelocity = direction.normalized;
        targetVelocity *= targetSpeed;

        //���ٳ��ԴﵽĿ���ٶ�
        result = targetVelocity - character_V;
        result /= timeToTarget;

        //����ٶ��Ƿ����
        if(result.magnitude>maxAcceleration)
        {
            result=result.normalized;
            result *= maxAcceleration;
        }
        return result;
    }
    // Update is called once per frame
    void Update()
    {
        character.position+= character_V * Time.deltaTime;
        Quaternion rotate = Quaternion.LookRotation(target.position - character.position,new Vector3(0,1,0));
        character.rotation = Quaternion.Slerp(character.rotation , rotate ,character_Vr);
        Vector3 resultA = getStreeing();
        if (resultA.magnitude != 0 )
        {
            character_V += resultA * Time.deltaTime;
        }
        else
        {
            character_V *= 0;
        }
        float resultB = ar(character_Vr);
        if(resultB==0)
        {
            character_Vr *= 0;
        }
        else
        {
            character_Vr += resultB*Time.deltaTime;
        }
        
    }
}
