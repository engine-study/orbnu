using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap, Falloff, Blocks};
	public DrawMode drawMode;

	public MapData mapData;
	public MapRegions mapRegions;
	public bool autoUpdate;
	public bool randomSeed;

	public GameObject blockParent;

	public RegionGenerator [] generators;

	float[,] falloffMap;

	public void GenerateMap() {

		if(randomSeed) {
			mapData.seed = Random.Range(0,100000000);
		}

		generators = GetComponentsInChildren<RegionGenerator>();
		
		float[,] noiseMap = Noise.GenerateNoiseMap(mapData);	

		Color[] colourMap = new Color[mapData.mapWidth * mapData.mapHeight];
		for (int y = 0; y < mapData.mapHeight; y++) {
			for (int x = 0; x < mapData.mapWidth; x++) {
				float currentHeight = Mathf.Clamp01(noiseMap [x, y] - falloffMap[x,y]);
				
				for (int i = 0; i < mapRegions.regions.Length; i++) {
					if (currentHeight <= mapRegions.regions [i].height) {
						colourMap [y * mapData.mapWidth + x] = mapRegions.regions [i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> (true);

		blockParent.SetActive(drawMode == DrawMode.Blocks);
		display.gameObject.SetActive(drawMode != DrawMode.Blocks);

		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapData.mapWidth, mapData.mapHeight));
		} else if (drawMode == DrawMode.Falloff) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(falloffMap));
		} else if(drawMode == DrawMode.Blocks) {

		}
	}

	void OnValidate() {

		// Debug.Log("Editor validate");

		if (mapData.mapWidth < 1) {
			Debug.LogError("No map width");
			mapData.mapWidth = 1;
		}
		if (mapData.mapHeight < 1) {
			Debug.LogError("No map height");
			mapData.mapHeight = 1;
		}

		if (mapData.mapWidth % 2 == 0) {
			Debug.LogError("Must have odd numbered width");
			mapData.mapWidth += 1;
		}
		if (mapData.mapHeight % 2 == 0) {
			Debug.LogError("Must have odd numbered height");
			mapData.mapHeight += 1;
		}

		if (mapData.lacunarity < 1) {
			Debug.LogError("No map lacunarity");
			mapData.lacunarity = 1;
		}
		if (mapData.octaves < 0) {
			Debug.LogError("No octaves");
			mapData.octaves = 0;
		}

		falloffMap = FalloffGenerator.GenerateFalloffMap(mapData.mapWidth, mapData.mapHeight);
	}

	void OnDrawGizmos()
    {
        // Draws the Light bulb icon at position of the object.
        // Because we draw it inside OnDrawGizmos the icon is also pickable
        // in the scene view.

        Gizmos.DrawIcon(transform.position, "Map.png", true);
    }
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}