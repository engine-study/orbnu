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

public class BuildingManager : MUDTableToComponent {

    public BuildingManager Instance;

    protected override void Awake() {
        base.Awake();
        Instance = this;
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        Instance = null;
    }

    protected override void Subscribe(mud.Unity.NetworkManager nm)
    {
        var SpawnSubscription = BuildingTable.OnRecordInsert().ObserveOnMainThread().Subscribe(OnInsertRecord);
        _disposers.Add(SpawnSubscription);

        var UpdateSubscription = ObservableExtensions.Subscribe(BuildingTable.OnRecordUpdate().ObserveOnMainThread(),
                OnUpdateRecord);
        _disposers.Add(UpdateSubscription);

    }

    protected override string TableToKey<T>(T tableUpdate) {return (tableUpdate as BuildingTableUpdate).Key;}

    protected override MUDComponent TableToMUDComponent<T>(T tableUpdate)
    {
        BuildingTableUpdate update = tableUpdate as BuildingTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return null;
        }

        // MUDComponent newComponent = Instantiate(componentType) as MUDComponent;

        return null;

    }

}
