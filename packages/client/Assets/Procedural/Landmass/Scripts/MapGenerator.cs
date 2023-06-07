using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : Generator {

	public enum DrawMode {None, NoiseMap, ColourMap, Falloff, Blend, Blocks};

 	[Header("Generator")]
    public DrawMode drawMode;
	public MapData mapData;
	public MapRegions mapRegions;
	public bool autoUpdate;
	public bool randomSeed;


	protected GameObject [,] gameobjects;
	protected float[,] noiseMap;	
	protected float[,] falloffMap;
	protected float[,] blendMap;
    protected Color[] colourMap;

	[Header("Debug")]
	public Transform blockParent;
	public MapDisplay display;
	public Generator [] maps;
	public Generator [] regions;

    public override void Generate() {
		base.Generate();

		if(randomSeed) {
			mapData.seed = Random.Range(0,100000000);
		}

		if(mapData.genType == MapData.MapGen.Noise) {
			noiseMap = Noise.GenerateNoiseMap(mapData);	
		} else {
			noiseMap = Noise.GenerateFillMap(mapData);
		}
		blendMap = new float[mapData.mapWidth,mapData.mapHeight];

		FalloffGenerator falloffGenerator = GetComponent<FalloffGenerator>();

		if(falloffGenerator) {
			falloffMap = falloffGenerator.GenerateFalloffMap(mapData.mapWidth, mapData.mapHeight);
		}

		for (int y = 0; y < mapData.mapHeight; y++) {
			for (int x = 0; x < mapData.mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				if(falloffGenerator) {
					currentHeight = Mathf.Clamp01(currentHeight - (1f - falloffMap[x,y]) * mapData.falloffStrength);
				} 
				noiseMap[x,y] = currentHeight;
			}
		}

		maps = GetComponentsInChildren<MapGenerator>();
		foreach(MapGenerator g in maps) {

			if(g == this) 
				continue;
				
			g.Create();
			Merge(this, g, new Vector2(g.transform.position.x, g.transform.position.z));
		}

		regions = GetComponentsInChildren<RegionGenerator>();
		foreach(RegionGenerator g in regions) {

			if(g == this) 
				continue;
				
			g.Generate();
		}

	}

	public override void Render() {
		base.Render();

		blockParent = transform.Find("Blocks");
		if(blockParent != null) {
			DestroyImmediate(blockParent.gameObject);
		}

		if(blockParent == null) {
			blockParent = new GameObject("Blocks").transform;
			blockParent.parent = transform;
			blockParent.localPosition = Vector3.zero;
			blockParent.localRotation = Quaternion.identity;
		}

		GameObject blockPrefab = Resources.Load("Map/Block") as GameObject;

		colourMap = new Color[mapData.mapWidth * mapData.mapHeight];
		for (int y = 0; y < mapData.mapHeight; y++) {
			for (int x = 0; x < mapData.mapWidth; x++) {

				float currentHeight = noiseMap [x, y];
			
				for (int i = 0; i < mapRegions.regions.Length; i++) {
					if (currentHeight <= mapRegions.regions [i].height) {
						colourMap [y * mapData.mapWidth + x] = mapRegions.regions [i].colour;

						Vector3 position = new Vector3(x - mapData.mapWidth * .5f, mapRegions.regions [i].height, y - mapData.mapHeight * .5f);
						// Vector3 position = new Vector3(x - mapData.mapWidth * .5f, mapRegions.regions [i].height * .75f + currentHeight * .5f, y - mapData.mapHeight * .5f);
						// Vector3 position = new Vector3(.5f + x - mapData.mapWidth * .5f, currentHeight, .5f + y - mapData.mapHeight * .5f);
						GameObject block = Instantiate(blockPrefab, position, Quaternion.identity, blockParent);

						break;
					}
				}
			}
		}


		if(display == null) {
			display = Instantiate(Resources.Load("Map/MapDisplay") as GameObject, transform.position, transform.rotation, transform).GetComponent<MapDisplay>();
			display.name = "MapDisplay";
		}

		blockParent.gameObject.SetActive(drawMode == DrawMode.Blocks);
		display.gameObject.SetActive(drawMode != DrawMode.Blocks && drawMode != DrawMode.None);

		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapData.mapWidth, mapData.mapHeight));
		} else if (drawMode == DrawMode.Falloff) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(falloffMap));
		} else if(drawMode == DrawMode.Blend) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(blendMap));
		} else if(drawMode == DrawMode.Blocks) {
			
		}
	}

	public static void Merge(MapGenerator gen, MapGenerator modGen, Vector2 origin) {

		int startX = (int)Mathf.Floor(origin.x + gen.mapData.mapWidth * .5f - modGen.mapData.mapWidth * .5f);
		int startY = (int)Mathf.Floor(origin.y + gen.mapData.mapHeight * .5f - modGen.mapData.mapHeight * .5f);
		
		int iMod = 0;
		for (int i = startX; i < startX + modGen.mapData.mapWidth; i++) {

			int jMod = 0;
			for (int j = startY; j < startY + modGen.mapData.mapHeight; j++) {

				if(i >= 0 && j >= 0 && i < gen.mapData.mapWidth && j < gen.mapData.mapHeight) {
					float currentHeight = Mathf.Lerp(gen.noiseMap[i,j], modGen.noiseMap[iMod,jMod], modGen.falloffMap[iMod,jMod]);
					// float currentHeight = modGen.noiseMap[iMod,jMod];
					gen.blendMap[i,j] = modGen.falloffMap[iMod,jMod];
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