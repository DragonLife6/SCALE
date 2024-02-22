using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DarkAuraCircleBase : MonoBehaviour
{
    protected float damage;
    protected float delay;
    protected float size;
    protected Vector3 initialSize = Vector3.zero;

    protected float critChance = 0f;
    protected float critPower = 1f;

    public virtual void SetParameters(float newDamage, float newDelay, float newSize, float chance, float power)
    {
        damage = newDamage;
        delay = newDelay;
        size = newSize;

        critChance = chance;
        critPower = power;
    }

    public void ChangeSize(float newSize)
    {
        if (initialSize == Vector3.zero)
        {
            initialSize = transform.localScale;
        }
        transform.localScale = initialSize * newSize;
    }
}
