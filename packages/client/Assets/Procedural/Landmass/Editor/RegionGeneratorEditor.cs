using UnityEngine;
using System.Collections;
using UnityEditor;

// Create a 180 degrees wire arc with a ScaleValueHandle attached to the disc
// lets you visualize some info of the transform

[CustomEditor(typeof(RegionGenerator))]
class RegionGeneratorEditor : Editor
{
    void OnSceneGUI()
    {
        RegionGenerator region = (RegionGenerator)target;
        if (region == null)
        {
            return;
        }

        Handles.color = Color.blue;
        Handles.Label(region.transform.position + Vector3.up * 2, region.regionData.regionName + "\n" + region.transform.position.ToString());

        Handles.BeginGUI();
        if (GUILayout.Button("Reset Area", GUILayout.Width(100)))
        {
            region.regionData.size = 5;
        }
        Handles.EndGUI();

        Handles.DrawWireDisc(region.transform.position, Vector3.up, region.regionData.size);

    }
}