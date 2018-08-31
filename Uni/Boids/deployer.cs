using UnityEngine;
using System.Collections;

public class deployer : MonoBehaviour
{

		// the Boid prefab transform
		public Transform fishboid;
		public Transform sharkboid;
		public int fishnumber = 20;
		public int sharknumber = 5;
		public float spawn_range = 15f;

		// initialise the boids
		void Start ()
		{
				for (int i = 0; i < fishnumber; i++) {
						Instantiate (fishboid, new Vector3 (spawn_range * 2 * Random.value - spawn_range, 1f, spawn_range * 2 * Random.value - spawn_range), Quaternion.identity);
				}
				for (int i = 0; i < sharknumber; i++) {
						Instantiate (sharkboid, new Vector3 (spawn_range * 2 * Random.value - spawn_range, 1f, spawn_range * 2 * Random.value - spawn_range), Quaternion.identity);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
