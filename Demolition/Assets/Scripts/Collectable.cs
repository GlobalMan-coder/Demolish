using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    Manager manager;

    public bool hasBeenCollected;

    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();

        target = FindObjectOfType<Camera>().transform.GetChild(0);
        hasBeenCollected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenCollected)
        {
            if (Vector3.Distance(target.position, transform.position) < 2)
            {
                manager.UpdateMoney(1);
                Destroy(gameObject);
            }
            else
            {
                Vector3 velocity = target.position - transform.position;
                velocity.Normalize();
                //velocity *= 1.25f;
                transform.position += velocity;
            }
        }
    }

    public void HasBeenCollected()
    {
        if (!hasBeenCollected)
        {
            hasBeenCollected = true;
            //GetComponent<AudioSource>().Play();
        }
    }
}
