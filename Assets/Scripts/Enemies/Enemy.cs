using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform tf;

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private UIEnemyCode codeUI;

    private List<EnemyCodeBlock> currentCode;

    private int healthCurrent;
    private int codeStep;
    private int xPos;
    private int yPos;

    #region Properties
    public int XPos
    {
        get { return xPos; }
    }
    public int YPos
    {
        get { return yPos; }
    }
    #endregion

    private void Awake()
    {
        xPos = 6;
        yPos = 6;

        SetData(enemyData);

        GameManager.onTick += DoTick;
        GameManager.onNewTick += DoNewTick;

    }

    private void Start()
    {
        UpdateEnemytransform();
    }

    public void SetData(EnemyData ed)
    {
        enemyData = ed;
        healthCurrent = ed.health;
        codeStep = 0;
        currentCode = new List<EnemyCodeBlock>();
        foreach (EnemyBaseCodeBlockStruct e in ed.baseCode)
        {
            currentCode.Add(EnemyFunctions.CodeBlockFromType(this, e.code, e.param));
        }
        currentCode[0].ReadyCode(this);
    }

    public void MoveEnemy(int x, int y)
    {
        if (MapManager.Instance.IsTileWalkable(xPos + x, yPos + y))
        {
            xPos += x;
            yPos += y;
            UpdateEnemytransform();
        }
    }

    private void UpdateEnemytransform()
    {
        tf.position = new Vector3(xPos - 3, yPos - 2.375f, 0);
        codeUI.PlaceOnScreen(tf.position, new Vector2(0, -24f));
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
