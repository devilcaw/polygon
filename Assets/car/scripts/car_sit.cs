using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_sit : MonoBehaviour {
	public Character_control Ccont;
	private bool ontrig;
	private GameObject player;
	public GameObject car;
	private bool in_car;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		Ccont = player.GetComponent<Character_control> ();
	}
	

	void Update () {
		if ((Input.GetKeyDown (KeyCode.E)) & (ontrig == true) & (in_car == false)) {
			in_car = true;
			Ccont.can_walk = false;
			Ccont.can_ride = true;
			player.transform.parent = car.transform;
		} else {
			if ((Input.GetKeyDown (KeyCode.E)) & (in_car == true)) {
				in_car = false;
				Ccont.can_walk = true;
				Ccont.can_ride = false;
				player.transform.parent = null;
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			ontrig = true;
		}
	}

	void OnTriggerExit ( Collider col) {
		if (col.gameObject.tag == "Player") {
			ontrig = false;
		}
	}
}
