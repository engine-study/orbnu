using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    [Header("Cursor")]
    public PlacementUI placement;
    public StatsUI stats;

    void Start() {
        
        SPCursor.OnCursorPosition += OnCursorPosition;
        SPCursor.OnHover += UpdateHover;
    }

    void OnDestroy() {
        SPCursor.OnCursorPosition -= OnCursorPosition;
        SPCursor.OnHover -= UpdateHover;
    }

    void OnCursorPosition(Vector2 newPos) {

        Entity newEntity = MapGenerator.Instance.GetEntityAtPosition(newPos);
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
