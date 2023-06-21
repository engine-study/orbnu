using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultNamespace;
using ObservableExtensions = UniRx.ObservableExtensions;
using System.Threading.Tasks;
using mud.Unity;
using UniRx;
using IWorld.ContractDefinition;

public class BuildingManager : MUDTable
{
    
    protected override void Subscribe(mud.Unity.NetworkManager nm)
    {
        var SpawnSubscription = BuildingTable.OnRecordInsert().ObserveOnMainThread().Subscribe(OnInsert);
        _disposers.Add(SpawnSubscription);

        var UpdateSubscription = ObservableExtensions.Subscribe(BuildingTable.OnRecordUpdate().ObserveOnMainThread(),
                OnUpdate);
        _disposers.Add(UpdateSubscription);

    }

    protected override async void Spawn(NetworkManager nm)
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

        base.Spawn(nm);

    }

}
