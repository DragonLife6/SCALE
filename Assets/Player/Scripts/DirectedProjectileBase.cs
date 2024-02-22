using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedProjectileBase : MonoBehaviour
{
    protected float damage = 0f;
    protected float speed = 0f;
    protected float newSize = 0f;

    protected float critChance = 0f;
    protected float critPower = 1f;

    protected Transform target;
    protected Vector3 moveDirection;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public virtual void SetParameters(float dmg, float spd, float size, float chance, float power, Transform trgt)
    {
        damage = dmg;
        speed = spd;
        target = trgt;
        transform.localScale *= size;
        newSize = size;
        critChance = chance;
        critPower = power;

        moveDirection = (target.position - transform.position).normalized;
    }

    
}
