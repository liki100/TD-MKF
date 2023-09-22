using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    
    public void Init()
    {
        transform.position = _startPosition;
    }
}
