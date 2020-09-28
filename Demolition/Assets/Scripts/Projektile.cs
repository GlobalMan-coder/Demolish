using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projektile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "breakable")
        {
            Debug.Log("HIt");

            collision.gameObject.GetComponentInParent<ApplyDamage>().ApplyADamage(
                GetComponent<Rigidbody>().velocity.magnitude,
                collision.contacts[0].point, 2);


        }
    }
}
