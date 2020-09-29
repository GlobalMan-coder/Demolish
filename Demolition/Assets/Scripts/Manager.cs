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

    public GameObject roundCompleteText;

    public List<GameObject> progressBarImagesList;

    // Start is called before the first frame update
    void Start()
    {
        FindRounds();

        roundCompleteText.SetActive(false);

        for(int i = 0; i < progressBarImagesList.Count; i++)
        {
            progressBarImagesList[i].SetActive(false);
        }
    }
   

    public void Restart(int index)
    {
        SceneManager.LoadScene(index);
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
        roundCompleteText.SetActive(true);
        roundCompleteText.GetComponent<Animation>().Play();

        progressBarImagesList[roundIndex].SetActive(true);

        roundIndex++;
        if(roundIndex < roundsList.Count)
        {
            positionTGoTo = roundsList[roundIndex].cameraPosition;
            MoveCameraToNextRound();
        }

        StartCoroutine(WaitToDeactivateRound());
    }

    IEnumerator WaitToDeactivateRound()
    {
        for (int i = 0; i < roundsList.Count; i++)
        {
            if (roundIndex == i)
            {
                roundsList[i].gameObject.SetActive(true);
            }
        }

        yield return new WaitForSeconds(2);

        for(int i = 0; i < roundsList.Count; i++)
        {
            if(i < roundIndex)
            {
                roundsList[i].gameObject.SetActive(false);
            }
        }
    }
}
