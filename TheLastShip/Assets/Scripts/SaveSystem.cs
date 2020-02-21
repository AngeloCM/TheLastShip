using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Store data automatically as player reaches checkpoints.
/// </summary>
public static class SaveSystem 
{
    public static Vector3 CargoShipPosition;
    public static Vector3 PlayerShipPosition;

    public static float CargoShipHealth;
    public static float PlayerShipHealth;

    public static int CheckPointNumber; 

    public static void UpdateCheckPoint (Vector3 cargo, Vector3 player, float cargohp, float playerhp , int checkpointnum )
    {
        CargoShipPosition = cargo;
        PlayerShipPosition = player;

        CargoShipHealth = cargohp;
        PlayerShipHealth = playerhp;

        CheckPointNumber = checkpointnum;
    }
}
