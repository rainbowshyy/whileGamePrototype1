using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFunctions
{
    public static CodeBlock CodeBlockFromType(CodeBlocks type, int[] parameters, bool[] locked)
    {
        if (type == CodeBlocks.Wait)
            return new WaitBlock(parameters, locked);
        else if (type == CodeBlocks.MoveTowardsPlayer)
            return new MoveTowardsPlayerBlock(parameters, locked);
        else
            return null;
    }
}
