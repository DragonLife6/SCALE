using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragonHalfProjectile : DragonSpellProjectile
{
    public float rotationSpeed = 2f;
    public float moveSpeedAdd = 5f;
    public float deleteTime = 1f;

    public override void Movement()
    {
        moveDirection = transform.rotation * Vector3.up;
        transform.Translate(moveDirection * 5f * moveSpeedAdd * Time.deltaTime, Space.World);

        moveSpeedAdd += Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        Destroy(gameObject, deleteTime);
    }
}
