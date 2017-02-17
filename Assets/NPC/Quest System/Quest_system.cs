using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private bool On_trig;

	[System.Serializable]
	public class Quest_box	{
		public bool passed;
		public GameObject Quest_npc; 
		public GameObject Quest_item; //item for quest
		public string[] Text; // Quest description
	}
	public List<string> Text = new List<string>(); // dialog

	public GameObject Quest_window; // global window;


	public int Prize;
	public Quest_box[] Stage;
	[HideInInspector]
	public bool quest_active;
	[HideInInspector]
	public bool Ruined; // ruined quest
	

	void Start () {
		for (int i = 0; i < Stage.Length; i++) {
			if (Stage [i].Quest_npc != null) {
				Stage [i].Quest_npc.GetComponent<Quest_system_npc_role> ().Stage = i;
				Stage [i].Quest_npc.GetComponent<Quest_system_npc_role> ().quest = GetComponent<Quest_system> ();
				Stage [i].Quest_npc.GetComponent<Quest_system_npc_role> ().CanSpeak.Quest_window = Quest_window;
			}
		}
	}
	

	void Update () {
		if ((On_trig == true) & (Input.GetButtonDown ("Use")) & (Quest_window.activeSelf == false)) {
			
			Quest_window.GetComponent<Dialog_window> ().quest = GetComponent<Quest_system> ();
			Quest_window.SetActive (true);
			for (int j = 0; j < Stage.Length - 1; j++) {
				if (Stage [j].passed == true) {
					Stage[j].Quest_npc.GetComponent<Quest_system_npc_role>().CanSpeak.Text = null;
					for (int i = 0; i < Stage [j].Text.Length-1; i++)
						Stage [j].Quest_npc.GetComponent<Quest_system_npc_role> ().CanSpeak.Text.Add (Stage [j + 1].Text [i]);
				}
			}
		}

		if (quest_active == true) {
			if (quest_type == Quest_type.Killer) {
				if (Stage [Stage.Length - 1].passed == true) {
					Debug.Log ("WIN");
				}
			}

			if (quest_type == Quest_type.Item) {
				if (Stage [Stage.Length - 1].Quest_item != null) {
					Debug.Log ("WIN");
				}
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