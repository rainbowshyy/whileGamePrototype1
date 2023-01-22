using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ICodeAgent
{
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private MoveableObject move;

    public Projectiles type;

    #region Properties

    public MoveableObject Move { get { return move; } }

    #endregion

    private void Awake()
    {
        Move.SetPos(6, 6);
    }
}

public enum Projectiles
{
    Blue
}
