using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainMaterial {Dust, Soil, Grass, Sand, Rock, Forest, Water}
public class Terrain : Entity
{
    public TerrainMaterial material;
    public Renderer r;

    public void SetMaterial(TerrainMaterial newMaterial) {
        material = newMaterial;
    }

}
