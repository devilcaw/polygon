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
	public Transform guard;
	public int lvl;
	public int health;

	private Animator animator;
	private NavMeshAgent agent;
	private GameObject player;

	[HideInInspector]
	public GameObject enemy;
	private  bool attack;
	private bool fight;
	[HideInInspector]
	public bool onTrig;
	private bool can_hit;
	private float time;
	[HideInInspector]
	public Golems golem;

	public Collider[] ragdoll;


	void Start () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		health = 100 * lvl;

		for (int i = 0; i < ragdoll.Length; i++) {
			ragdoll [i].gameObject.GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.Interpolate;
			ragdoll [i].gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			ragdoll [i].isTrigger = true;
		}
	}
	

	void Update () {
		if (health > 0) {
			if (enemy)
				Fight ();
			else if (GolemType == GolemType.player) {
				if (guard == null)
					Move (player.transform);
				else if (guard != null)
					Move (guard);
			} else if (GolemType == GolemType.enemy) {
				if (guard == null)
					enemy = player;
				else if (guard != null)
					Move (guard);	
			}
		} else if (health <= 0)
			Death ();
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
		if (golem != null)
		if (golem.health <= 0) {
			enemy = null;
			golem = null;
		}
	}

	void Attack() {
		animator.SetBool ("attack", true);
		attack = true;

		transform.LookAt (enemy.transform.position);

	

		time = Random.Range (1f, 5f);
	}

	void OnTriggerEnter(Collider col) {
		if (GolemType == GolemType.player) {
			if (col.gameObject.tag == "Golem_enemy")
				Debug.Log (1);
				can_hit = true;
		} else if (GolemType == GolemType.enemy) {
			if (col.gameObject.tag == "Golem_player")
				can_hit = true;
			else if (col.gameObject.tag == "Player")
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
			else if (col.gameObject.tag == "Player")
				can_hit = false;
		}
	}

	void Move(Transform target) {
		if ((Vector3.Distance (transform.position, target.transform.position) > 2) & (!fight))
			agent.SetDestination (target.transform.position);
		else
			agent.SetDestination (transform.position);
	}

	public void AttackEnd() {
		animator.SetBool ("attack", false);
		if (can_hit) {
			if (enemy.gameObject.tag != "Player")
				golem.health -= new System.Random ().Next (1, lvl + 1);
			else
				Debug.Log ("hit player");
		}
	}
	void Death() {
		animator.enabled = false;
		GetComponent<CapsuleCollider> ().enabled = false;
		GetComponent<NavMeshAgent> ().enabled = false;

		for (int i = 0; i < ragdoll.Length; i++) {
			ragdoll [i].isTrigger = false;
			ragdoll [i].gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		}
	}
}