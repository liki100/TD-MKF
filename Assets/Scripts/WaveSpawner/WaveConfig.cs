using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "ScriptableObjects/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private List<WaveData> _waves;
    [SerializeField] private List<Enemy> _allEnemyTemplates;

    public List<WaveData> Waves => _waves;
    public List<Enemy> EnemyTemplates => _allEnemyTemplates;


}


