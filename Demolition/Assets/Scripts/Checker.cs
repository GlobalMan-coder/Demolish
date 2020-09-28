using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public bool hasFallen;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFallen)
        {
            CheckIfHasFallen();
        }
    }

    void CheckIfHasFallen()
    {
        if(startPosition.y - transform.position.y > 3)
        {
            hasFallen = true;
            GetComponentInParent<OneShape>().CheckerHasFallen();
        }
    }
}
