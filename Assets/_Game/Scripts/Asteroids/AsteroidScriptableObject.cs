using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroid Stats", fileName = "New Asteroid Stats")]
public class AsteroidScriptableObject : ScriptableObject
{
    [SerializeField] private Vector2 _force = new Vector2(200, 600);
    [SerializeField] private Vector2 _size = new Vector2(0.2f, 1);
    [SerializeField] private Vector2 _torque = new Vector2(0.1f, 0.5f);

    [SerializeField] private bool _shouldSplit = false;
    [SerializeField] private int _splitCount = 2;
    [SerializeField] private AsteroidScriptableObject _splitStats;

    public float MinForce => _force.x;
    public float MaxForce => _force.y;
    public float MinSize => _size.x;
    public float MaxSize => _size.y;
    public float MinTorque => _torque.x;
    public float MaxTorque => _torque.y;
    public bool ShoudSplit => _shouldSplit;
    public int SplitCount => _splitCount;
    public AsteroidScriptableObject SplitStats => _splitStats;
}
