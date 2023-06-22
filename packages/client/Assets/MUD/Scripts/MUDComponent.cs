using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class MUDComponent : ScriptableObject
{
    public System.Action OnUpdated;
    public MUDEntity Entity {get{return entity;}}
    
    [Header("Component")]
    public MUDEntity prefab;
    
    [Header("Debug")]
    [SerializeField] protected MUDEntity entity;

    public virtual void Init(MUDEntity ourEntity) {
        entity = ourEntity;
        InitFromTable();
    }

    public abstract void InitFromTable();
}
