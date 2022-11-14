using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{

    //转向速度
    private float vR = 0f;//0~1 插值表示
    //检测对象
    public GameObject player;
    private Transform playerTransform;
    //AI的位置
    private Transform tran;
    public GameObject gun;
    //检测距离
    public float searchDistance=10;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        tran = this.GetComponent<Transform>();
    }

    //转向目标
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
        //如果在距离范围内则尝试转向目标并开枪，否则维持当前状态。
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
