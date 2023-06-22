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

public abstract class MUDTableToPrefab : MUDTable
{
    [Header("Table")]
    public MUDEntity componentPrefab;
    public MUDComponent [] components;

    protected override void OnInsertRecord<T>(T tableUpdate)
    {
        base.OnInsertRecord(tableUpdate);
        Spawn(tableUpdate);
    }


    protected override void OnDeleteRecord<T>(T tableUpdate)
    {
        base.OnDeleteRecord(tableUpdate);
        Destroy(tableUpdate);
    }

    protected override void OnUpdateRecord<T>(T tableUpdate)
    {
        base.OnUpdateRecord(tableUpdate);
        Update(tableUpdate);
    }

    protected virtual void Spawn<T>(T tableUpdate)
    {

    }

    protected virtual void Update<T>(T tableUpdate)
    {

    }

    protected virtual void Destroy<T>(T tableUpdate)
    {

    }


}
