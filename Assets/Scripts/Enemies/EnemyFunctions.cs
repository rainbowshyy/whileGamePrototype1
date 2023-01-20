using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFunctions
{
    public static EnemyCodeBlock CodeBlockFromType(Enemy enemy, EnemyCodeBlocks type, int[] parameters)
    {
        if (type == EnemyCodeBlocks.Wait)
            return new EnemyWaitBlock(enemy, parameters);
        else if (type == EnemyCodeBlocks.MoveTowardsPlayer)
            return new EnemyMoveTowardsPlayerBlock(enemy, parameters);
        else
            return null;
    }
}
