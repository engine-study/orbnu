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
            graphics.position = new Vector3(Mathf.Round(mousePos.x * 100f) / 100f, mousePos.y, Mathf.Round(mousePos.z * 100f)/100f);

        } else {
            graphics.position = Vector3.MoveTowards(graphics.position, mousePos, 50f * Time.deltaTime);
        }
    }
}
