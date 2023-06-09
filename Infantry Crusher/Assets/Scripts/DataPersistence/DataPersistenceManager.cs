using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
     [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public static DataPersistenceManager instance{get; private set; }
    public GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;
    private void Awake()
    {
        if(instance != null)
        {
            throw new System.Exception("Found more than one Data Persistence Manager in scene");
        }

        instance = this;
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();   
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if(gameData == null)
        {
            NewGame();
        }

        for (int i = 0; i < dataPersistenceObjects.Count; i++)
        {
            dataPersistenceObjects[i].LoadData(gameData);
        }
    }

    public void SaveGame()
    {
         for (int i = 0; i < dataPersistenceObjects.Count; i++)
        {
            dataPersistenceObjects[i].SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();   
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return  new List<IDataPersistence>(dataObjects);
    }
}
