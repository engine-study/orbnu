using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using mud.Client;

public abstract class MUDComponent : MonoBehaviour
{
    public System.Action OnUpdated;
    public MUDEntity Entity {get{return entity;}}
    
    [Header("Debug")]
    [SerializeField] protected MUDEntity entity;

    public virtual void Init(MUDEntity ourEntity) {
        entity = ourEntity;
        GetTableValue();
    }

    public abstract void GetTableValue();

    public virtual void UpdateComponent(IMudTable table, TableEvent eventType) {

        if(eventType == TableEvent.Insert) {

        } else if(eventType == TableEvent.Delete) {

        } else if(eventType == TableEvent.Update) {
            
        }
    }

}
