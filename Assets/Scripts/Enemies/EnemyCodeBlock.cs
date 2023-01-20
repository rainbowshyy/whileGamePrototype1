using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyCodeBlock
{
    //public Func<Enemy, int[], bool> code;
    public int[] parameters;
    public EnemyCodeBlocks type;

    protected EnemyCodeBlock(Enemy enemy, int[] paramParameters)
    {
        this.parameters = paramParameters;
    }

    public abstract void ReadyCode(Enemy enemy);

    public abstract Enemy RunCode(Enemy enemy);

    public abstract string ShowSyntax();
}

public class EnemyWaitBlock : EnemyCodeBlock
{
    public EnemyWaitBlock(Enemy paramEnemy, int[] paramParameters) : base(paramEnemy, paramParameters)
    {
        type = EnemyCodeBlocks.Wait;
    }

    public override void ReadyCode(Enemy enemy)
    {

    }

    public override Enemy RunCode(Enemy enemy)
    {
        return enemy;
    }

    public override string ShowSyntax()
    {
        return "Wait();";
    }
}

public class EnemyMoveTowardsPlayerBlock : EnemyCodeBlock
{
    public EnemyMoveTowardsPlayerBlock(Enemy paramEnemy, int[] paramParameters) : base(paramEnemy, paramParameters)
    {
        type = EnemyCodeBlocks.MoveTowardsPlayer;
        //code = (paramEnemy, paramParameters) => { return false; };
    }

    public override void ReadyCode(Enemy enemy)
    {
        Vector2 playerDelta = new Vector2(PlayerMovement.Instance.XPos - enemy.XPos, PlayerMovement.Instance.YPos - enemy.YPos);
        if (playerDelta.x == 0 && playerDelta.y == 0)
            parameters = new int[2] { 0, 0 };
        else if (Mathf.Abs(playerDelta.x) > Mathf.Abs(playerDelta.y))
            parameters = new int[2] { (int)Mathf.Sign(playerDelta.x), 0 };
        else
            parameters = new int[2] { 0, (int)Mathf.Sign(playerDelta.y) };
    }

    public override Enemy RunCode(Enemy paramEnemy)
    {
        Enemy enemy = paramEnemy;

        enemy.MoveEnemy(parameters[0], parameters[1]);

        return enemy;
    }

    public override string ShowSyntax()
    {
        return "Move(<color=#FF3939><b>" + parameters[0].ToString() + "</b></color>, <color=#28FF4F><b>" + parameters[1].ToString() + "</b></color>);";
    }
}

public enum EnemyCodeBlocks
{
    Wait,
    Move,
    MoveTowardsPlayer
}

[Serializable]
public struct EnemyBaseCodeBlockStruct
{
    public EnemyCodeBlocks code;
    public int[] param;
}
