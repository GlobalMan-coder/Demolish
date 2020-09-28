using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;

public class ApplyDamage : MonoBehaviour
{
    RayfireRigid rigid;
    //public float damageValue = 50f;
    //public Transform damagePoint;
    //public float damageRadius = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<RayfireRigid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    public void ApplyADamage(float damageVlue, Vector3 point, float damageRadius)
    {
        rigid.ApplyDamage(damageVlue, point, damageRadius);
    }
}
