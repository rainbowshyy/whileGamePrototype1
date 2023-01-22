using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICodeAgent
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform tf;

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private UIEnemyCode codeUI;

    [SerializeField] private MoveableObject move;

    private List<CodeBlock> currentCode;

    private int healthCurrent;
    private int codeStep;

    #region Properties
    public MoveableObject Move
    {
        get { return move; }
    }
    #endregion

    private void Awake()
    {
        SetData(enemyData);

        GameManager.onTick += DoTick;
        GameManager.onNewTick += DoNewTick;
        move.onMove += (MoveEventContext context) => { codeUI.PlaceOnScreen(context.worldPos, Vector2.zero); };
    }

    private void Start()
    {
        move.SetPos(6, 6);
    }

    public void SetData(EnemyData ed)
    {
        enemyData = ed;
        healthCurrent = ed.health;
        codeStep = 0;
        currentCode = new List<CodeBlock>();
        foreach (BaseCodeBlockStruct e in ed.baseCode)
        {
            currentCode.Add(CodeBlockUtility.CodeBlockFromType(e.code, e.param, new bool[2] { true, true }));
        }
        currentCode[0].ReadyCode(this);

        move.XOffset = ed.xOffset;
        move.YOffset = ed.yOffset;
    }

    private void DoTick()
    {
        currentCode[codeStep].RunCode(this);
        codeStep += 1;
        if (codeStep >= currentCode.Count)
            codeStep = 0;
    }

    private void DoNewTick()
    {
        currentCode[codeStep].ReadyCode(this);
        codeUI.SetText(currentCode[codeStep].ShowSyntax());
    }
}
