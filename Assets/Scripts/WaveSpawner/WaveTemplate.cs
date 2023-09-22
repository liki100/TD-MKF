using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WaveTemplate", menuName = "ScriptableObjects/WaveTemplate")]
public class WaveTemplate : ScriptableObject
{
    [SerializeField] private List<Segment> segments;

    public List<Segment> Segments => segments;
}

[Serializable]
public class Segment
{
    [SerializeField] private float _positionY;
    [SerializeField] private List<Enemies> _enemies;

    public float PositionY => _positionY;
    public List<Enemies> Enemies => _enemies;
}

[Serializable]
public class Enemies
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _count;

    public Enemy Enemy => _enemy;
    public int Count => _count;
}
