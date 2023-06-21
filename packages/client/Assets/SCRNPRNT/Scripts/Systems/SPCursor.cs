using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCursor : MonoBehaviour
{
    public static System.Action<Entity> OnHover;
    public static System.Action<Vector3> OnCursorPosition;
    [Header("Cursor")]
    public bool grid;
    public Transform graphics;
    [SerializeField] Vector3 rawMousePos, mousePos, lastPos;
    [SerializeField] Vector3 gridPos, lastGridPos;
    [SerializeField] Entity hover, lastHover;

    // Update is called once per frame
    void Update()
    {
        UpdateMouse();
    }

    void UpdateMouse()
    {
        lastPos = rawMousePos;
        rawMousePos = SPInput.MouseWorldPos;

        if (grid)
        {
            mousePos = new Vector3(Mathf.Round(rawMousePos.x * 1f) / 1f, rawMousePos.y, Mathf.Round(rawMousePos.z * 1f) / 1f);
        }
        else
        {
            mousePos = Vector3.MoveTowards(graphics.position, rawMousePos, 50f * Time.deltaTime);
        }

        graphics.position = mousePos;

        lastGridPos = gridPos;
        // gridPos = MapGenerator.PositionToGrid(mousePos);
        gridPos = MapGenerator.WorldToGrid(mousePos);
        if(gridPos != lastGridPos) {
            if(OnCursorPosition != null)
                OnCursorPosition.Invoke(gridPos);
        }

        if(mousePos != lastPos) {
            UpdateHover();
        }
    }

    void UpdateHover() {

        lastHover = hover;
        hover = GetEntityFromRadius(mousePos + Vector3.up * .1f,.25f);

        if(lastHover != hover) {
            OnHover?.Invoke(hover);
        }


    }


    Collider[] hits;
    public Entity GetEntityFromRadius(Vector3 position, float radius) {
        if (hits == null) { hits = new Collider[10]; }

        int amount = Physics.OverlapSphereNonAlloc(SPInput.MouseWorldPos, radius, hits, LayerMask.NameToLayer("Nothing"), QueryTriggerInteraction.Collide);
        int selectedItem = -1;
        float minDistance = 999f;
        Entity bestItem = null;
        List<Entity> entities = new List<Entity>();

        for (int i = 0; i < amount; i++)
        {
            Entity checkItem = hits[i].GetComponentInParent<Entity>();

            if (!checkItem)
                continue;

            entities.Add(checkItem);

            float distance = Vector3.Distance(SPInput.MouseWorldPos, hits[i].ClosestPoint(position));
            if (distance < minDistance)
            {
                minDistance = distance;
                selectedItem = i;
                bestItem = checkItem;
            }
        }

        return bestItem;
    }

    public Entity [] GetEntitiesFromRadius(Vector3 position, float radius)
    {

        if (hits == null) { hits = new Collider[10]; }

        int amount = Physics.OverlapSphereNonAlloc(SPInput.MouseWorldPos, radius, hits, LayerMask.NameToLayer("Nothing"), QueryTriggerInteraction.Collide);
        int selectedItem = -1;
        float minDistance = 999f;
        Entity bestItem = null;
        List<Entity> entities = new List<Entity>();

        for (int i = 0; i < amount; i++)
        {
            Entity checkItem = hits[i].GetComponentInParent<Entity>();

            if (!checkItem)
                continue;

            entities.Add(checkItem);

            float distance = Vector3.Distance(SPInput.MouseWorldPos, checkItem.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                selectedItem = i;
                bestItem = checkItem;
            }
        }

        // return bestItem;

        return entities.ToArray();

    }
}
