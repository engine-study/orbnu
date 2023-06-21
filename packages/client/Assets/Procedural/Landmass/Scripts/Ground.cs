using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundMaterial {Dust, Soil, Grass, Sand, Rock, Forest, Water}
public class Ground : Entity
{
    public GroundMaterial material;
    public Renderer r;

    public void SetMaterial(GroundMaterial newMaterial) {
        material = newMaterial;
    }

}
