﻿using UnityEngine;
using System;
using System.Collections;
using UnityEngine.AI;
using UnityEditor;

internal enum Npc_type {
	npc_type_military,
	npc_type_raider,
	npc_type_rebel,
	npc_type_civil
}

[RequireComponent(typeof (NavMeshAgent))]

public class NPC_system : MonoBehaviour {

	[SerializeField] private Npc_type npc_type = Npc_type.npc_type_civil;

	[System.Serializable]
	public class Player_class {
		[Header("Player")]
		public GameObject player;
		public Transform look_track;
		public Transform player_look_track;

		public GameObject player_car;
	}
		
	[System.Serializable]
	public class Npc_body_class {
		[Header("Ragdoll")]
		public Collider[] ragdoll;

		[Header("Cloth")]
		public SkinnedMeshRenderer Npc_skin;

		[Header("Bottoms")]
		public GameObject Npc_cloth_bottoms;

		[Tooltip("Cloth for face")]
		public GameObject[] balaklava;
		public int balaklava_n;
	}

	[System.Serializable]
	public class Look_distantion_class {
		[Header("Look distantion")]
		public float look_dist;
		public float look_ugolV;
	}

	[System.Serializable]
	public class Move_class	{
		[Header("Move")]
		public System.Random rand = new System.Random();
		public NavMeshAgent nagent;
	}

	[System.Serializable]
	public class Target_places_class {
		[Header("Target places")]
		public int place_number;
		public float now_wait;
		public int place_count;

		[System.Serializable]
		public class in_place {
		public Transform target_place;
		[Tooltip("how long wait in the N place")]
		public float place_wait;
		public String place_anim;
		}
		public in_place[] On_place;
	}

	[System.Serializable]
	public class Fight_class {
		[Header("Fight")]
		public int health;
		public bool damage_get;
		public bool spalil;
	}

	[ContextMenu ("Do Something")]
	void DoSomething () {
		Debug.Log ("Perform operation");
	}

	[HideInInspector]	
	public Animator anim;

	public Player_class Player_group;
	public Npc_body_class Npc_body;
	public Look_distantion_class Look_dist;
	public Move_class move;
	public Target_places_class T_places;
	public Fight_class fight;

	void Start () {
		for (int i = 0; i < Npc_body.ragdoll.Length; i++) {
			Npc_body.ragdoll [i].gameObject.GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.Interpolate;
			Npc_body.ragdoll [i].gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			Npc_body.ragdoll [i].isTrigger = true;
		}

		Player_group.player = GameObject.FindGameObjectWithTag ("Player");
		Player_group.player_car = GameObject.FindGameObjectWithTag ("Player_car");
		Physics.IgnoreCollision (Player_group.player_car.GetComponent<Collider>(), GetComponent<Collider> ());
		for (int i = 0; i < Player_group.player_car.GetComponent<car_control> ().backCols.Length; i++) {
			for (int j = 0; j < Npc_body.ragdoll.Length; j++) {
				Physics.IgnoreCollision (Player_group.player_car.GetComponent<car_control> ().backCols [i], Npc_body.ragdoll[j].GetComponent<Collider> ());
				Physics.IgnoreCollision (Player_group.player_car.GetComponent<car_control> ().frontCols [i], Npc_body.ragdoll[j].GetComponent<Collider> ());
			}
		}

		gameObject.AddComponent<NavMeshAgent> ();
		move.nagent = GetComponent<NavMeshAgent> ();
		move.nagent.height = 1.8f;
		move.nagent.baseOffset = -0.085f;

		T_places.place_count = T_places.On_place.Length;
		T_places.place_number = move.rand.Next (0, T_places.place_count);

		anim = GetComponent<Animator> ();
	}

	void Update () {

		if (fight.health == 0) {
			Component[] components = GetComponents<Component> ();
			for (int i = 0; i < components.Length; i++) {
				if (components [i] as Collider) {
					Collider col = components [i] as Collider;
					col.enabled = false;
				}
				if (components [i] as Animator) {
					Animator anima = components [i] as Animator;
					anima.enabled = false;
				}
				if (components [i] as Quest_system) {
					Quest_system qs = components [i] as Quest_system;
					for (int j = 0; j < qs.Stage.Length; j++)
						if (qs.Stage [j].Quest_npc != null)
							Destroy (qs.Stage [j].Quest_npc.GetComponent<Quest_system_npc_role> ());

					qs.enabled = false;
				}
				if (components[i] as NavMeshAgent) {
					NavMeshAgent navagent = components[i] as NavMeshAgent;
					navagent.enabled = false;
				}
			}
		} else {

			if ((transform.position.x == T_places.On_place [T_places.place_number].target_place.position.x) & (transform.position.z == T_places.On_place [T_places.place_number].target_place.position.z)) {
				anim.SetBool (T_places.On_place [T_places.place_number].place_anim, true);
				if (transform.rotation != T_places.On_place [T_places.place_number].target_place.rotation)
					transform.rotation = Quaternion.Slerp (transform.rotation, T_places.On_place [T_places.place_number].target_place.rotation, 0.01f);
			
				T_places.now_wait += Time.deltaTime;
				if (T_places.now_wait >= T_places.On_place [T_places.place_number].place_wait) {
					anim.SetBool (T_places.On_place [T_places.place_number].place_anim, false);
					T_places.place_number = move.rand.Next (0, T_places.place_count);
					T_places.now_wait = 0;
				}
			} 

			move.nagent.SetDestination (T_places.On_place [T_places.place_number].target_place.position);

			if (move.nagent.velocity.z != 0) {
				anim.SetBool ("walk", true);
			} else {
				anim.SetBool ("walk", false);
			}







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
}