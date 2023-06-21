using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    [Header("Cursor")]
    public PlacementUI placement;
    public StatsUI stats;
    public InfoUI info;
    public Entity entity;
    public Ground terrain;

    void Awake() {
        SPCursor.OnHover += UpdateHover;
        SPCursor.OnGridPosition += OnCursorPosition;
    }

    void OnDestroy() {
        SPCursor.OnHover -= UpdateHover;
        SPCursor.OnGridPosition -= OnCursorPosition;
    }

    void OnCursorPosition(Vector3 newPos) {
        
        Entity newEntity = MapGenerator.GetEntityAtPosition((Vector3)newPos);
        entity = newEntity;

        if(entity == null) {
            stats.ToggleWindow(false);
        } else {
            stats.ToggleWindow(true);
            stats.UpdateEntity(entity);
        }


        Ground newTerrain = MapGenerator.GetTerrainAtPosition((Vector3)newPos);
        terrain = newTerrain;
        
        if(terrain == null) {
            info.ToggleWindow(false);
        } else {
            info.ToggleWindow(true);
            info.UpdateEntity(terrain);
        }

    }

    void UpdateHover(Entity newEntity) {
      
    }



}
