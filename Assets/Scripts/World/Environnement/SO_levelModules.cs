using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Object/Environnement/Level Module")]
public class SO_levelModules : ScriptableObject
{
    public List<SO_PlatformTypes> allowPlatforms = new List<SO_PlatformTypes>();

    public Vector2 height;

    public bool isByDefault;
    public Vector2Int platformsNumbers;

}