using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class car_inventory : MonoBehaviour {
	private GameObject player;
	private bool on_trig;

	public GameObject pricel;
	public GameObject inventory_table;

	[System.Serializable]
	public class Car_panel {
		public GameObject car;

	}

	[System.Serializable]
	public class Vaccine {
		public GameObject Open_vaccine;
	}

	public Vaccine vaccine;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		vaccine.Open_vaccine.GetComponent<Button> ().onClick.AddListener (Vaccine_button);
	}
	

	void Update () {
		if ((Input.GetKeyDown (KeyCode.E)) & (on_trig == true)) {
			if (inventory_table.activeSelf)
				inventory_table.SetActive (false);
			else
				inventory_table.SetActive (true);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player")
			on_trig = true;
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player")
			on_trig = false;
	}

	void Vaccine_button() {
		Debug.Log (1);
	}
}