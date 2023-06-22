using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using ObservableExtensions = UniRx.ObservableExtensions;
using UniRx;

public class BlockNumberManager : MUDTableToComponent
{
    protected override void Subscribe(mud.Unity.NetworkManager nm)
    {
        var Sub  = ObservableExtensions.Subscribe(BlockInfoTable.OnRecordUpdate().ObserveOnMainThread(),
                OnInsertRecord);
        _disposers.Add(Sub);
    }


    protected override string TableToKey<T>(T tableUpdate) {return (tableUpdate as BlockInfoTableUpdate).Key;}
    protected override MUDComponent TableToMUDComponent<T>(T tableUpdate)
    {
        throw new System.NotImplementedException();
    }


}
