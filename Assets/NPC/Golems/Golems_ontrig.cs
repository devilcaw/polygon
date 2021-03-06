﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golems_ontrig : MonoBehaviour {
	private Golems golem;


	void Start() {
		golem = transform.parent.GetComponent<Golems> ();
		golem.triger = gameObject;
	}

	void OnTriggerEnter(Collider col) {
		if (((golem.GolemType == GolemType.enemy) & (col.gameObject.tag == "Golem_player")) |
			(((golem.GolemType == GolemType.player) & (col.gameObject.tag == "Golem_enemy") & (golem.enemy == null)))) {
			golem.enemy = col.gameObject;
			golem.golem = col.gameObject.GetComponent<Golems> ();
			gameObject.SetActive (false);
		}
	}
	void OnTriggerExit(Collider col) {
		
	}
}
