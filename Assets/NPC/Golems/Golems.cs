using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum GolemType {
	player,
	enemy
}
public class Golems : MonoBehaviour {

	public GolemType GolemType = GolemType.enemy;

	public int lvl;
	public int health;

	private Animator animator;
	private NavMeshAgent agent;
	private GameObject player;

	public GameObject enemy;
	private  bool attack;
	private bool fight;
	public bool onTrig;
	private bool can_hit;
	private float time;
	public Golems golem;

	void Start () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		health = 100 * lvl;
	}
	

	void Update () {
		if (enemy)
			Fight ();

	}

	void Fight() {
		if ((Vector3.Distance (transform.position, enemy.transform.position) > 2) & (!attack))
			agent.SetDestination (enemy.transform.position);
		else {
			agent.SetDestination (transform.position);

			if (!attack)
				Attack ();
			else {
				if (time > 0)
					time -= Time.deltaTime;
				else if (!animator.GetBool ("attack"))
					attack = false;
			}
		}
	}

	void Attack() {
		animator.SetBool ("attack", true);
		attack = true;

		transform.LookAt (enemy.transform.position);

	

		time = 2;
	}

	void OnTriggerEnter(Collider col) {
		if (GolemType == GolemType.player) {
			if (col.gameObject.tag == "Golem_enemy")
				can_hit = true;
		} else if (GolemType == GolemType.enemy) {
			if (col.gameObject.tag == "Golem_player")
				can_hit = true;
		}
	}
	void OnTriggerExit(Collider col) {
		if (GolemType == GolemType.player) {
			if (col.gameObject.tag == "Golem_enemy")
				can_hit = false;
		} else if (GolemType == GolemType.enemy) {
			if (col.gameObject.tag == "Golem_player")
				can_hit = false;
		}
	}

	void Move() {
		if ((Vector3.Distance (transform.position, player.transform.position) > 2) & (!fight))
			agent.SetDestination (player.transform.position);
		else
			agent.SetDestination (transform.position);
	}

	public void AttackEnd() {
		animator.SetBool ("attack", false);
		if (can_hit)
			golem.health -= lvl;
	}
}
