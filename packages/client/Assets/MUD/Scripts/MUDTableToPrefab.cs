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

    protected override void OnInsertRecord<T>(T tableUpdate)
    {
        base.OnInsertRecord(tableUpdate);
        TableSpawn(tableUpdate);
    }


    protected override void OnDeleteRecord<T>(T tableUpdate)
    {
        base.OnDeleteRecord(tableUpdate);
        TableDestroy(tableUpdate);
    }

    protected override void OnUpdateRecord<T>(T tableUpdate)
    {
        base.OnUpdateRecord(tableUpdate);
        TableUpdate(tableUpdate);
    }

    protected virtual void TableSpawn<T>(T tableUpdate)
    {

    }

    protected virtual void TableUpdate<T>(T tableUpdate)
    {

    }

    protected virtual void TableDestroy<T>(T tableUpdate)
    {

    }


}
