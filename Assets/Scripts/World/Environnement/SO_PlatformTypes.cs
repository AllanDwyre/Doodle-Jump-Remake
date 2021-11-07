using UnityEngine;

public enum PlateformZoningShape
{
    circle,
    arc,
    rectangle
}

public enum PlatformTypes
{
    neutral,
    bonus,
    obstacle
}

[CreateAssetMenu(menuName = "Scriptable Object/Environnement/Platform")]
public class SO_PlatformTypes : ScriptableObject
{
    [Header("Plateform")]
    public PlatformTypes platformTypes;
    public float presenceChance;
    public GameObject platformPrefab;

    [Header("Allowed Attributes")]
    public PlateformZoningShape allowedZoningShape;
    [Range(0f, 1.5f)]
    public float radiusOfAllowedSpawn;
    public Vector2 vectorOfAllowedSpawn;

    [Header("Prohibited Attributes")]
    public PlateformZoningShape prohibitedZoningShape;

    [Range(0f, 1.5f)]
    public float radiusOfprohibitedSpawn;
    public Vector2 vectorOfprohibitedSpawn;

}
