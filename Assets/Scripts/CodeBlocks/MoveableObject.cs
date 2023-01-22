using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private Transform tf;

    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    private int xPos;
    private int yPos;

    public Action<MoveEventContext> onMove;

    #region Properties
    public int XPos
    {
        get { return xPos; }
    }
    public int YPos
    {
        get { return yPos; }
    }
    public float XOffset
    {
        get { return XOffset; }
        set
        {
            xOffset = value;
            UpdateTransform();
        }
    }
    public float YOffset
    {
        get { return YOffset; }
        set
        {
            yOffset = value;
            UpdateTransform();
        }
    }
    #endregion

    public void Move(int x, int y)
    {
        if (MapManager.Instance.IsTileWalkable(xPos + x, yPos + y))
        {
            xPos += x;
            yPos += y;
            UpdateTransform();
            onMove?.Invoke(new MoveEventContext(xPos, yPos, tf.position));
        }
    }

    private void UpdateTransform()
    {
        tf.position = new Vector3(xPos - 3 + xOffset, yPos - 2.5f + yOffset, 0);
    }

    public void SetPos(int x, int y)
    {
        xPos = x;
        yPos = y;
        UpdateTransform();
        onMove?.Invoke(new MoveEventContext(xPos, yPos, tf.position));
    }
}

public struct MoveEventContext
{
    public int x;
    public int y;
    public Vector3 worldPos;

    public MoveEventContext(int paramX, int paramY, Vector3 paramWorldPos)
    {
        this.x = paramX;
        this.y = paramY;
        this.worldPos = paramWorldPos;
    }
}
