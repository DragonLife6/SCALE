using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HandProjectileBase : MonoBehaviour
{
    protected float damage = 0f;
    protected float size = 0f;

    protected float critChance = 0f;
    protected float critPower = 1f;

    public void SetParameters(float dmg, float sz, float chance, float power)
    {
        damage = dmg;
        size = sz;

        critChance = chance;
        critPower = power;

        transform.localScale *= size;
    }
}
