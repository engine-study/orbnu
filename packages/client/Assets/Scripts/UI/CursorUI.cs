using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    [Header("Cursor")]
    public PlacementUI placement;
    public StatsUI stats;

    void Awake() {
        SPCursor.OnHover += UpdateHover;
        SPCursor.OnCursorPosition += OnCursorPosition;
    }

    void OnDestroy() {
        SPCursor.OnHover -= UpdateHover;
        SPCursor.OnCursorPosition -= OnCursorPosition;
    }

    void OnCursorPosition(Vector3 newPos) {
        
        Entity newEntity = MapGenerator.GetEntityAtPosition((Vector3)newPos);

        if(newEntity == null) {
            stats.ToggleWindow(false);
        } else {
            stats.ToggleWindow(true);
            stats.UpdateEntity(newEntity);
        }

    }

    void UpdateHover(Entity newEntity) {
        
      
    }



}
