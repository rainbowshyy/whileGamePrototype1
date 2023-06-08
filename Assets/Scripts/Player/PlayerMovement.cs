using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEPENDENCIES 
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField] private Transform tf;

    public MoveableObject move;

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
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        xPos = 3;
        yPos = 0;
        UpdatePlayerTransform();
    }

    //Known weakness: diagonal walkable check
    public void MovePlayer(int x, int y)
    {
        if (MapManager.Instance.IsTileWalkable(xPos + x, yPos + y))
        {
            xPos += x;
            yPos += y;
            UpdatePlayerTransform();
        }
    }
    
    private void UpdatePlayerTransform()
    {
        tf.position = new Vector3(xPos - 3, yPos - 2.5f, 0);
    }
}
