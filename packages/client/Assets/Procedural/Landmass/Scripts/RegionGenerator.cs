using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RegionGenerator : MonoBehaviour
{
    public RegionData regionData;

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, Vector3.up * .01f + (Vector3.right + Vector3.forward) * regionData.size);
    }



}
