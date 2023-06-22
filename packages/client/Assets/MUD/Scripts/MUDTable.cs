using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IWorld.ContractDefinition;
using mud.Unity;
using mud.Client;
using NetworkManager = mud.Unity.NetworkManager;
using UniRx;
using ObservableExtensions = UniRx.ObservableExtensions;
using System.Threading.Tasks;

public abstract class MUDTable : MonoBehaviour
{


    protected CompositeDisposable _disposers = new();
    protected mud.Unity.NetworkManager net;
    public Action OnAdded, OnUpdated, OnDeleted;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        Debug.Log(gameObject.name + " Start");

        net = mud.Unity.NetworkManager.Instance;
        net.OnNetworkInitialized += InitTable;
    }

    protected virtual void OnDestroy()
    {
        _disposers?.Dispose();
        net.OnNetworkInitialized -= InitTable;
    }

    // var SpawnSubscription = table.OnRecordInsert().ObserveOnMainThread().Subscribe(OnUpdateTable);
    // _disposers.Add(SpawnSubscription);

    // var UpdateSubscription  = ObservableExtensions.Subscribe(PositionTable.OnRecordUpdate().ObserveOnMainThread(),
    //         OnChainPositionUpdate);
    // _disposers.Add(UpdateSubscription);

    protected virtual async void InitTable(NetworkManager nm)
    {
        Subscribe(nm);
    }

    protected abstract void Subscribe(NetworkManager nm);

    protected virtual void OnInsertRecord<T>(T tableUpdate)
    {

    }

    protected virtual void OnDeleteRecord<T>(T tableUpdate)
    {

    }

    protected virtual void OnUpdateRecord<T>(T tableUpdate)
    {

    }


}
