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
        
        var table = PositionTable.GetTableValue(Entity.Key);

        if (table == null)
        {
            Debug.LogError("No position on " + entity.name);
            return;
            // yield break;
        }

        position = new Vector2( (float)table.x, (float)table.y) ;
    }


}
