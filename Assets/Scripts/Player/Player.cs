using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICodeAgent
{
    public static Player Instance;

    [SerializeField] private MoveableObject move; 
    #region Properties

    public MoveableObject Move
    {
        get { return move; }
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
        Move.SetPos(3, 0);
    }
}
