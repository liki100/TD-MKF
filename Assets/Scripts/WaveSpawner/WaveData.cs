using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave")]
public class WaveData : ScriptableObject
{
    [SerializeField] private List<WaveTemplate> _waveTemplates;
    [SerializeField] private float _delayAfterSpawn;
    
    public List<WaveTemplate> WaveTemplates => _waveTemplates;
    public float Delay => _delayAfterSpawn;
}
