// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;

// public class DataPersistenceManager : MonoBehaviour
// {
//     [Header("File Storage Config")]
//     [SerializeField] private string fileName;

//     private GameData gameData;
//     private List<IDataPersistence> dataPersistenceObjects;
//     private FileDataHandler dataHandler;

//     public static DataPersistenceManager instance { get; private set;}
    
//     private void Awake()
//     {
//         if (instance != null)
//         {
//             Debug.LogError("Found more than on Data Persistnece Manager in scene.");
//         }
//         instance = this;
//     }

//     private void Start()
//     {
//         this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
//         this.dataPersistenceObjects = FindAllDataPersistenceObjects();
//         LoadGame();
//     }

//     private void NewGame()
//     {
//         this.gameData = new GameData();
//     }

//     private void LoadGame()
//     {
//         //load any saved data from a fil using the data handler
//         this.gameData = dataHandler.Load();

//         //if no data can be loaded, initialise a new game
//         if (this.gameData == null)
//         {
//         Debug.Log("No data was found. Initializing data to defaults.");
//         NewGame();
//         } 

//         //push the loaded data to all other scripts that need it
//         foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
//         {
//             dataPersistenceObj.LoadData(gameData);
//         }

//         Debug.Log("Loaded game data");
//     }

//     private void SaveGame()
//     {
//         //pass the data to other scripts so they can update it
//         foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
//         {
//             dataPersistenceObj.SaveData(ref gameData);
//         }
        
//         Debug.Log("Saved game data");

//         //save the data to a file using the data handler
//         dataHandler.Save(gameData);
//     }

//     private void OnApplicationQuit()
//     {
//         SaveGame();
//     }

//     private List<IDataPersistence> FindAllDataPersistenceObjects()
//     {
//         IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
//             .OfType<IDataPersistence>();

//         return new List<IDataPersistence>(dataPersistenceObjects);
//     }
// }
