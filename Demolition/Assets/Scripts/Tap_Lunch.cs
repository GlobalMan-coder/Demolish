using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap_Lunch : MonoBehaviour
{
    public float forwardForce;
    public float upForce;
    public Transform canvasTransform;
    public float divisionDeviationX;
    public float divisionDeviationY;
    public float duration = 3f;
    public GameObject boxPrefab;
    public GameObject trackPrefab;
    public GameObject camera;

    Vector3 startingPositionOfThrow;
    void Start()
    {
        startingPositionOfThrow = new Vector3(camera.transform.position.x, camera.transform.position.y - 2f, camera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
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

    void ThrowBox(Vector3 dir)
    {
        GameObject oneBox = Instantiate(boxPrefab, startingPositionOfThrow, camera.transform.rotation);
        oneBox.GetComponent<Rigidbody>().AddForce(dir * forwardForce, ForceMode.Impulse);

        float randomXtorque = Random.Range(-8f, 8f);
        float randomYTorque = Random.Range(-8f, 8f);
        float randomZTorque = Random.Range(-8f, 8f);

        oneBox.GetComponent<Rigidbody>().AddTorque(new Vector3(randomXtorque, randomYTorque, randomZTorque), ForceMode.Impulse);
        GameObject track = Instantiate(trackPrefab, canvasTransform);
        track.GetComponent<LunchSprite>().bullet = oneBox.transform;
        Destroy(oneBox, duration);
        Destroy(track, duration);
    }
}
