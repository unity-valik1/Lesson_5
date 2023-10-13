using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    private bool isMoving = false;
    private Vector2 touchStartPos;

    public float rotationSpeed = 2.0f;//�������� �������� �������

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;

                // ���������, ��������� �� ������� �� ����� ������� ������
                if (touchStartPos.x > Screen.width / 2f)
                {
                    isMoving = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isMoving)
            {
                Vector2 touchDelta = touch.position - touchStartPos;

                float rotationAmount = touchDelta.x * rotationSpeed * Time.deltaTime;//�������� ������� ������� �� ��� Y �� ������ �������� ������
                transform.Rotate(Vector3.up, -rotationAmount);

                touchStartPos = touch.position;//��������� ��������� ������� �������
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isMoving = false;
            }
        }
    }
}
