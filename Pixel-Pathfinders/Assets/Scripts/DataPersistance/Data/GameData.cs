using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{

    public Dictionary<string, bool> obelisksDestroyed;

    //the values in this constructor will be the default values
    //the game start with when there is no data to load
    public GameData()
    {
        obelisksDestroyed = new Dictionary<string, bool>();
    }
}
