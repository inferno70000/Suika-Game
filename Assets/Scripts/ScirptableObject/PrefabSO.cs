using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefab SO", menuName = "Scriptable Object/Prefab SO")]
public class PrefabSO : ScriptableObject
{
    public float radius;
    public int mass;
    public int score;
    public Vector3 scale;
}
