using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "rndmspawner", menuName = "Collidables/rndmspawner")]

public class RandomSpawner : Collidable
{
    public List<Collidable> spawnList;
    public List<float> spawnchance;

    public override void  setup(GameObject GO)
    {

        int spawn = SelectSpawn();
        Vector3 spawnpos = GO.transform.position;
        Destroy(GO);
        GameObject tempGO = Instantiate<GameObject>(spawnList[spawn].prefab, spawnpos, Quaternion.identity);
        spawnList[spawn].setup(tempGO);

        GO.name = this.name;

    }

    public int SelectSpawn()
    {
        int i = 0;
        float RF = Random.value;
        while (RF < spawnchance.Count && spawnchance[0] < 1.0f)
        {
            i++;
        }

        return i;
    }
}
