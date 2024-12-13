using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    [Serializable]
    public struct EnemyData
    {
        public string nameClip;
        public int health;
        public float speed;
    }

    public List<EnemyData> enemyDatas;
}