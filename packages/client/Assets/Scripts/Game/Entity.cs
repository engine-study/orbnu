using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mud.Client;
public class Entity : MUDEntity
{
    
    [Header("Entity")]
    public Stats stats;
    public Vector3 gridPos;

    public override void Init()
    {
        base.Init();

    }

    public virtual void OnDrawGizmosSelected() {
        // #if UNITY_EDITOR
        // UnityEditor.Handles.Label(transform.position, "World Pos: " + transform.position.ToString() + "\nGrid Pos: " + gridPos.ToString());
        // #endif
    }
}
