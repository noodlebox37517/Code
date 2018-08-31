using UnityEngine;
using System.Collections;

public class sharkboid_script : MonoBehaviour
{

		// the global centre of the simulation
		public Vector3 simulation_centre = new Vector3 (0f, 11.0f, 0f);
		// the global radius of the simulation
		public float simulation_radius = 10f;
		// the boid maximum velocity
		// the distance where sepration is applied
		public float separation_distance = 1.0f;
		// the distance where cohesion is applied
		public float cohesion_distance = 5.0f;
		// the sepration strength
		public float separation_strength = 50.0f;
		// the distance where cohesion is applied
		public float cohesion_strength = 5.0f;
		//wander circle diameter
		public float wander_circlediameter = 2;
		//max velocity to clamp speed
		public float maxvel = 1f;
		//max wall push velocity
		public float max_wallvel = 1f;
		// time step for time based functions such as changing direction
		public float thinktime = 5f;
		//rate at which food is decayed
		public float digestionrate = 0.9f;
		//limit of how hungry shark will get before it hunts
		public float hungerlimit = .5f;
		//max food shark can hold
		public float appettite = 10f;
		//diustance which shark can eat/attack fish
		public float attackdistance = 1f;


		// the list of Boid neighbours
		private GameObject[] boids;
		private GameObject[] sharkboids;
		// the cohesive position
		private Vector3 cohesion_pos;
		//wander direction
		private Vector3 wander_dir;
		//timer to control direction changes
		private float timer;
		//random angle for wander
		private float radrndm_angle = 0;
		//number of other boids in cohesive range
		private float cohesive_number = 0;
		// cohesive force
		private Vector3 cohesive_force = new Vector3 (0f, 0f, 0f);
		// actual food
		private float fed = 10f;
		//hunger
		private float hunger = 0f;
		// how much foos shark has cuaght
		private float food = 0f;
		//gameobject of closest fish
		private GameObject closestfish = null;
		// fsm array and state variables
		int[,] fsmArray = new int[,]{{0,-1,1},{0,0,-1}};
		int currentState = 0;
		int val = 0;

