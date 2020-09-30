using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject camera;

    public int roundIndex;
    public List<OneRound> roundsList;

    Vector3 positionTGoTo;

    public GameObject roundCompleteText;

    public List<GameObject> progressBarImagesList;

    int amountOfCoins;
    public Text moneyText;
    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        amountOfCoins = PlayerPrefs.GetInt("coins", 0);
        UpdateMoney(0);

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

        //StartCoroutine(GiveCoins());

        roundIndex++;
        if(roundIndex < roundsList.Count)
        {
            positionTGoTo = roundsList[roundIndex].cameraPosition;
            MoveCameraToNextRound();
        }

        PlayerPrefs.SetInt("coins", amountOfCoins);

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

    public IEnumerator GiveCoins()
    {
        for(int i = 0; i < 8; i++)
        {
            float random = Random.Range(0f, 0.1f);
            yield return new WaitForSeconds(random);

            GameObject oneCoin = Instantiate(coinPrefab);
            oneCoin.transform.position = roundsList[roundIndex].transform.position +
                new Vector3(0,0,20);

            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            oneCoin.transform.position += new Vector3(randomX, randomY, randomZ);

        }
    }

    public void UpdateMoney(int amount)
    {
        amountOfCoins += amount;
        moneyText.text = amountOfCoins.ToString();
    }
}
