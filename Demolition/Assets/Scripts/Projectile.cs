using UnityEngine;
public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 accelerate;
    public bool isForced;
    Rigidbody rb;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(isForced) rb.AddForce(accelerate, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //isForced = false;
        if (collision.gameObject.tag == "breakable")
        {
            Debug.Log("HIt");

            collision.gameObject.GetComponentInParent<ApplyDamage>().ApplyADamage(
                GetComponent<Rigidbody>().velocity.magnitude,
                collision.contacts[0].point, 2);

        }
    }
}