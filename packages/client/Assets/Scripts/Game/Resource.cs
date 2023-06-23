using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Structure
{
    public override void OnDrawGizmosSelected() {
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position + Vector3.up * 1f, "World Pos: " + transform.position.ToString() + "\nGrid Pos: " + gridPos.ToString());
        #endif
    }
}
