using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Manager : MonoBehaviour
{
    public GameObject camera;

    public int roundIndex;
    public List<OneRound> roundsList;

    Vector3 positionTGoTo;

    // Start is called before the first frame update
    void Start()
    {
        FindRounds();
    }
   

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void FindRounds()
    {
        foreach(OneRound round in FindObjectsOfType<OneRound>())
        {
            roundsList.Add(round);

            roundsList = roundsList.OrderBy(
            x => x.transform.position.x
            ).ToList();
        }
    }

    void MoveCameraToNextRound()
    {
        iTween.MoveTo(camera,
            iTween.Hash("position", positionTGoTo, "easeType", "easeInOutExpo", "time", 2f,
            "delay", 1f));
    }

    public void RoundWasComplete()
    {
        roundIndex++;
        if(roundIndex < roundsList.Count)
        {
            positionTGoTo = roundsList[roundIndex].cameraPosition;
            MoveCameraToNextRound();
        }
    }
}
