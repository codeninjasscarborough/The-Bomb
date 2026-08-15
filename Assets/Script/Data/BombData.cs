using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb Level", menuName = "Bombs/Bomb Data")]
public class BombData : ScriptableObject
{
    public int levelNum;
    public float rating;
    public float cost;
    public Mesh bombMesh;

    public float damage;
    public float moneyMult;
    public float risk;

    public float explosionRadius;
    public float moveSpeedDuringDrop;
    public float moveSpeedBeforeDrop;
    public float gravityScale;

}
