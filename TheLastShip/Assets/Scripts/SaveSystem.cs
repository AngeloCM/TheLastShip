using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
/// <summary>
/// Store data automatically as player reaches checkpoints.
/// </summary>
public class SaveSystem : MonoBehaviour
{
    public static void SaveCheckpoint (PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.exe";
        FileStream stream = new FileStream(path, FileMode.Create);

        //GameplayData data = new GameplayData(player);
    }


}
