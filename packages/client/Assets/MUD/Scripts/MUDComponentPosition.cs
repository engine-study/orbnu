using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Position", menuName = "MUD/Components/Position", order = 1)]
public class MUDComponentPosition : MUDComponent
{
   [Header("Position")]
    public Vector2 position;

    public override void GetTableValue() {
        MUDTablePosition.GetPosition(this);
    }

    public override void UpdateComponent(MUDComponent update, TableEvent eventType)
    {
        base.UpdateComponent(update, eventType);

        position = (update as MUDComponentPosition).position;
        entity.gameObject.transform.position = new Vector3(position.x, 0f, position.y);
    }

}
