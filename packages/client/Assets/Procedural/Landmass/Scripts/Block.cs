using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {Dust, Soil, Grass, Sand, Rock, Forest, Water}
public class Block : Entity
{
    public BlockType material;
    public Renderer r;

    public void SetMaterial(BlockType newMaterial) {
        material = newMaterial;
    }

}
