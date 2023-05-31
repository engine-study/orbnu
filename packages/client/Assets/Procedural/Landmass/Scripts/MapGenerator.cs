using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap};
	public DrawMode drawMode;

	public MapData mapData;
	public MapRegions mapRegions;
	public bool autoUpdate;
	public bool randomSeed;


	public void GenerateMap() {

		if(randomSeed) {
			mapData.seed = Random.Range(0,100000000);
		}
		
		float[,] noiseMap = Noise.GenerateNoiseMap(mapData);

		Color[] colourMap = new Color[mapData.mapWidth * mapData.mapHeight];
		for (int y = 0; y < mapData.mapHeight; y++) {
			for (int x = 0; x < mapData.mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < mapRegions.regions.Length; i++) {
					if (currentHeight <= mapRegions.regions [i].height) {
						colourMap [y * mapData.mapWidth + x] = mapRegions.regions [i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapData.mapWidth, mapData.mapHeight));
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
		if (mapData.lacunarity < 1) {
			Debug.LogError("No map lacunarity");
			mapData.lacunarity = 1;
		}
		if (mapData.octaves < 0) {
			Debug.LogError("No octaves");
			mapData.octaves = 0;
		}


	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}