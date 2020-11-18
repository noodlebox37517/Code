using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Row", menuName = "Level/Row")]
public class Row : ScriptableObject
{
    public Collidable[] objects = new Collidable[3];
    public bool flipable = false;
    public Row FlippedRow;


    public List<GameObject> Spawn(Vector3 spawnpos)
    {
        List<GameObject> created = new List<GameObject>();
        InputController IC = GameMaster.instance.GetComponent<InputController>();
        Collidable[] tempobjects = new Collidable[3];
        for (int i = 0; i < objects.Length; i++)
        {
           tempobjects[i] = objects[i];
        }
            
        
        if (flipable)
        {
            if (Random.value >= .5f)
            {
                Collidable tempc = tempobjects[0];
                tempobjects[0] = tempobjects[2];
                tempobjects[2] = tempc;
            }
        }
        for (int i = 0; i < tempobjects.Length; i++)
        {
            if (tempobjects[i] != null)
            {
                spawnpos = IC.movePositions[i];
                spawnpos.z = GameMaster.instance.spawnOffset;
                GameObject tempGO = Instantiate<GameObject>(tempobjects[i].prefab, spawnpos, Quaternion.identity);
                created.Add(tempGO);
                tempobjects[i].setup(tempGO);
                tempGO.GetComponent<CollidableObject>().Lane(i);

            }
        }

        return created;
    }
    public Collidable[] GetFlipped()
    {
        Collidable[] tempobjects = new Collidable[3];
        for (int i = 0; i < objects.Length; i++)
        {
            tempobjects[i] = objects[i];
        }
        Collidable tempc = tempobjects[0];
        tempobjects[0] = tempobjects[2];
        tempobjects[2] = tempc;
        return tempobjects;
    }
}
