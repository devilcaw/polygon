using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class car_inventory : MonoBehaviour {
	private GameObject player;
	private bool on_trig;
	private Character_control Ccont;
	private GameObject cam;

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
		Ccont = player.GetComponent<Character_control> ();
		cam = GameObject.FindWithTag ("MainCamera");

		vaccine.Open_vaccine.GetComponent<Button> ().onClick.AddListener (Vaccine_button);
	}
	

	void Update () {
		if ((Input.GetKeyDown (KeyCode.E)) & (on_trig == true)) {
			if (inventory_table.activeSelf) {
				
				pricel.SetActive (true);
				inventory_table.SetActive (false);
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				for ( int i = 0; i < cam.GetComponent<CamPos_Scr> ().camDots.Length - 1; i++)
					cam.GetComponent<CamPos_Scr> ().camDots [i].GetComponent<Cam_Scr> ().enabled = true;

				Ccont.can_walk = true;
			} else {
				
				pricel.SetActive (false);
				inventory_table.SetActive (true);
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				for ( int i = 0; i < cam.GetComponent<CamPos_Scr> ().camDots.Length - 1; i++)
					cam.GetComponent<CamPos_Scr> ().camDots [i].GetComponent<Cam_Scr> ().enabled = false;

				Ccont.can_walk = false;
			}
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