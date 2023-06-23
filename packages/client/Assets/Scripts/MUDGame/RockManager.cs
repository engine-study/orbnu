using DefaultNamespace;
using IWorld.ContractDefinition;
using mud.Unity;
using UniRx;
using UnityEngine;
using System.Collections;
using ObservableExtensions = UniRx.ObservableExtensions;

public class RockManager : MonoBehaviour
{
    private CompositeDisposable _disposers = new();
    private mud.Unity.NetworkManager net;

    void Start()
    {
        Debug.Log("Rock Manager Start");
        net = NetworkManager.Instance;
        net.OnNetworkInitialized += Spawn;
    }

    void Spawn(NetworkManager nm)
    {
        Debug.Log("ROCK MANAGER SPAWN");

        // net = NetworkManager.Instance;
        // net.OnNetworkInitialized += Spawn;

        // moveMarker.SetActive(false);
        // _player = GetComponent<PlayerSync>();
        var RockSub = RockTable.OnRecordInsert().ObserveOnMainThread().Subscribe(OnUpdateRocks);
        _disposers.Add(RockSub);
    }

    private void OnUpdateRocks(RockTableUpdate update)
    {
        Debug.Log("__________________OnUpdateRocks");

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return;
        }

        StartCoroutine(SpawnRock(update));

    }

    protected IEnumerator SpawnRock(RockTableUpdate update)
    {

        Debug.Log("___ SPAWNING ROCK _____");
        string KeyTrunc = SPHelper.GiveTruncatedHash(update.Key);

        var RockPosition = PositionTable.GetTableValue(update.Key);
        if (RockPosition == null)
        {
            Debug.LogError("No position on " + KeyTrunc);
            yield break;
        }

        var RockSpawnPoint = new Vector3((float)RockPosition.x, 0f, (float)RockPosition.y);
        Debug.Log("Spawning " + KeyTrunc);
        Debug.Log("Position " + RockSpawnPoint.ToString());

        // var Rock = SPGame.SpawnOnt(guestCore, RockSpawnPoint, Quaternion.identity, false, false, false);
        // var Rock = Instantiate(Resources.Load("Ont/RockMUD") as GameObject, RockSpawnPoint, Quaternion.identity);
        
        // SPCore rockCore = new SPCore();
        // rockCore.SetBlock((Resources.Load("Blocks/BlockRock") as SPBlockScriptable).MakeBlock());

        RockMUD rock = Instantiate(Resources.Load("RockMud") as GameObject, RockSpawnPoint, Quaternion.identity).GetComponent<RockMUD>();

        rock.key = update.Key;
        rock.name = "Rock " + KeyTrunc;

        // Rock.GetComponent<RockSync>().key = update.Key;
        // Rock.name = Rock.name + " " + KeyTrunc;

        // Debug.Log("Spawned " + Rock.name);

        // bool isLocal = update.Key == net.addressKey;

        // if (isLocal) {
        //     RockSync.localRockKey = update.Key;
        //     Debug.Log("Setting local Rock key to " + KeyTrunc + "...");
        // }

        // var RockMUD = Rock.GetComponent<RockMUD>();
        // RockMUD.SetIsLocal(isLocal);
        // // RockMUD.SetCore(new SPCoreRock());
        // RockMUD.SetMudKey(update.Key);


    }

    private void OnDestroy()
    {
        _disposers?.Dispose();
    }
}