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
using Cysharp.Threading.Tasks;

public class BuildingManager : MUDTableManager {

    // [Header("Building Manager")]
    public static BuildingManager Instance;

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

        var UpdateSubscription = BuildingTable.OnRecordUpdate().ObserveOnMainThread().Subscribe(OnUpdateRecord);
        _disposers.Add(UpdateSubscription);

    }

    protected override IMudTable RecordUpdateToTable(RecordUpdate tableUpdate)
    {
        BuildingTableUpdate update = tableUpdate as BuildingTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return null;
        }

        return currentValue;

    }

    public void Build(Vector2 position) {
        SendBuildTX(System.Convert.ToInt32(position.x), System.Convert.ToInt32(position.y)).Forget();
    }

    private async UniTaskVoid SendBuildTX(int x, int y)
    {
        try
        {
            // function moveFrom(int32 startX, int32 startY, int32 x, int32 y) public {
            await NetworkManager.Instance.worldSend.TxExecute<BuildFunction>(x, y);
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("Building noise");
        }
    }

}
