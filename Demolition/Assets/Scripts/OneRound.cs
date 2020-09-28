using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneRound : MonoBehaviour
{
    public Vector3 cameraPosition;

    public List<OneShape> shapesList;

    Manager manager;

    public int numberOfBallsForBonus = 5;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();

        cameraPosition = transform.GetChild(0).transform.position;

        FindShapes();
    }
    

    void FindShapes()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<OneShape>())
            shapesList.Add(child.GetComponent<OneShape>());
        }
    }

    public void ShapeHasFallen()
    {
        if (AllShapesFallen())
        {
            Debug.Log("round complete");
            manager.RoundWasComplete();
        }
    }

    bool AllShapesFallen()
    {
        for (int i = 0; i < shapesList.Count; i++)
        {
            if (!shapesList[i].GetComponent<OneShape>().hasBeenDestroyed)
            {
                return false;
            }
        }

        return true;
    }
}
