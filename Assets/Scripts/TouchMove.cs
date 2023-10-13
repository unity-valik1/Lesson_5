using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{

    [Header("�������� ������������")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("�������� ��������")]
    //[SerializeField] private float rotationSpeed = 500f;

    [Header("����������")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravityScale = 2f;

    [Header("������ �� �������")]
    [SerializeField] private CharacterController controller;

    private float gravity;

    private Camera mainCamera;

    public Joystick joystick;

    private bool isMoving = false;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;

                // ���������, ��������� �� ������� �� ����� ������� ������
                if (touchStartPos.x < Screen.width / 2f)
                {
                    isMoving = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isMoving)
            {
                touchEndPos = touch.position;
                Vector2 touchDelta = touchEndPos - touchStartPos;
                //Vector2 touchDelta = touch.position - touchStartPos;
                //Vector3 movement = new Vector3(touchDelta.x, 0, touchDelta.y) * moveSpeed * Time.deltaTime;
                //transform.Translate(movement);

                float inputH = joystick.Horizontal;
                float inputV = joystick.Vertical;

                Vector3 forward = mainCamera.transform.forward;
                forward.y = 0;
                forward.Normalize();
                Vector3 right = mainCamera.transform.right;
                right.y = 0;
                right.Normalize();

                Vector3 moveDirection = forward * inputV + right * inputH;
                if (moveDirection.magnitude > 1)
                {
                    moveDirection.Normalize();//����������� ������������ �� ���������
                }
                //if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
                //{
                //    transform.rotation = Quaternion.LookRotation(moveDirection);//���� ���� ���� � �������
                //}
                if (controller.isGrounded)//����� �� player �� �����
                {
                    gravity = -0.1f;
                    if (Input.GetButtonDown("Jump"))//������
                    {
                        gravity = jumpHeight;
                    }
                }
                else
                {
                    gravity += gravityScale * Physics.gravity.y * Time.deltaTime;//����������
                }


                moveDirection.y = gravity;//����������

                controller.Move(moveDirection * moveSpeed * Time.deltaTime);//������������ � �����, � �����-�� �����������

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isMoving = false;
            }
        }

        //float inputH = joystick.Horizontal;
        //float inputV = joystick.Vertical;

        //Vector3 forward = mainCamera.transform.forward;
        //forward.y = 0;
        //forward.Normalize();
        //Vector3 right = mainCamera.transform.right;
        //right.y = 0;
        //right.Normalize();

        //Vector3 moveDirection = forward * inputV + right * inputH;
        //if (moveDirection.magnitude > 1)
        //{
        //    moveDirection.Normalize();//����������� ������������ �� ���������
        //}

        ////if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        ////{
        ////    transform.rotation = Quaternion.LookRotation(moveDirection);//���� ���� ���� � �������
        ////}

        //if (controller.isGrounded)//����� �� player �� �����
        //{
        //    gravity = -0.1f;
        //    if (Input.GetButtonDown("Jump"))//������
        //    {
        //        gravity = jumpHeight;
        //    }
        //}
        //else
        //{
        //    gravity += gravityScale * Physics.gravity.y * Time.deltaTime;//����������
        //}


        //moveDirection.y = gravity;//����������

        //controller.Move(moveDirection * moveSpeed * Time.deltaTime);//������������ � �����, � �����-�� �����������

    }
}
