using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultNamespace;
using ObservableExtensions = UniRx.ObservableExtensions;
using System.Threading.Tasks;
using mud.Unity;
using UniRx;
using IWorld.ContractDefinition;
using mud.Client;

public class BuildingManager : MUDTableToPrefab
{

    protected override void Subscribe(mud.Unity.NetworkManager nm)
    {
        var SpawnSubscription = BuildingTable.OnRecordInsert().ObserveOnMainThread().Subscribe(OnInsertRecord);
        _disposers.Add(SpawnSubscription);

        var UpdateSubscription = ObservableExtensions.Subscribe(BuildingTable.OnRecordUpdate().ObserveOnMainThread(),
                OnUpdateRecord);
        _disposers.Add(UpdateSubscription);

    }

    protected override async void InitTable(NetworkManager nm)
    {

        base.InitTable(nm);

    }

    protected override void OnInsertRecord<T>(T tableUpdate)
    {
        base.OnInsertRecord(tableUpdate);

        BuildingTableUpdate update = tableUpdate as BuildingTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return;
        }
    }

    protected override void OnUpdateRecord<T>(T tableUpdate)
    {
        base.OnUpdateRecord(tableUpdate);

        BuildingTableUpdate update = tableUpdate as BuildingTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return;
        }

    }


}
