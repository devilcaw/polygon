using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golems_player : MonoBehaviour {

	public int lvl;
	public int health;

	private Animator animator;
	private NavMeshAgent agent;
	private GameObject player;

	private GameObject enemy;
	private  bool attack;
	private bool fight;
	private bool onTrig;
	private bool can_hit;
	private float time;


	void Start () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		health = 100 * lvl;
	}
	

	void Update () {
		Fight ();

	}

	void Fight() {
		if ((Vector3.Distance (transform.position, player.transform.position) > 2) & (!attack))
			agent.SetDestination (player.transform.position);
		else {
			agent.SetDestination (transform.position);

			if (!attack)
				Attack ();
			else {
				if (time > 0)
					time -= Time.deltaTime;
				else if (!animator.GetBool("attack"))
					attack = false;
			}
		}
	}

	void Attack() {
		animator.SetBool ("attack", true);
		attack = true;

		transform.LookAt (player.transform.position);

		if (can_hit)
			Debug.Log (can_hit);

		time = 2;
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Golem_enemy")
			can_hit = true;
	}
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Golem_enemy")
			can_hit = false;
	}

	void Move() {
		if ((Vector3.Distance (transform.position, player.transform.position) > 2) & (!fight))
			agent.SetDestination (player.transform.position);
		else
			agent.SetDestination (transform.position);
	}

	public void AttackEnd() {
		animator.SetBool ("attack", false);
	}
}
