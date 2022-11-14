using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{

    //移动速度 后续速度均用百分数修改
    private static readonly float maxMovingSpeed = 8f;
    public float movingSpeed = 8f;

    //跳跃速度
    private float jumpSpeed = 5f;

    //转向速度(鼠标灵敏度)(初始速度)后续速度的更改用百分数进行增减。
    private static readonly float maxSteeringSpeed = 300f;

    //坐标位置
    private Transform thisTransform;

    //相机
    public GameObject thisCamera;
    private CameraController an;

    //角色控制器
    private CharacterController controller;

    //地面检测
    public bool isGround;

    //地面检测对象
    public Transform groundCheck;

    //地面检测半径
    public float checkRadius;

    //地面层
    public LayerMask groundLayer;

    //向上速度
    private Vector3 velocity;

    //重力
    private float gravity = 9.8f;

    //判定体积

    //感染状态（无感染0，前期1，中期2，死亡3）——————————————————————
    private int infectionStatus = 0;

    //感染时长
    private float infectionTime = 0;

    //感染条长度
    private float infectionLength = 0;

    //武器
    public GameObject Gun;


    //行为————————————————————————————————————————————
    //移动——————————————————————
    private void Charactermove()
    {
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (isGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var dir = transform.forward * vertical + transform.right * horizontal;
        var move = dir * movingSpeed * Time.deltaTime;

        controller.Move(move);
        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = jumpSpeed;
        }
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }



    //速度变化——————————————————————
    //转向 委托给相机。
    public void SetSteeringSpeed(float s)
    {
        an.mouseSensitivity = maxSteeringSpeed*s;
    }
    //移动
    public void SetMovingSpeed(float s)
    {
        movingSpeed = maxMovingSpeed * s;
    }

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.GetComponent<Transform>();
        controller = transform.GetComponent<CharacterController>();
        an = thisCamera.GetComponent<CameraController>();

    }

    // Update is called once per frame
    void Update()
    {
        //移动
        Charactermove();

    }
}
