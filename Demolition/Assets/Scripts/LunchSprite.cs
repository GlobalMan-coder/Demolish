using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchSprite : MonoBehaviour
{
    [HideInInspector] public Transform bullet;
    //float firstDistance = 2f;
    //float deviation;
    float scale = 1f;
    Camera camera;
    void Start()
    {
        camera = Camera.main;
        //deviation = firstDistance - (camera.transform.position - bullet.position).magnitude;
    }
    void Update()
    {
        transform.position = camera.WorldToScreenPoint(bullet.position);
        //transform.localScale = Vector3.one * firstDistance / ((camera.transform.position - bullet.position).magnitude + deviation);
        if(scale > 0){
            scale *= 0.9f;
        }
        transform.localScale = Vector3.one * scale;
    }
}
