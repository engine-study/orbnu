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

	protected GameObject [,] gameobjects;
	protected float[,] noiseMap;	
	protected float[,] falloffMap;
    protected Color[] colourMap;

	[Header("Debug")]
	public MapDisplay display;
	public Generator [] generators;

    public override void Generate() {
		base.Generate();

		if(randomSeed) {
			mapData.seed = Random.Range(0,100000000);
		}

		noiseMap = Noise.GenerateNoiseMap(mapData);	
		FalloffGenerator falloffGenerator = GetComponent<FalloffGenerator>();

		if(falloffGenerator) {
			falloffMap = falloffGenerator.GenerateFalloffMap(mapData.mapWidth, mapData.mapHeight);
		}

		for (int y = 0; y < mapData.mapHeight; y++) {
			for (int x = 0; x < mapData.mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				if(falloffGenerator) {
					currentHeight = Mathf.Clamp01(currentHeight + (falloffMap[x,y] * falloffGenerator.weight));
				} 
				noiseMap[x,y] = currentHeight;
			}
		}

		generators = GetComponentsInChildren<Generator>();
		foreach(Generator g in generators) {

			if(g == this) 
				continue;
				
			g.Create();

			if(g is MapGenerator)
				Merge(this, g as MapGenerator, new Vector2(g.transform.position.x, g.transform.position.z));
		}

	}

	public override void Render() {
		base.Render();

		colourMap = new Color[mapData.mapWidth * mapData.mapHeight];
		for (int y = 0; y < mapData.mapHeight; y++) {
			for (int x = 0; x < mapData.mapWidth; x++) {

				float currentHeight = noiseMap [x, y];
				
				for (int i = 0; i < mapRegions.regions.Length; i++) {
					if (currentHeight <= mapRegions.regions [i].height) {
						colourMap [y * mapData.mapWidth + x] = mapRegions.regions [i].colour - Color.black * .25f;

						break;
					}
				}
			}
		}


		if(display == null) {
			display = Instantiate(Resources.Load("Map/MapDisplay") as GameObject, transform.position, transform.rotation, transform).GetComponent<MapDisplay>();
			display.name = "MapDisplay";
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

	public void Merge(MapGenerator gen, MapGenerator modGen, Vector2 origin) {

		int startX = (int)Mathf.Floor(origin.x + gen.mapData.mapWidth * .5f - modGen.mapData.mapWidth * .5f);
		int startY = (int)Mathf.Floor(origin.y + gen.mapData.mapHeight * .5f - modGen.mapData.mapHeight * .5f);
		
		int iMod = 0;
		for (int i = startX; i < startX + modGen.mapData.mapWidth; i++) {

			int jMod = 0;
			for (int j = startY; j < startY + modGen.mapData.mapHeight; j++) {

				if(i >= 0 && j >= 0 && i < gen.mapData.mapWidth && j < gen.mapData.mapHeight) {
					Debug.Log(i + " " + j);					
					float currentHeight = Mathf.Lerp(gen.noiseMap[i,j], modGen.noiseMap[iMod,jMod], modGen.falloffMap[iMod,jMod]);
					// float currentHeight = modGen.noiseMap[iMod,jMod];
					gen.noiseMap[i,j] = currentHeight;
				} 

				jMod++;
			}

			iMod++;
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

        Gizmos.DrawWireCube(transform.position, Vector3.up * .01f + Vector3.right * mapData.mapWidth + Vector3.forward * mapData.mapHeight);
        Gizmos.DrawIcon(transform.position, "Map.png", true);
    }
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}