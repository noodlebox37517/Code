using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Tools 
{
    public static int Randsign()
    {
        if ((Random.value > 0.5f))
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    /// <summary>
    /// this method will return the player tagged object, it uses find so is clucnky replace with better system. 
    /// do not call every frame
    /// TODO
    /// </summary>
    /// <returns></returns>
    public static GameObject Player()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if(go== null)
        {
            Debug.LogWarning("Player not found");
        }
      return  GameObject.FindGameObjectWithTag("Player");
    }
    public static Vector2 VelocityInterception2D(Vector2 RunnerPos,Vector2 RunnerVel, Vector2 ChaserPos, float chaserSpeed)
    {
        bool possible = false;
        Vector2 intercept = RunnerPos;
        float tintersept =0f;
        //are they same pos
        if (ChaserPos == (RunnerPos))
        {
            possible = true;
            intercept = RunnerPos;
            tintersept = 0;
           // m_chaserVelocity = SVector2d.Zero;
            return RunnerPos;
        }

        if (chaserSpeed <= 0)
        {
            //chaser not moving
            Debug.Log("Interception immpossible: chaser no velocity");
            return RunnerPos;
        }
        Vector2 dirtoRunner = ChaserPos - RunnerPos;
        float disttoRunner = dirtoRunner.magnitude;
        float runnerSpeed = RunnerVel.magnitude;


        //check runner moving
        if (runnerSpeed == 0)
        {
            //how fast can chaser get to runner pos
            tintersept = disttoRunner / chaserSpeed;
            intercept = RunnerPos;
            Debug.Log("Time to intercept" + tintersept);
        }
        else
        {
                float a = chaserSpeed * chaserSpeed - runnerSpeed * runnerSpeed;
                float b = 2 * Vector2.Dot(dirtoRunner,RunnerVel); //check this
                float c = -disttoRunner * disttoRunner;
            //outs
                double t1, t2;
            if(!CMath.QuadraticSolver(a ,b,c, out t1, out t2))
            {
                Debug.Log(" no real solution, interception impossible");
                return intercept;

            }
            if (t1< 0 && t2 <0)
            {
            // time is negative
                Debug.Log(" no real solution, interception impossible: reason past");
                return intercept;
            }
            if(t1>0 && t2 > 0)
            {
            //two pos
                tintersept = (float)System.Math.Min(t1, t2);
            }
            else
            { //one is negative
                tintersept = (float)System.Math.Max(t1, t2);
            }

            intercept = (RunnerPos + RunnerVel * tintersept);


            possible = true;
            
        }
        Debug.Log("intercept was " + possible);
        return intercept;
    }
}
