using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golems_ontrig : MonoBehaviour {
	private Golems golem;


	void Start() {
		golem = transform.parent.GetComponent<Golems> ();
	}

	void OnTriggerEnter(Collider col) {
		if (((golem.GolemType == GolemType.enemy) & (col.gameObject.tag == "Golem_player") & (golem.enemy == null)) |
			(((golem.GolemType == GolemType.player) & (col.gameObject.tag == "Golem_enemy") & (golem.enemy == null)))) {
			golem.enemy = col.gameObject;
			golem.golem = col.gameObject.GetComponent<Golems> ();
		}
	}
	void OnTriggerExit(Collider col) {
		
	}
}
