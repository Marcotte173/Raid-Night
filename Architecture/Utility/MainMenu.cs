using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject logo;
    public GameObject menu;
    public float logoTime;
    public static MainMenu instance;
    public GameObject continueButton;

    private void Awake()
    {
        logo.SetActive(true);
        instance = this;
    }
    public void Update()
    {
        if (logoTime > 0) logoTime -= Time.deltaTime;
        else
        {
            logo.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void NewGame()
    {
        DataPersistenceManager.instance.NewGame();
        //UIManager.instance.Menu();

    }
    public void Continue()
    {
        DataPersistenceManager.instance.LoadGame();
        UIManager.instance.Menu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
