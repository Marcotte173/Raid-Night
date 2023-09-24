using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // if no data can be loaded, initialize to a new game
        if (this.gameData == null) Utility.instance.TurnOff(MainMenu.instance.continueButton.gameObject);
        else Utility.instance.TurnOn(MainMenu.instance.continueButton.gameObject);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        ///
        //All the things that a new game needs
        ///
        //Set currency and renown
        Guild.instance.guildCurrency = Guild.instance.guildRenown = 10;
        //Set the time
        TimeManagement.instance.day = 1;
        TimeManagement.instance.week = 1;
        TimeManagement.instance.month = 1;
        TimeManagement.instance.year = 2022;
        TimeManagement.instance.hour = 7;
        TimeManagement.instance.UpdateTimeDisplay();
        TimeManagement.instance.UpdateEvents();
        //Create Players
        CreateAgent.instance.CreatePlayers();
       
    }

    public void LoadGame()
    {
        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
