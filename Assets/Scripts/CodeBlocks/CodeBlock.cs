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

    public abstract bool RunCode();

    public abstract string ShowSyntax();
}

public class MoveBlock : CodeBlock
{
    public MoveBlock(int[] paramParameters, bool[] paramLocked) : base(paramParameters, paramLocked)
    {
        type = CodeBlocks.Move;
        //code = (param) => { PlayerMovement.Instance.MovePlayer(param[0], param[1]); return false; };
    }

    public override bool RunCode()
    {
        //return code(new int[] { parameters[0], parameters[1] });
        PlayerMovement.Instance.MovePlayer(parameters[0], parameters[1]);
        return false;
    }

    public override string ShowSyntax()
    {
        return "Move(<color=#FF3939><b>" + parameters[0].ToString() + "</b></color>, <color=#28FF4F><b>" + parameters[1].ToString() + "</b></color>);";
    }
}

public enum CodeBlocks
{
    Move
}