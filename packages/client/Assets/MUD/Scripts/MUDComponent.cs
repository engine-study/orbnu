using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class MUDComponent : ScriptableObject
{
    public MUDEntity Entity {get{return entity;}}
    
    [Header("Component")]
    [SerializeField] protected MUDEntity entity;

    public virtual void Init(MUDEntity ourEntity) {
        entity = ourEntity;
        UpdateFromTable();
    }

    public abstract void UpdateFromTable();
}
