using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ICodeAgent
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform tf;

    [SerializeField] private MoveableObject move;

    private int codeStep;

    #region Properties

    public MoveableObject Move { get { return move; } }

    #endregion

    private void Awake()
    {
        Move.SetPos(6, 6);
    }
}
