using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void Cure();
public class NpcOne : MonoBehaviour
{

    //加入玩家为目标
    public GameObject character;
    //获取玩家状态
    public CharacterState cState;
    //寻路委托
    Arrive arr;
    //治疗行为
    public static Cure cure;
    //治疗
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
    //检测到玩家倒地，转化为前去救援状态。
    void GoToCure()
    {
        this.GetComponent<Align>().enabled=true;
        this.GetComponent<Arrive>().enabled=true;
    }
    //检测到玩家被救起来，转化默认状态
    void CharacterIsAlive()
    {
        Debug.Log("停止救助");
        curing = false;
        this.GetComponent<Align>().enabled = false;
        this.GetComponent<Arrive>().enabled = false;
    }
    //当到达目的地后，开始救援
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
