using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLunch : MonoBehaviour
{
    Vector3 startingPositionOfThrow;
    public float forwardForce;
    public GameObject boxPrefab;
    public float duration = 3f;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Input.mousePosition;
            startingPositionOfThrow = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Vector3 dir;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                dir = (hit.point - startingPositionOfThrow).normalized;
            }
            else
            {
                dir = (ray.direction * 1000 + camera.transform.position - startingPositionOfThrow).normalized;
            }
            ThrowBox(dir);
        }
    }
    Transform ThrowBox(Vector3 dir)
    {
        GameObject oneBox = Instantiate(boxPrefab, startingPositionOfThrow, camera.transform.rotation);
        oneBox.GetComponent<Rigidbody>().AddForce(dir * forwardForce, ForceMode.Impulse);
        float randomXtorque = Random.Range(-8f, 8f);
        float randomYTorque = Random.Range(-8f, 8f);
        float randomZTorque = Random.Range(-8f, 8f);
        oneBox.GetComponent<Rigidbody>().AddTorque(new Vector3(randomXtorque, randomYTorque, randomZTorque), ForceMode.Impulse);
        Destroy(oneBox, duration);
        return oneBox.transform;
    }
}
