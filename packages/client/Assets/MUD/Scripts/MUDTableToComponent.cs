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

public abstract class MUDTableToComponent : MUDTable
{
    public bool SpawnIfNoEntityFound{get{return componentType.prefab != null;}}
    
    [Header("Table")]
    public MUDComponent componentType;
    public Dictionary<string, MUDEntity> EntityComponents;
    public Dictionary<string, MUDComponent> Components;

    protected override void Awake()
    {
        base.Awake();

        if(componentType == null) {
            Debug.LogError("No MUDComponent type to update");
        }

        EntityComponents = new Dictionary<string, MUDEntity>();
    }

    protected override void OnInsertRecord<T>(T tableUpdate)
    {
        base.OnInsertRecord(tableUpdate);
        IngestTableEvent(tableUpdate, TableEvent.Insert);
    }


    protected override void OnDeleteRecord<T>(T tableUpdate)
    {
        base.OnDeleteRecord(tableUpdate);
        IngestTableEvent(tableUpdate, TableEvent.Delete);
    }

    protected override void OnUpdateRecord<T>(T tableUpdate)
    {
        base.OnUpdateRecord(tableUpdate);
        IngestTableEvent(tableUpdate, TableEvent.Update);
    }


    protected abstract MUDComponent TableToMUDComponent<T>(T tableUpdate);
    protected abstract string TableToKey<T>(T tableUpdate);


    protected virtual void IngestTableEvent<T>(T tableUpdate, TableEvent eventType) {

        //process the table event to a key and the entity of that key
        string entityKey = TableToKey(tableUpdate);

        if(string.IsNullOrEmpty(entityKey)) {
            Debug.LogError("No key found in " + gameObject.name, gameObject);
        }

        MUDEntity entity = MUDEntity.GetEntity(entityKey);

        //create the entity if it doesn't exist
        if(entity == null && SpawnIfNoEntityFound) {
            SpawnEntityPrefab(entityKey, componentType.prefab);
        }

        //find the component on that entity
        MUDComponent component = entity.GetMUDComponent(componentType);
        MUDComponent tableComponent = TableToMUDComponent(tableUpdate);

        if(entity != null && SpawnIfNoEntityFound && eventType == TableEvent.Delete) {
            DestroyEntity(entityKey);
        }
        
    }

    protected virtual void UpdateComponent(MUDComponent update, TableEvent eventType) {

         if(eventType == TableEvent.Insert) {

        } else if(eventType == TableEvent.Delete) {

        } else if(eventType == TableEvent.Update) {
            
        }
    }



    protected virtual MUDEntity SpawnEntityPrefab(string newKey, MUDEntity prefab) {

        MUDEntity newEntity = null;

        if(MUDEntity.Entities.ContainsKey(newKey)) {
            //get the entity if it exists
            newEntity = MUDEntity.Entities[newKey];
        } else {
            //spawn the entity if it doesnt exist
            newEntity = Instantiate(prefab,Vector3.up * -1000f, Quaternion.identity);
            EntityComponents.Add(newKey, newEntity);
            Components.Add(newKey, newEntity.GetMUDComponent(componentType));
            newEntity.SetMudKey(newKey);
            MUDEntity.ToggleEntity(true, newEntity);
        }

        return newEntity;
    }

    protected virtual void DestroyEntity(string newKey) {
        MUDEntity newEntity = EntityComponents[newKey];
        EntityComponents.Remove(newKey);

        Destroy(newEntity);

    }

}
