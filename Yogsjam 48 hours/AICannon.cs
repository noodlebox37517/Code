using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICannon : MonoBehaviour
{
    public RangedAttackModel amod;
    public Transform spawnpoint;
    [SerializeField]
    float rotationSpeed = 1;

    float lastShoot = 0;
    [SerializeField]
    float rechargeTime = 2;
    [SerializeField]
    float maxRange = 50;

    [SerializeField]
    ParticleSystem fireEffect;

    // Update is called once per frame
    void Update()
    {
        // find nearest enemy
        GameObject cloest = FindClosestEnemy();
        if (cloest != null && Vector3.Distance(cloest.transform.position, transform.position) < maxRange)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = (cloest.transform.position - transform.position) * -1;

            // The step size is equal to speed times frame time.
            float singleStep = rotationSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step and calculate a rotation a step closer to the target
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(newDirection);

            //calculate distance between current and desired rotation
            float test = Quaternion.Angle(transform.rotation, newRotation);
            // applies rotation to this object
            transform.rotation = newRotation;

            if (test == 0 && lastShoot + rechargeTime < Time.fixedTime)
            {
                lastShoot = Time.fixedTime;
                // TODO shoot here
                Debug.Log("Bang!");
                if (fireEffect != null)
                {
                    fireEffect.Stop();
                    fireEffect.Play();
                }

                amod.Attack(gameObject, spawnpoint, -targetDirection);

            }
        }
    }


    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
