using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Position", menuName = "MUD/Components/Position", order = 1)]
public class MUDComponentPosition : MUDComponent
{
   [Header("Position")]
    public Vector2 position;

    public override void InitFromTable() {
        // MUDTablePosition.UpdatePosition(this);
    }


}
