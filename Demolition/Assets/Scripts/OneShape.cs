using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShape : MonoBehaviour
{
    public bool hasBeenDestroyed;

    public List<Transform> checkersList;

    // Start is called before the first frame update
    void Start()
    {
        FindCheckers();
    }

    void FindCheckers()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "checker")
            {
                checkersList.Add(child);
            }
        }
    }

    public void CheckerHasFallen()
    {
        if (AllCheckersFallen())
        {
            if (!hasBeenDestroyed)
            {
                hasBeenDestroyed = true;
                Debug.Log("Destroyed!");
                GetComponentInParent<OneRound>().ShapeHasFallen();
            }
        }
    }

    bool AllCheckersFallen()
    {
        for(int i = 0; i < checkersList.Count; i++)
        {
            if (!checkersList[i].GetComponent<Checker>().hasFallen)
            {
                return false;
            }
        }

        return true;
    }
}
