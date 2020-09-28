using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugUI : MonoBehaviour
{
    public GameObject buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons.SetActive(false);
    }

    public void ActivateButtons()
    {
        buttons.SetActive(!buttons.activeSelf);
    }

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
