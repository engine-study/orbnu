using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//given an entity with a component, spawn it
public class MUDSpawner : MonoBehaviour
{

    public Dictionary<string, MUDEntity> entityTable;

    public void AddEntity(string key, MUDEntity newEntity) {
        entityTable.Add(key,newEntity);
    }

    public void DeleteEntity(string key) {
        entityTable.Remove(key);
    }

    public MUDEntity GetEntity(string key) {
        if(!entityTable.ContainsKey(key)) {
            return null;
        } else {
            return entityTable[key];
        }
    }

    public MUDEntity SpawnEntityOfComponent(string key, GameObject prefab) {

        GameObject newGo = Instantiate(prefab, transform.position,Quaternion.identity);
        MUDEntity newEntity = newGo.GetComponent<MUDEntity>();
        
        if(newEntity == null) {
            newEntity = newGo.AddComponent<MUDEntity>();
        }

        newEntity.SetMudKey(key);

        return newEntity;
    }

    // void Spawn()
    // {
    //     string KeyTrunc = SPHelper.GiveTruncatedHash(update.Key);

    //     var playerPosition = PositionTable.GetTableValue(update.Key);
    //     if (playerPosition == null)
    //     {
    //         Debug.LogError("No position on " + KeyTrunc);
    //         yield break;
    //     }

    //     while (MUDGame.NetworkLoaded == false) { yield return null; }

    //     var playerSpawnPoint = new Vector3((float)playerPosition.x, 0f, (float)playerPosition.y);
    //     Debug.Log("Spawning " + KeyTrunc);
    //     Debug.Log("Position " + playerSpawnPoint.ToString());

    //     MUDEntity player = new MUDEntity();

    //     player.GetComponent<PlayerSync>().key = update.Key;
    //     player.name = player.name + " " + KeyTrunc;

    //     Debug.Log("Spawned " + player.name);

    //     bool isLocal = update.Key == net.addressKey;

    //     if (isLocal)
    //     {
    //         PlayerSync.localPlayerKey = update.Key;
    //         Debug.Log("Setting local player key to " + KeyTrunc + "...");
    //     }

    //     var playerMUD = player.GetComponent<PlayerMUD>();
    //     playerMUD.SetIsLocal(isLocal);
    //     playerMUD.SetMudKey(update.Key);
    // }
}
