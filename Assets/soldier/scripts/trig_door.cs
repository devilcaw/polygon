using UnityEngine;
using System.Collections;

public class trig_door : MonoBehaviour {
	public door_system ds;
	public bool col_forward;
	public bool col_back;

	void Start () {
	
	}
	

	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			ds.ex = false;
			ds.on_trig = true;
			if (col_forward == true)
				ds.col_forward = true;
			else if (col_back == true)
				ds.col_back = true;
		}
	}
	void OnTriggerExit() {
		ds.ex = true;
		ds.on_trig = false;
		ds.col_forward = false;
		ds.col_back = false;
	}
}