using UnityEngine;

[System.Serializable]
public class EnemyPrefabData
{
    [SerializeField] private EnemyType _enemyType;

    [SerializeField] private int _enemyGrade;
        
    [SerializeField] private Enemy _prefab;
        
    public EnemyType EnemyType => _enemyType;

    public int EnemyGrade => _enemyGrade;

    public Enemy Prefab => _prefab;
}