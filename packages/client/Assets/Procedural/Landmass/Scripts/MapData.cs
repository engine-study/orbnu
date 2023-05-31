using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Engine/Map/MapData", order = 1)]
public class MapData : ScriptableObject
{
    [Header("Map Data")]
    public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

    // void OnValidate() {
	// 	Debug.Log("Data validate");
	// }

}
