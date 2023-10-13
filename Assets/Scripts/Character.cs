using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("Скорость Передвижение")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Скорость Поворота")]
    //[SerializeField] private float rotationSpeed = 500f;

    [Header("Гравитация")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravityScale = 2f;

    [Header("Ссылки на объекты")]
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
            moveDirection.Normalize();//Нормализуем передвижение по диагонали
        }

        if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);//куда идем туда и смотрим
        }

        if (controller.isGrounded)//стоит ли player на земле
        {
            gravity = -0.1f;
            if (Input.GetButtonDown("Jump"))//прыжок
            {
                gravity = jumpHeight;
            }
        }
        else
        {
            gravity += gravityScale * Physics.gravity.y * Time.deltaTime;//гравитания
        }


        moveDirection.y = gravity;//гравитания

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);//Передвижение в кадре, в каком-то направлении
    }
}
