using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
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

        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

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

        if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);//���� ���� ���� � �������
        }

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
}
