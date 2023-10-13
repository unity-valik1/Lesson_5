using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    private bool isMoving = false;
    private Vector2 touchStartPos;

    public float rotationSpeed = 2.0f;//Скорость вращения объекта

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;

                // Проверяем, находится ли касание на левой стороне экрана
                if (touchStartPos.x > Screen.width / 2f)
                {
                    isMoving = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isMoving)
            {
                Vector2 touchDelta = touch.position - touchStartPos;

                float rotationAmount = touchDelta.x * rotationSpeed * Time.deltaTime;//Изменяем поворот объекта по оси Y на основе движения пальца
                transform.Rotate(Vector3.up, -rotationAmount);

                touchStartPos = touch.position;//Обновляем начальную позицию касания
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isMoving = false;
            }
        }
    }
}
