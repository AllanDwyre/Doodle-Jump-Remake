using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject spawnPlatform;
    [SerializeField] private List<SO_levelModules> levelModules = new List<SO_levelModules>();
    [SerializeField] SO_levelModules basicModule;

    [SerializeField] private int targetNumOfPlatform = 100;
    [SerializeField] private float levelWidth = 4f;

    List<SO_PlatformTypes> allowPlatforms = new List<SO_PlatformTypes>();
    //[HideInInspector] 
    public int currentNumberPlateform = 0;

    SO_levelModules currentModule = null;
    bool moduleInProgress = false;
    int currentNumberModule = 0;
    Vector2 Yclamp;
    GameObject previousObject;
    SO_PlatformTypes previousPlatformType;

    private GameObject player;
    private void Start()
    {
        currentModule = basicModule;
        allowPlatforms = currentModule.allowPlatforms;
        Yclamp = currentModule.height;

        previousObject = spawnPlatform;
        previousPlatformType = allowPlatforms[0];

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        if (currentNumberPlateform < targetNumOfPlatform)
        {
            Random.InitState(Random.Range(0, 1000));
            Module();
            currentNumberModule++;

            SO_PlatformTypes newPlatformType;
            if (moduleInProgress)
            {
                newPlatformType = allowPlatforms[Random.Range(0, allowPlatforms.Count)];
            }
            else
            {
                newPlatformType = GetPlatformByPourcentage();
            }

            GameObject newObject = Instantiate(
                newPlatformType.platformPrefab,
                GetplatformPosition(),
                Quaternion.identity,
                transform
                );

            currentNumberPlateform++;
            previousObject = newObject;
            previousPlatformType = newPlatformType;
        }
    }
    private void Module()
    {
        if (Random.Range(0, 50) < 1 && !moduleInProgress)
        {
            moduleInProgress = true;
            currentModule = levelModules[Random.Range(0, levelModules.Count)];
            allowPlatforms = currentModule.allowPlatforms;
            Yclamp = currentModule.height;
            currentNumberModule = 0;
        }
        if (moduleInProgress && currentNumberModule > Random.Range(currentModule.platformsNumbers.x, currentModule.platformsNumbers.y))
        {
            currentModule = basicModule;
            allowPlatforms = basicModule.allowPlatforms;
            moduleInProgress = false;
        }
    }
    private SO_PlatformTypes GetPlatformByPourcentage()
    {
        int beforeRejection = 10;
        int index = 0;
        bool isValidate = false;
        do // Pour la chance de presence : 
        {
            index = Random.Range(0, allowPlatforms.Count);
            if (previousPlatformType != null && previousPlatformType.platformTypes == PlatformTypes.obstacle) // Pour eviter un second obstacle
            {
                if (allowPlatforms[index].platformTypes != PlatformTypes.obstacle)
                {
                    int chance = Random.Range(0, 100);

                    if (chance < allowPlatforms[index].presenceChance)
                    {
                        isValidate = true;
                    }
                }
            }
            else
            {
                int chance = Random.Range(0, 100);

                if (chance < allowPlatforms[index].presenceChance)
                {
                    isValidate = true;
                }
                beforeRejection--;
            }
        } while (isValidate == false && beforeRejection > 0);
        return allowPlatforms[index];
    }

    private Vector3 GetplatformPosition()
    {
        Vector3 pos;

        pos = new Vector3(Random.Range(-levelWidth, levelWidth), previousObject.transform.position.y + Random.Range(Yclamp.x, Yclamp.y), 0);
        if (!CheckDistance(pos))
        {
            return GetplatformPosition();
        }
        return pos;
    }

    private bool CheckDistance(Vector3 pos)
    {
        if (Vector3.Distance(previousObject.transform.position, pos) > 4f)//previousPlatformType.radiusOfprohibitedSpawn)
        {
            return true;
        }
        return false;
    }
}
///// <summary>
///// permet d'avoir le nombre voulu de plateform
///// </summary>
//private void SpawnPlatform()
//{
//    while (currentPlateform < numberOfPlateforms) // Pour remplir le nombre 
//    {
//        PlateformPosition();
//        int index = GetPlatformByPourcentage();

//        Instantiate(platforms[index].platformPrefab, spawnPosition, Quaternion.identity, this.transform);
//        previousPlateformSO = platforms[index];
//        currentPlateform++;
//    }
//}
//private void PlateformPosition()
//{
//    if (previousPlateformSO != null && previousPlateformSO.platformTypes != PlatformTypes.obstacle) // Pour plateforme sans obstacle
//    {
//        spawnPosition.y += Random.Range(minY, maxY);
//    }
//    else // Pour garantir un chemin
//    {
//        spawnPosition.y += Random.Range(-minY, maxY / 2f);
//    }
//    spawnPosition.x = Random.Range(-levelWidth, levelWidth);
//}

//private int GetPlatformByPourcentage()
//{
//    int beforeRejection = 10;
//    int index = 0;
//    bool isValidate = false;
//    do // Pour la chance de presence : 
//    {
//        index = Random.Range(0, platforms.Count);
//        if (previousPlateformSO != null && previousPlateformSO.platformTypes == PlatformTypes.obstacle) // Pour eviter un second obstacle
//        {
//            if (platforms[index].platformTypes != PlatformTypes.obstacle)
//            {
//                int chance = Random.Range(0, 100);

//                if (chance < platforms[index].presenceChance)
//                {
//                    isValidate = true;
//                }
//            }
//        }
//        else
//        {
//            int chance = Random.Range(0, 100);

//            if (chance < platforms[index].presenceChance)
//            {
//                isValidate = true;
//            }
//            beforeRejection--;
//        }
//    } while (isValidate == false && beforeRejection > 0);
//    return index;
//}
