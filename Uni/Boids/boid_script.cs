using UnityEngine;
using System.Collections;

public class boid_script : MonoBehaviour
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
		// size of the circle used to determine wander direction
		public float wander_circlediameter = 2;
		//max velocity
		public float maxvel = 1f;
		// max velocity wall push back
		public float max_wallvel = 1f;
		// time step for time based functions such as changing direction
		public float fishthinktime = 5f;
//comfort distance to sharks
		public float comfortdistance = 5f;
		// maximum food value of fish
		public float maxfoodvalue = 20;
		//time to remain dead
		public float deadtime = 10f;
	

		// the list of Boid neighbours
		private GameObject[] boids;
		private GameObject[] sharkboids;
		// the cohesive position
		private Vector3 cohesion_pos;
		// wander direction
		private Vector3 wander_dir;
		//Array of fears
		private float[] fears;
		//timer
		private float timer;
		//death timer
		private float deadtimer;
		//Jangle for wander direction
		private float radrndm_angle = 0;
		//number of other boids in cohesive range
		private float cohesive_number = 0;
		// cohesive force
		private Vector3 cohesive_force = new Vector3 (0f, 0f, 0f);
		//fishs real food value
		private float foodvalue;
		//fsm array
		int[,] fsmArray = new int[,]{{0,1,-1,-1,2},{0,-1,0,-1,2},{0,-1,-1,0,-1}};

		int currentState = 0;
		int val = 0;
		void Start ()
		{
				// initialise to null (this script might start while other boids are still being created)
				boids = null;
				sharkboids = null;
				fears = null;
				// create the cohesion vector
				cohesion_pos = transform.position;
				//creat direction for wander 
				wander_dir = transform.position + transform.GetComponent<Rigidbody>().velocity;
				// timer init
				timer = 0f;
				//food value creation
				foodvalue = Mathf.Max (maxfoodvalue * Random.value, maxfoodvalue / 10);

		}
	
		void FixedUpdate ()
		{
				//fsm to control fish states
				switch (fsmArray [currentState, val + 1]) {
				case 0:
						Debug.Log ("Wander");
						currentState = 0;
						GetComponent<Renderer>().material.color = Color.gray;
						break;
				case 1:
						Debug.Log ("Flee");
						currentState = 1;
						GetComponent<Renderer>().material.color = Color.white;
						break;
				case 2:
						Debug.Log ("Dead");
						currentState = 2;
						GetComponent<Renderer>().material.color = Color.black;
						break;
				default:
						break;
				}
				//when fish is dead controls actions 
				if (currentState == 2) {
						if (Time.time < deadtimer) { 
								return;
						} else {
								val = 2;
								return;
						}
				}

				///////////////////////////////////////////////////////////////////////////
				//deals with each shark determining fear and if action should be taken
				if (sharkboids == null) {
						sharkboids = GameObject.FindGameObjectsWithTag ("shark");
						if (fears == null) {
								fears = new float[sharkboids.Length];
						}
				} else {
						for (int sharkboid_index = 0; sharkboid_index < sharkboids.Length; sharkboid_index ++) {
								Vector3 sharkpos = sharkboids [sharkboid_index].transform.position;
								fears [sharkboid_index] = Mathf.Min (comfortdistance / Vector3.Distance (transform.position, sharkpos), 1);
								if (fears [sharkboid_index] == 1) {

										val = 0;
								} else {
										val = 1;
								}
								if (currentState == 1) {
										//determines predator predicated pos and adds force away from position
										Vector3 predator_predicted_pos = sharkpos + .5f * Vector3.Distance (transform.position, sharkpos) * sharkboids [sharkboid_index].GetComponent<Rigidbody>().velocity;
										GetComponent<Rigidbody>().AddForce (Vector3.ClampMagnitude (predator_predicted_pos - transform.position, 50f), ForceMode.Force);
										Debug.DrawLine (sharkpos, predator_predicted_pos, Color.white);
								}
//			}
						}
				}

				/////////////////////////////////////////////////////////////////////////////////
				// if the boids list is null
				if (boids == null) {
						// get the other boids
						boids = GameObject.FindGameObjectsWithTag ("boid");
				} else {
						// deal with the boid escape case - must stay within simulation_radius
						if (Vector3.Distance (simulation_centre, transform.position) > simulation_radius - 2) {
								if (Vector3.Distance (simulation_centre, transform.position) > simulation_radius) {
								}
								//push boids back into simulation the closer to the wall the more force is given
								float radius_scalar = Vector3.Distance (simulation_centre, transform.position) - (simulation_radius - 2) / 2;
								GetComponent<Rigidbody>().AddForce (Vector3.ClampMagnitude (simulation_centre - transform.position * radius_scalar, max_wallvel), ForceMode.Force);

						}

				}
	
				for (int boid_index = 0; boid_index < boids.Length; boid_index ++) {
						boid_script r = (boid_script)boids [boid_index].GetComponent (typeof(boid_script));
						if (r.returnstate () != 2) {
								// position of boid at index
								Vector3 pos = boids [boid_index].transform.position;
								Quaternion rot = boids [boid_index].transform.rotation;
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
						
										} //else if(dist >= cohesion_distance){
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
				//timer controls creates random angle
				if (Time.time > timer) {
						timer = Time.time + fishthinktime;
						float rndm_angle = 360 * Random.value;
						radrndm_angle = Mathf.Deg2Rad * rndm_angle;
				}
				//sets coords for wander circle
				float x = (wander_circlediameter / 2) * Mathf.Cos (radrndm_angle) + (transform.position.x + Mathf.Clamp (transform.GetComponent<Rigidbody>().velocity.x, -maxvel, maxvel));
				float y = (wander_circlediameter / 2) * Mathf.Sin (radrndm_angle) + (transform.position.y + Mathf.Clamp (transform.GetComponent<Rigidbody>().velocity.y, -maxvel, maxvel));
				float z = (wander_circlediameter / 2) * Mathf.Sin (radrndm_angle) + (transform.position.z + Mathf.Clamp (transform.GetComponent<Rigidbody>().velocity.z, -maxvel, maxvel));
				wander_dir.Set (x, (y), z);
				// adds force for wander
				GetComponent<Rigidbody>().AddForce (wander_dir - transform.position, ForceMode.Force);
				Debug.DrawLine (transform.position, wander_dir, Color.blue);
				Debug.DrawLine (transform.position, transform.position + transform.GetComponent<Rigidbody>().velocity, Color.red);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (transform.position + transform.GetComponent<Rigidbody>().velocity), 6f);
// clamps velocity of fish
				transform.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude (transform.GetComponent<Rigidbody>().velocity, maxvel);

		}
		// method to be called by shark boid when eats fish
		public float eaten ()
		{
				Debug.Log ("eaten");
				val = 3;
				deadtimer = Time.time + deadtime;
				return foodvalue;
		}
		//returns state to shark boid
		public int returnstate ()
		{
				return currentState;
		}
}
