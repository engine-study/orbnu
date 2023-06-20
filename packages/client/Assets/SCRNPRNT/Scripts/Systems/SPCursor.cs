using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCursor : MonoBehaviour
{
    public System.Action<Entity> OnHover;
    public bool grid;
    public Transform graphics;
    Vector3 rawMousePos, mousePos, lastPos;
    Entity hover, lastHover;

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

        if(mousePos != lastPos) {
            UpdateHover();
        }
    }

    void UpdateHover() {

        lastHover = hover;
        hover = GetEntityFromRadius(mousePos,.25f);

        if(lastHover != hover) {
            OnHover.Invoke(hover);
        }
    }


    Collider[] hits;

    Entity GetEntityFromRadius(Vector3 position, float radius)
    {

        if (hits == null) { hits = new Collider[10]; }

        int amount = Physics.OverlapSphereNonAlloc(SPInput.MouseWorldPos, radius, hits, LayerMask.NameToLayer("Nothing"), QueryTriggerInteraction.Collide);
        int selectedItem = -1;
        float minDistance = 999f;
        Entity bestItem = null;

        for (int i = 0; i < amount; i++)
        {
            Entity checkItem = hits[i].GetComponentInParent<Entity>();

            if (!checkItem)
                continue;

            float distance = Vector3.Distance(SPInput.MouseWorldPos, checkItem.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                selectedItem = i;
                bestItem = checkItem;
            }
        }

        return bestItem;

    }
}
