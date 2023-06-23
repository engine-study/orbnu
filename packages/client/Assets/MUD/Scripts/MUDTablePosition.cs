using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ObservableExtensions = UniRx.ObservableExtensions;
using mud.Unity;
using UniRx;
using DefaultNamespace;

public class MUDTablePosition : MUDTableToComponent
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

    protected override string TableToKey<T>(T tableUpdate) {return (tableUpdate as PositionTableUpdate).Key;}

    public static void GetPosition(MUDComponentPosition component)
    {

        MUDComponentPosition positionComp = component as MUDComponentPosition;

        var table = PositionTable.GetTableValue(component.Entity.Key);

        if (table == null)
        {
            Debug.LogError("No position on " + component.Entity.gameObject, component.Entity.gameObject);
            return;
        }

        positionComp.position = new Vector2((float)table.x, (float)table.y);
    }

    protected override MUDComponent TableToMUDComponent<T>(T tableUpdate)
    {
        PositionTableUpdate update = tableUpdate as PositionTableUpdate;

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null)
        {
            Debug.LogError("No currentValue");
            return null;
        }

        MUDComponentPosition newComponent = Instantiate(componentType) as MUDComponentPosition;
        newComponent.position = new Vector2((float)currentValue.x, (float)currentValue.y);

        return newComponent;
    }


}
