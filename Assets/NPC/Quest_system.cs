using System.Collections;
using System.Collections.Generic;
using UnityEngine;



internal enum Reputation {
	Civil

}
internal enum Quest_type {
	Killer,
	Item
}
public class Quest_system : MonoBehaviour {
	[SerializeField]
	private Quest_type quest_type = Quest_type.Killer;
	[SerializeField]
	private Reputation[] reputation; // give reputation 

	private bool Ruined; // ruined quest
	private bool On_trig;

	[System.Serializable]
	public class Quest_box	{
		public bool passed;
		public GameObject Quest_npc; 
		public GameObject Quest_item; //item for quest

	}
		
	public int Prize;

	public Quest_box[] Stage;
	private bool quest_active;


	void Start () {
		for (int i = 0; i < Stage.Length; i++) {
			Stage [i].Quest_npc.GetComponent<Quest_system_npc_role> ().Stage = i;
			Stage [i].Quest_npc.GetComponent<Quest_system_npc_role> ().quest = GetComponent<Quest_system>();
		}
	}
	

	void Update () {
		if ((On_trig == true) & (Input.GetButtonDown ("Use")))
			quest_active = true;
		
		if (quest_active == true) {
			if (quest_type == Quest_type.Killer) {
				if (Stage [Stage.Length - 1].passed == true) {
					Debug.Log ("WIN");
				}
			}

			if (quest_type == Quest_type.Item) {
				
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player")
			On_trig = true;	
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player")
			On_trig = false;
	}
}
