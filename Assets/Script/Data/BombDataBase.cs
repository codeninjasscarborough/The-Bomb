using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb Database", menuName = "Bombs/Bomb Database")]
public class BombDataBase : ScriptableObject
{
    [SerializeField] private List<BombData> allBombs;

    public BombData GetBombAtIndex(int index) {
        if (index < BombCount)
        {
            return allBombs[index];
        }
        return null;
    }
    public int BombCount => allBombs.Count;

}
