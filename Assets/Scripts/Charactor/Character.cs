using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{

    //�ƶ��ٶ� �����ٶȾ��ðٷ����޸�
    private static readonly float maxMovingSpeed = 8f;
    public float movingSpeed = 8f;

    //��Ծ�ٶ�
    private float jumpSpeed = 5f;

    //ת���ٶ�(���������)(��ʼ�ٶ�)�����ٶȵĸ����ðٷ�������������
    private static readonly float maxSteeringSpeed = 300f;

    //����λ��
    private Transform thisTransform;

    //���
    public GameObject thisCamera;
    private CameraController an;

    //��ɫ������
    private CharacterController controller;

    //������
    public bool isGround;

    //���������
    public Transform groundCheck;

    //������뾶
    public float checkRadius;

    //�����
    public LayerMask groundLayer;

    //�����ٶ�
    private Vector3 velocity;

    //����
    private float gravity = 9.8f;

    //�ж����

    //��Ⱦ״̬���޸�Ⱦ0��ǰ��1������2������3����������������������������������������������
    private int infectionStatus = 0;

    //��Ⱦʱ��
    private float infectionTime = 0;

    //��Ⱦ������
    private float infectionLength = 0;

    //����
    public GameObject Gun;


    //��Ϊ����������������������������������������������������������������������������������������
    //�ƶ���������������������������������������������
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



    //�ٶȱ仯��������������������������������������������
    //ת�� ί�и������
    public void SetSteeringSpeed(float s)
    {
        an.mouseSensitivity = maxSteeringSpeed*s;
    }
    //�ƶ�
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
        //�ƶ�
        Charactermove();

    }
}
