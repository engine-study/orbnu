using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MUDTableUpdate : MUDTable
{
    [Header("Table")]
    public MUDComponent componentType;
    
    public abstract void UpdateComponent(MUDComponent component);

    protected override void OnInsertRecord<T>(T tableUpdate)
    {
        base.OnInsertRecord(tableUpdate);
        TableInsert(tableUpdate);
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

    protected virtual void TableInsert<T>(T tableUpdate)
    {

    }

    protected virtual void TableUpdate<T>(T tableUpdate)
    {

    }

    protected virtual void TableDestroy<T>(T tableUpdate)
    {

    }

}
