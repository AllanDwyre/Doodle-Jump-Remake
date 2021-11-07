using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum allowRotation
{
    all,
    x,
    y,
    z
}

[CreateAssetMenu(menuName = "Scriptable Object/Background/Probe")]
public class SO_Probes : ScriptableObject
{
    public float Radius;
    [Tooltip("The maximum scale the object can have")] public int maxScale;
    [Tooltip("The minimum Z position of the object")] public int minZ;
    [Tooltip("The chance of the object spawn")] public float presenceChance;
    [Tooltip("If the object is allow to rotate")] public bool canRotate;
    [Tooltip("The Axis of rotation of the object")] public allowRotation allowedRotation;
    [Tooltip("The object prefab")] public GameObject probe;
}
