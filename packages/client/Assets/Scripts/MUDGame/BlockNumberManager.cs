using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using ObservableExtensions = UniRx.ObservableExtensions;
using UniRx;

public class BlockNumberManager : MUDTableUpdate
{
    protected override void Subscribe(mud.Unity.NetworkManager nm)
    {
        var Sub  = ObservableExtensions.Subscribe(BlockInfoTable.OnRecordUpdate().ObserveOnMainThread(),
                TableUpdate);
        _disposers.Add(Sub);
    }

    public override void UpdateComponent(MUDComponent component) {

        MUDComponentPosition positionComp = component as MUDComponentPosition;

        var tablueValue = PositionTable.GetTableValue(component.Entity.Key);

        if (tablueValue == null)
        {
            Debug.LogError("No position on " + component.Entity.gameObject);
            return;
        }

        positionComp.position = new Vector2((float)tablueValue.x, (float)tablueValue.y);
        
    }


    protected override void TableUpdate<T>(T tableUpdate)
    {
        base.TableUpdate(tableUpdate);


    }
}
