using UnityEngine;
using System;
using System.Collections;
using UnityEngine.AI;

internal enum Npc_type {
	npc_type_military,
	npc_type_raider,
	npc_type_rebel,
	npc_type_civil
}

[RequireComponent(typeof (NavMeshAgent))]

public class NPC_system : MonoBehaviour {

	[SerializeField] private Npc_type npc_type = Npc_type.npc_type_civil;

	[Header("Player")]
	public GameObject player;
	public Transform look_track;
	public Transform player_look_track;

	[Header("Look distantion")]
	public float look_dist;
	public float look_ugolV;

	[Header("Move")]
	private System.Random rand = new System.Random();
	public NavMeshAgent nagent;
	public bool spalil;

	[Header("Target places")]
	private int place_number;
	public int place_count;
	public Transform[] target_place;

	void Start () {
		
		gameObject.AddComponent<NavMeshAgent> ();
		nagent = GetComponent<NavMeshAgent> ();
		nagent.height = 1.8f;
		nagent.baseOffset = -0.085f;

		place_count = target_place.Length;
		place_number = rand.Next (0, place_count);
	}
	

	void FixedUpdate () {

		if ((transform.position.x == target_place[place_number].position.x) &(transform.position.z == target_place[place_number].position.z)) {
			place_number = rand.Next (0, place_count);
		}
		nagent.SetDestination (target_place [place_number].position);

		/*
		if (spalil == true) {
			nagent.SetDestination (player.transform.position);
		}

		if ((spalil == true) & (Vector3.Distance (transform.position, player.transform.position) <= 2.5f)) {
			player.GetComponent<Character_control>().enabled = false;
		}
		Debug.DrawRay (look_track.position + Vector3.up, player_look_track.transform.position - look_track.position - Vector3.up, Color.red);
		RaycastHit hit;
		Ray rays = new Ray (look_track.position + Vector3.up, player_look_track.transform.position - look_track.position - Vector3.up);
		Quaternion look = Quaternion.LookRotation (player_look_track.transform.position - transform.position);
		float ugol = Quaternion.Angle (transform.rotation, look);

		if (ugol < look_ugolV) {
			if (Physics.Raycast (rays, out hit, look_dist)) {
				//Debug.Log (hit.transform.gameObject.name);
				if (hit.transform.gameObject.tag == "Player") {
					spalil = true;
					Vector3 rot = transform.eulerAngles;
					look_track.LookAt(player_look_track.transform.position);
					float bodyY = Mathf.LerpAngle(rot.y, transform.eulerAngles.y, Time.deltaTime * 5);
					transform.eulerAngles = new Vector3(rot.x, bodyY, rot.z);

				}
			}
		} */
	}
}