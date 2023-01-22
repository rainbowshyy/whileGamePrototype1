using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CodeBlock
{
    //public Func<int[], bool> code;
    public int[] parameters;
    public bool[] locked;
    public CodeBlocks type;

    protected CodeBlock(int[] paramParameters, bool[] paramLocked)
    {
        this.parameters = paramParameters;
        this.locked = paramLocked;
    }

    public abstract void ReadyCode(ICodeAgent agent);

    public abstract bool RunCode(ICodeAgent agent);

    public abstract string ShowSyntax();
}

public static class CodeBlockUtility
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

public class MoveBlock : CodeBlock
{
    public MoveBlock(int[] paramParameters, bool[] paramLocked) : base(paramParameters, paramLocked)
    {
        type = CodeBlocks.Move;
        //code = (param) => { PlayerMovement.Instance.MovePlayer(param[0], param[1]); return false; };
    }

    public override void ReadyCode(ICodeAgent agent)
    {

    }

    public override bool RunCode(ICodeAgent agent)
    {
        //return code(new int[] { parameters[0], parameters[1] });
        //PlayerMovement.Instance.MovePlayer(parameters[0], parameters[1]);
        agent.Move.Move(parameters[0], parameters[1]);
        return false;
    }

    public override string ShowSyntax()
    {
        return "Move(<color=#FF3939><b>" + parameters[0].ToString() + "</b></color>, <color=#28FF4F><b>" + parameters[1].ToString() + "</b></color>);";
    }
}

public class MoveTowardsPlayerBlock : CodeBlock
{
    public MoveTowardsPlayerBlock(int[] paramParameters, bool[] paramLocked) : base(paramParameters, paramLocked)
    {
        type = CodeBlocks.MoveTowardsPlayer;
        //code = (paramEnemy, paramParameters) => { return false; };
    }

    public override void ReadyCode(ICodeAgent agent)
    {
        Vector2 playerDelta = new Vector2(Player.Instance.Move.XPos - agent.Move.XPos, Player.Instance.Move.YPos - agent.Move.YPos);
        if (playerDelta.x == 0 && playerDelta.y == 0)
            parameters = new int[2] { 0, 0 };
        else if (Mathf.Abs(playerDelta.x) > Mathf.Abs(playerDelta.y))
            parameters = new int[2] { (int)Mathf.Sign(playerDelta.x), 0 };
        else
            parameters = new int[2] { 0, (int)Mathf.Sign(playerDelta.y) };
    }

    public override bool RunCode(ICodeAgent agent)
    {
        agent.Move.Move(parameters[0], parameters[1]);
        return false;
    }

    public override string ShowSyntax()
    {
        return "Move(<color=#FF3939><b>" + parameters[0].ToString() + "</b></color>, <color=#28FF4F><b>" + parameters[1].ToString() + "</b></color>);";
    }
}

public class WaitBlock : CodeBlock
{
    public WaitBlock(int[] paramParameters, bool[] paramLocked) : base(paramParameters, paramLocked)
    {
        type = CodeBlocks.Wait;
    }

    public override void ReadyCode(ICodeAgent agent)
    {

    }

    public override bool RunCode(ICodeAgent agent)
    {
        return false;
    }

    public override string ShowSyntax()
    {
        return "Wait();";
    }
}

public enum CodeBlocks
{
    Move,
    MoveTowardsPlayer,
    Wait
}

[Serializable]
public struct BaseCodeBlockStruct
{
    public CodeBlocks code;
    public int[] param;
}