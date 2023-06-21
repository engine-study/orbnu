using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    [Header("Cursor")]
    public PlacementUI placement;
    public StatsUI stats;
    public Entity entity;

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

    }

    void UpdateHover(Entity newEntity) {
      
    }



}
