using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
    [SerializeField] float startSpeed = 10;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform track;
    [SerializeField] float bendRate = 5f;
    Camera mainCamera;
    Vector3 startingPos;
    Vector3 speedVector;
    Vector3 accelerate;
    float mass;
    Vector3 deltaSpeed, deltaforce;
    LineRenderer line;
    bool isMouseDown;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        mass = bulletPrefab.GetComponent<Rigidbody>().mass;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            line.enabled = true;
            track.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Lunch();
            isMouseDown = false;
            line.enabled = false;
            track.gameObject.SetActive(false);
        }
        if (isMouseDown)
        {
            speedVector = GetDir() * startSpeed;
            accelerate = new Vector3(-speedVector.x * bendRate , 0, 0);
            //deltaSpeed = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            //deltaforce = new Vector3(deltaSpeed.x * -5f, deltaSpeed.y * -5f, 0);
            //speedVector += deltaSpeed;
            //accelerate += deltaforce;
            LineDraw();
        }
    }
    void Lunch()
    {
        Rigidbody bullet = Instantiate(bulletPrefab).GetComponent<Rigidbody>();
        bullet.GetComponent<Projectile>().accelerate = accelerate;
        bullet.GetComponent<Projectile>().isForced = true;
        bullet.transform.position = startingPos;
        bullet.velocity = speedVector;
        Destroy(bullet.gameObject, 2f);
    }
    Vector3 GetDir()
    {
        Vector3 mousePos = Input.mousePosition;
        startingPos = mainCamera.transform.position + new Vector3(0, -2, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 dir;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            dir = (hit.point - startingPos).normalized;
        }
        else
        {
            dir = (ray.direction * 1000 + mainCamera.transform.position - startingPos).normalized;
        }
        return dir;
    }
    void LineDraw()
    {
        float timeSpace = (20 - startingPos.z) / speedVector.z / 20f;
        float time;

        for(int i = 0; i < 20; i++)
        {
            time = timeSpace * i;
            Vector3 pos = startingPos + speedVector * time + 0.5f * (accelerate + new Vector3(0, -9.8f, 0) )* time * time;
            line.SetPosition(i, pos);
            track.GetChild(i).position = pos;
        }
    }
}