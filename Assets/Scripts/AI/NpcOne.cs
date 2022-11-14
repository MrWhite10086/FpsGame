using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void Cure();
public class NpcOne : MonoBehaviour
{

    //�������ΪĿ��
    public GameObject character;
    //��ȡ���״̬
    public CharacterState cState;
    //Ѱ·ί��
    Arrive arr;
    //������Ϊ
    public static Cure cure;
    //����
    bool curing = false;
    // Start is called before the first frame update
    void Start()
    {
        cState = character.GetComponent<CharacterState>();
        cState.IsDying += GoToCure;
        arr = this.GetComponent<Arrive>();
        arr.iAmReady+=IsCuring;
        CharacterState.iAmAlive += CharacterIsAlive;
    }
    //��⵽��ҵ��أ�ת��Ϊǰȥ��Ԯ״̬��
    void GoToCure()
    {
        this.GetComponent<Align>().enabled=true;
        this.GetComponent<Arrive>().enabled=true;
    }
    //��⵽��ұ���������ת��Ĭ��״̬
    void CharacterIsAlive()
    {
        Debug.Log("ֹͣ����");
        curing = false;
        this.GetComponent<Align>().enabled = false;
        this.GetComponent<Arrive>().enabled = false;
    }
    //������Ŀ�ĵغ󣬿�ʼ��Ԯ
    void IsCuring()
    {
        if(!curing)
        {
            curing = true;
            cure();
        }

    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
