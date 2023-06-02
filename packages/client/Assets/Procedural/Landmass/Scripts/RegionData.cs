using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "RegionData", menuName = "Engine/Map/RegionData", order = 1)]
public class RegionData : ScriptableObject
{
    [Header("Region Data")]
    public string regionName;
    public float size = 1f;

    
	void OnValidate() {
        if (size % 2 == 0) {
			Debug.LogError("Size must be odd");
			size += 1;
		}

    }

}