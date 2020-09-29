using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap_Lunch : MonoBehaviour
{
    public float forwardForce;
    public float upForce;
    public Transform targetParent;
    public float divisionDeviationX;
    public float divisionDeviationY;
    public float duration = 3f;
    public GameObject boxPrefab;
    public GameObject trackPrefab;
    public GameObject camera;

    List<Vector3> dirs;
    Vector3 startingPositionOfThrow;

    void Start()
    {
        dirs = new List<Vector3>();
    }
    // Update is called once per frame
    public void Touch()
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
        GameObject track = Instantiate(trackPrefab, targetParent);
        track.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        dirs.Add(dir);
    }
    public void ThrowBoxes()
    {
        for(int i = targetParent.childCount - 1; i >=0 ; i--)
        {
            Destroy(targetParent.GetChild(i).gameObject);
        }
        for(int i = 0; i < dirs.Count; i++)
        {
            ThrowBox(dirs[i]);
        }
        dirs.Clear();
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
