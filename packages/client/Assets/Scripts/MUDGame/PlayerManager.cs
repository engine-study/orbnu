using DefaultNamespace;
using IWorld.ContractDefinition;
using mud.Unity;
using UniRx;
using UnityEngine;
using System.Collections;
using ObservableExtensions = UniRx.ObservableExtensions;
using System.Threading.Tasks;

public class PlayerManager : MUDTableToPrefab
{
    protected override void Subscribe(NetworkManager nm)
    {
        var SpawnSubscription = PlayerTable.OnRecordInsert().ObserveOnMainThread().Subscribe(OnInsertRecord);
        _disposers.Add(SpawnSubscription);

        var UpdateSubscription = ObservableExtensions.Subscribe(PlayerTable.OnRecordUpdate().ObserveOnMainThread(),
                OnUpdateRecord);
        _disposers.Add(UpdateSubscription);
    }

    protected override async void InitTable(NetworkManager nm)
    {
        Debug.Log("PLAYER MANAGER SPAWN");

        var addressKey = net.addressKey;
        var currentPlayer = PlayerTable.GetTableValue(addressKey);

        if (currentPlayer == null)
        {
            // spawn the player
            Debug.Log("Spawning TX...");
            await nm.worldSend.TxExecute<SpawnFunction>(0, 20);

        }

        base.InitTable(nm);

    }


    protected override void OnInsertRecord<T>(T tableUpdate)
    {
        base.OnInsertRecord(tableUpdate);

        PlayerTableUpdate update = tableUpdate as PlayerTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return;
        }

        StartCoroutine(SpawnPlayer(update));

    }

    protected override void OnUpdateRecord<T>(T tableUpdate)
    {
        base.OnUpdateRecord(tableUpdate);

        PlayerTableUpdate update = tableUpdate as PlayerTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return;
        }

    }

    protected IEnumerator SpawnPlayer(PlayerTableUpdate update)
    {

        string KeyTrunc = SPHelper.GiveTruncatedHash(update.Key);

        var playerPosition = PositionTable.GetTableValue(update.Key);
        if (playerPosition == null)
        {
            Debug.LogError("No position on " + KeyTrunc);
            yield break;
        }

        while (MUDGame.NetworkLoaded == false) { yield return null; }

        var playerSpawnPoint = new Vector3((float)playerPosition.x, 0f, (float)playerPosition.y);
        Debug.Log("Spawning " + KeyTrunc);
        Debug.Log("Position " + playerSpawnPoint.ToString());

        MUDEntity player = new MUDEntity();

        player.GetComponent<PlayerSync>().key = update.Key;
        player.name = player.name + " " + KeyTrunc;

        Debug.Log("Spawned " + player.name);

        bool isLocal = update.Key == net.addressKey;

        if (isLocal)
        {
            PlayerSync.localPlayerKey = update.Key;
            Debug.Log("Setting local player key to " + KeyTrunc + "...");
        }

        var playerMUD = player.GetComponent<PlayerMUD>();
        playerMUD.SetIsLocal(isLocal);
        playerMUD.SetMudKey(update.Key);

    }

}