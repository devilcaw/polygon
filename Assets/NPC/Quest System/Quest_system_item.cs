using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_system_item : MonoBehaviour {
	public int Stage; // this stage quest;

	private bool On_trig;
	public Quest_system quest;
	public int str_c;


	void Start () {
		
	}
	

	void Update () {
		if ((On_trig == true) & (Input.GetButtonDown ("Use"))) {
			quest.Stage [Stage].Quest_item = Resources.Load(gameObject.name.Remove(str_c)) as GameObject;

			if (quest.quest_type == Quest_type.Item)
				quest.Stage [Stage].passed = true;

			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log ("item_raycast");
		if (col.gameObject.tag == "Player")
			On_trig = true;	
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player")
			On_trig = false;
	}
}
