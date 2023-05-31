using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapRegions", menuName = "Engine/Map/MapRegions", order = 1)]
public class MapRegions : ScriptableObject
{
    [Header("Map Regions")]
   	public TerrainType[] regions;


}
