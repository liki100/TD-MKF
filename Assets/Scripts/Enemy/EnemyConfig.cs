using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private List<EnemyPrefabData> enemyPrefabsData;

    public Enemy Get(EnemyType type, int grade)
    {
        var enemy =
            enemyPrefabsData.FirstOrDefault(x =>
                x.EnemyType == type && x.EnemyGrade == grade);
            
        if (enemy == null || enemy.Prefab == null)
        {
            Debug.LogErrorFormat("Cannot find interactable of type {0} and grade {1}", type, grade);
            return null;
        }

        return enemy.Prefab;
    }
}
