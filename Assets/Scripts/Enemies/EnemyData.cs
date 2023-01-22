using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy/Create new enemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public List<BaseCodeBlockStruct> baseCode;
    public string enemyName;
    public int health;

    public float xOffset;
    public float yOffset;
}
