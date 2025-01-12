using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ObservableExtensions = UniRx.ObservableExtensions;
using mud.Unity;
using mud.Client;
using UniRx;
using DefaultNamespace;

public class PositionManager : MUDTableManager
{

    protected override void Subscribe(mud.Unity.NetworkManager nm)
    {

        var InsertSub = ObservableExtensions.Subscribe(PositionTable.OnRecordInsert().ObserveOnMainThread(),
                OnInsertRecord);
        _disposers.Add(InsertSub);

        var UpdateSub = ObservableExtensions.Subscribe(PositionTable.OnRecordUpdate().ObserveOnMainThread(),
                OnUpdateRecord);
        _disposers.Add(UpdateSub);
    }

    public static void GetPosition(MUDComponentPosition component)
    {

        MUDComponentPosition positionComp = component as MUDComponentPosition;

        var table = PositionTable.GetTableValue(component.Entity.Key);

        if (table == null)
        {
            Debug.LogError("No position on " + component.Entity.gameObject, component.Entity.gameObject);
            return;
        }

        positionComp.UpdateComponent(table, TableEvent.Manual);
    }


    protected override IMudTable RecordUpdateToTable(RecordUpdate tableUpdate)
    {
        PositionTableUpdate update = tableUpdate as PositionTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return null;
        }

        return currentValue;
    }


}