		// Use this for initialization
		void Start ()
		{
				// initialise to null (this script might start while other boids are still being created)
				boids = null;
				sharkboids = null;
				// create the cohesion vector
				cohesion_pos = transform.position;
				//creat direction for wander 
				wander_dir = transform.position + transform.GetComponent<Rigidbody>().velocity;
				timer = 0f;
				fed = Mathf.Max (appettite * Random.value, appettite / 5);
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				// timer to control change in wander and digestion applications
				if (Time.time > timer) {
						timer = Time.time + thinktime;
						float rndm_angle = 360 * Random.value;
						radrndm_angle = Mathf.Deg2Rad * rndm_angle;
						fed = Mathf.Min (((fed * digestionrate) + food), appettite);
						hunger = (1f - (fed / appettite));
						food = 0;
				}
				//set wander direction
				float x = (wander_circlediameter / 2) * Mathf.Cos (radrndm_angle) + (transform.position.x + Mathf.Clamp (transform.GetComponent<Rigidbody>().velocity.x, -maxvel, maxvel));
				float y = (wander_circlediameter / 2) * Mathf.Sin (radrndm_angle) + (transform.position.y + Mathf.Clamp (transform.GetComponent<Rigidbody>().velocity.y, -maxvel, maxvel));
				float z = (wander_circlediameter / 2) * Mathf.Sin (radrndm_angle) + (transform.position.z + Mathf.Clamp (transform.GetComponent<Rigidbody>().velocity.z, -maxvel, maxvel));
				wander_dir.Set (x, (y), z);
				//////// find boids to hunt
				if (boids == null) {
						boids = GameObject.FindGameObjectsWithTag ("boid");
				}
				for (int boid_index = 0; boid_index < boids.Length; boid_index ++) {
						Vector3 fishpos = boids [boid_index].transform.position;
						if (closestfish == null) {
								closestfish = boids [boid_index];
						}
						boid_script r = (boid_script)boids [boid_index].GetComponent (typeof(boid_script));
						// if state not dead hunt
						if (r.returnstate () != 2) {
								if (Vector3.Distance (transform.position, fishpos) < Vector3.Distance (transform.position, closestfish.transform.position)) {
										closestfish = boids [boid_index];
								}
						}
				}
				// sets fsm to hunt if hungry
				if (hunger > hungerlimit) {
						val = 1;
				} else {
						val = 0;
				}

				////////////////////////// fsm
				switch (fsmArray [currentState, val + 1]) {
				case 0:
						Debug.Log ("Wander");
						currentState = 0;
						break;
				case 1:
						Debug.Log ("Hunt");
						currentState = 1;
						break;
			
				default:
						break;
				}
				// if hunting  moves towards fish and attacks
				if (currentState == 1) {
						boid_script r = (boid_script)closestfish.GetComponent (typeof(boid_script));
						if (r.returnstate () != 2) {
								Vector3 fishpredpos = closestfish.transform.position + 0.5f * Vector3.Distance (transform.position, closestfish.transform.position) * closestfish.GetComponent<Rigidbody>().velocity;
								GetComponent<Rigidbody>().AddForce (fishpredpos - transform.position, ForceMode.Force);
								Debug.DrawLine (transform.position, closestfish.transform.position, Color.magenta);
								Debug.DrawLine (closestfish.transform.position, fishpredpos, Color.cyan);

								if (Vector3.Distance (transform.position, closestfish.transform.position) < attackdistance) {
										food = r.eaten ();
								}
						}
						// otherwise apply wander force
				} else if (currentState == 0) {
						GetComponent<Rigidbody>().AddForce (wander_dir - transform.position, ForceMode.Force);
				}
				/////////////////////////////////////////////////////////////////////////////////
				// if the boids list is null
				if (sharkboids == null) {
						// get the other boids
						sharkboids = GameObject.FindGameObjectsWithTag ("shark");
				} else {
						// deal with the boid escape case - must stay within simulation_radius
						if (Vector3.Distance (simulation_centre, transform.position) > simulation_radius - 2) {
								if (Vector3.Distance (simulation_centre, transform.position) > simulation_radius) {
								}
								float radius_scalar = Vector3.Distance (simulation_centre, transform.position) - (simulation_radius - 2) / 2;
								GetComponent<Rigidbody>().AddForce (Vector3.ClampMagnitude (simulation_centre - transform.position * radius_scalar, max_wallvel), ForceMode.Force);

						}
				}
				// swarm control for shark boids
				for (int boid_index = 0; boid_index < sharkboids.Length; boid_index ++) {
						// position of boid at index
						Vector3 pos = sharkboids [boid_index].transform.position;
						Quaternion rot = sharkboids [boid_index].transform.rotation;
						// the distance
						float dist = Vector3.Distance (transform.position, pos);
				
						// if not this boid
						if (dist > 0f) {
					
								// if within separation
								if (dist <= separation_distance) {
										// compute the scale of separationt
										float scale = separation_strength / dist;
										// add a separation force between this boid and its neighbour
										GetComponent<Rigidbody>().AddForce (Vector3.Normalize (transform.position - pos) * scale, ForceMode.Force);
										//Debug.DrawLine (transform.position, transform.position + Vector3.Normalize (transform.position - pos), Color.white);
								}
								if (dist < cohesion_distance && dist > separation_distance) { // if within cohesive distance but not separation
										// compute the cohesive position
										cohesive_number++;
										cohesion_pos = cohesion_pos + pos;
										// alignment - small rotations are applied based on the alignments of the neighbours
										transform.rotation = Quaternion.RotateTowards (transform.rotation, rot, 1f);
						
								}
						}
				}
				if (cohesive_number > 1) {
						cohesion_pos = cohesion_pos * (1 / cohesive_number);
						//add cohesive force
						// compute the scale
						cohesive_force = (cohesion_strength / Vector3.Distance (cohesion_pos, transform.position)) * (cohesion_pos - transform.position);
						// apply force
						GetComponent<Rigidbody>().AddForce (cohesive_force, ForceMode.Force);
						Debug.DrawLine (transform.position, cohesion_pos, Color.yellow);
						// zero the cohesion vector
						cohesion_pos = transform.position;
						cohesive_number = 1;
				}
				// clamp magitude and rotate towards center of velocity
				transform.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude (transform.GetComponent<Rigidbody>().velocity, maxvel);
				Debug.DrawLine (transform.position, wander_dir, Color.blue);
				Debug.DrawLine (transform.position, transform.position + transform.GetComponent<Rigidbody>().velocity, Color.red);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (transform.position + transform.GetComponent<Rigidbody>().velocity), 20f);
	
		}

}
