using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Посилання на гравця, за яким слідкуємо
    public float smoothSpeed = 0.125f; // Коефіцієнт згладжування
    public Vector3 offset; // Відступ від гравця
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            // Обчислюємо позицію, до якої ми хочемо змістити камеру
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = -10;

            // Застосовуємо згладжування, щоб зробити рух камери плавним
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // Задаємо позицію камери
            transform.position = smoothedPosition;
        }
    }
}
