using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPCursor : MonoBehaviour
{
    public bool grid;
    public Transform graphics;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = SPInput.MouseWorldPos;

        if(grid) {
            graphics.position = new Vector3(Mathf.Round(mousePos.x * 1f) / 1f, mousePos.y, Mathf.Round(mousePos.z * 1f)/1f);

        } else {
            graphics.position = Vector3.MoveTowards(graphics.position, mousePos, 50f * Time.deltaTime);
        }
    }
}
