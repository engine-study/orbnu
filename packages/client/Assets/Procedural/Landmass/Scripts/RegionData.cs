using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "RegionData", menuName = "Engine/Map/RegionData", order = 1)]
public class RegionData : ScriptableObject
{
    [Header("Region Data")]
    public string name;
    public int radius;

}