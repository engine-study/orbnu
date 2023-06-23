using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

[CreateAssetMenu(fileName = "BlockInfo", menuName = "MUD/Components/BlockInfo", order = 1)]
public class MUDComponentBlockInfo : MUDComponent
{
    [Header("BlockInfo")]
    public int blockNumber;
    public override void GetTableValue()
    {
        throw new System.NotImplementedException();
    }
}
