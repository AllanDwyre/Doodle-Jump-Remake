using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] List<SO_Probes> probesSetting = new List<SO_Probes>();

    [Header("Parametre")]
    public int numberOfProbes = 20;
    public Vector3 offset;
    public Vector3 bounds;
    [SerializeField] private int incrementHeight;
    Vector3 pos;

    public int currentCubeNbr = 0;
    private void Start()
    {
        pos = new Vector3(offset.x, offset.y, offset.z);
    }

    private void Update()
    {
        while (currentCubeNbr <= numberOfProbes)
        {
            int beforeRejection = 10;
            int index = 0;
            bool isValidate = false;
            do
            {
                index = Random.Range(0, probesSetting.Count);
                int chance = Random.Range(0, 100);
                if (chance < probesSetting[index].presenceChance)
                {
                    isValidate = true;
                }
            } while (isValidate == false && beforeRejection > 0);
            // Choice 

            // Position du futur probe : 
            pos.x = Random.Range(-bounds.x, bounds.x) + offset.x;
            pos.y += Random.Range(2, incrementHeight);
            if (probesSetting[index].minZ == 0)
            {
                pos.z = Random.Range(-bounds.z, bounds.z) + offset.z;
            }
            else // permet detre au moin au Z minimum
            {
                pos.z = Random.Range(probesSetting[index].minZ - offset.z, bounds.z) + offset.z;
            }
            //Instantiate
            GameObject currentCube = Instantiate(probesSetting[index].probe, pos, Quaternion.identity, this.transform);

            // Scale
            int scale = Random.Range(1, probesSetting[index].maxScale);
            currentCube.transform.localScale = new Vector3(scale, scale, scale);
            // Rotation
            if (probesSetting[index].canRotate)
            {
                Quaternion currentRot = currentCube.transform.rotation;
                if (probesSetting[index].allowedRotation == allowRotation.all)
                {
                    Vector3 rot = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
                    currentCube.transform.rotation = Quaternion.Euler(rot);
                }
                else if (probesSetting[index].allowedRotation == allowRotation.x)
                {
                    Vector3 rot = new Vector3(Random.Range(-180, 180), currentRot.y, currentRot.z);
                    currentCube.transform.rotation = Quaternion.Euler(rot);
                }
                else if (probesSetting[index].allowedRotation == allowRotation.y)
                {
                    Vector3 rot = new Vector3(currentRot.y, Random.Range(-180, 180), currentRot.z);
                    currentCube.transform.rotation = Quaternion.Euler(rot);
                }
                else if (probesSetting[index].allowedRotation == allowRotation.z)
                {
                    Vector3 rot = new Vector3(currentRot.x, currentRot.y, Random.Range(-180, 180));
                    currentCube.transform.rotation = Quaternion.Euler(rot);
                }
            }

            //Increment
            currentCubeNbr++;
        }
    }

}
