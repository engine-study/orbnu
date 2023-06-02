using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : Generator {

	public enum DrawMode {NoiseMap, ColourMap, Falloff, Blocks};

 	[Header("Generator")]
    public DrawMode drawMode;
	public MapData mapData;
	public MapRegions mapRegions;
	public bool autoUpdate;
	public bool randomSeed;

    protected Color[] colourMap;
	protected float[,] noiseMap;	
	protected float[,] falloffMap;

	[Header("Debug")]
	public MapDisplay display;
	public RegionGenerator [] generators;


    public override void Generate() {
		base.Generate();

		generators = GetComponentsInChildren<RegionGenerator>();
		foreach(Generator g in generators) {
			if(g == this) 
				continue;

			g.Generate();
		}


		if(display == null) {
			display = Instantiate(Resources.Load("Map/MapDisplay") as GameObject, transform.position, transform.rotation, transform).GetComponent<MapDisplay>();
			display.name = "MapDisplay";
		}

		if(randomSeed) {
			mapData.seed = Random.Range(0,100000000);
		}

		noiseMap = Noise.GenerateNoiseMap(mapData);	
		FalloffGenerator falloffGenerator = GetComponent<FalloffGenerator>();

		if(falloffGenerator) {
			falloffMap = falloffGenerator.GenerateFalloffMap(mapData.mapWidth, mapData.mapHeight);
		}

		colourMap = new Color[mapData.mapWidth * mapData.mapHeight];
		for (int y = 0; y < mapData.mapHeight; y++) {
			for (int x = 0; x < mapData.mapWidth; x++) {

				float currentHeight = noiseMap [x, y];

				if(falloffGenerator) {
					currentHeight = Mathf.Clamp01(currentHeight + (falloffMap[x,y] * falloffGenerator.weight));
				} 
				
				for (int i = 0; i < mapRegions.regions.Length; i++) {
					if (currentHeight <= mapRegions.regions [i].height) {
						colourMap [y * mapData.mapWidth + x] = mapRegions.regions [i].colour;
						break;
					}
				}
			}
		}

		
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