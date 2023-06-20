using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Entity
{
    [System.Serializable]
    public class Stats {
        int health = 0;
        int attack = 0;
        int energy = 0;
    }

    public Stats stats;
    public MeshRenderer [] mesh;

    public override void OnDrawGizmosSelected() {
        // #if UNITY_EDITOR
        // UnityEditor.Handles.Label(transform.position + Vector3.up * 1f, "World Pos: " + transform.position.ToString() + "\nGrid Pos: " + gridPos.ToString() + "\nBlock Pos: " + MapGenerator.Instance.WorldToGrid(transform.position));
        // #endif
    }

    
}
