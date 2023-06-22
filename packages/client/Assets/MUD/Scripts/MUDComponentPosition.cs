using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Position", menuName = "MUD/Components/Position", order = 1)]
public class MUDComponentPosition : MUDComponent
{
    [Header("Position")]
    public Vector2 position;

    public override void UpdateFromTable() {
        
        var tablePosition = PositionTable.GetTableValue(Entity.Key);

        if (tablePosition == null)
        {
            Debug.LogError("No position on " + entity.name);
            // yield break;
        }

        position = new Vector2( (float)tablePosition.x, (float)tablePosition.y) ;
    }


}
