using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {


    public float forwardForce;
    public float upForce;

    public float divisionDeviationX;
    public float divisionDeviationY;

    public GameObject boxPrefab;

    public GameObject camera;

    Vector3 startingPositionOfThrow;
	// Use this for initialization
	void Start () {
        startingPositionOfThrow = new Vector3(camera.transform.position.x, camera.transform.position.y - 2f, camera.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;

            //Debug.Log(mousePos.ToString());
            ThrowBox(mousePos.x / divisionDeviationX, mousePos.y / divisionDeviationY);
        }
	}

    void ThrowBox(float deviationOnX, float deviationOnY)
    {
        startingPositionOfThrow = new Vector3(camera.transform.position.x, camera.transform.position.y - 2f, camera.transform.position.z);
        GameObject oneBox = Instantiate(boxPrefab, startingPositionOfThrow, camera.transform.rotation);
        oneBox.GetComponent<Rigidbody>().AddForce(new Vector3(deviationOnX, upForce + deviationOnY, forwardForce), ForceMode.Impulse);

        float randomXtorque = Random.Range(-8f, 8f);
        float randomYTorque = Random.Range(-8f, 8f);
        float randomZTorque = Random.Range(-8f, 8f);

        oneBox.GetComponent<Rigidbody>().AddTorque(new Vector3(randomXtorque, randomYTorque, randomZTorque), ForceMode.Impulse);
    }
}
