using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using ObservableExtensions = UniRx.ObservableExtensions;
using UniRx;
using mud.Client;
public class BlockNumberManager : MUDTableManager
{
    protected override void Subscribe(mud.Unity.NetworkManager nm)
    {
        var Sub  = ObservableExtensions.Subscribe(BlockInfoTable.OnRecordUpdate().ObserveOnMainThread(),
                OnInsertRecord);
        _disposers.Add(Sub);
    }


    protected override IMudTable RecordUpdateToTable(RecordUpdate tableUpdate)
    {
        return null;
    }


}
