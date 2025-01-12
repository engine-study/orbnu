using DefaultNamespace;
using IWorld.ContractDefinition;
using mud.Unity;
using UniRx;
using UnityEngine;
using System.Collections;
using ObservableExtensions = UniRx.ObservableExtensions;
using System.Threading.Tasks;

public class PlayerManager : MUDTableManager
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
            await nm.worldSend.TxExecute<SpawnFunction>(0, 0);

        }

        base.InitTable(nm);

    }

    protected override mud.Client.IMudTable RecordUpdateToTable(mud.Client.RecordUpdate tableUpdate)
    {
        return (tableUpdate as PlayerTableUpdate).TypedValue.Item1;
    }

}
