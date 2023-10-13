using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClasswork : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speed = 10f;

    public CharacterController controller;

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float rotation = Input.GetAxis("Mouse X");


        Vector3 movement = new Vector3(horizontal * speed, gravity, vertical * speed);

        controller.Move(transform.TransformDirection(movement) * Time.deltaTime);
        controller.transform.Rotate(Vector3.up, rotation);
    }
}
