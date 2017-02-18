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
				Quest_system_npc_role quest_npc_role = Stage [i].Quest_npc.GetComponent<Quest_system_npc_role> ();
				quest_npc_role.Stage = i;
				quest_npc_role.quest = GetComponent<Quest_system> ();
				quest_npc_role.CanSpeak.Quest_window = Quest_window;
			}
		}

		for (int i = 0; i < Stage [0].Text.Length; i++)
			Text.Add (Stage [0].Text [i]);

	}
	

	void Update () {
		if ((On_trig == true) & (Input.GetButtonDown ("Use")) & (Quest_window.activeSelf == false)) {
			Dialog_window dialog_window = Quest_window.GetComponent<Dialog_window> ();
			dialog_window.call_obj = gameObject;
			Quest_window.SetActive (true);

			if (Stage [0].passed == false)
				for (int i = 0; i < Text.Count; i++)
					dialog_window.Dialog_text.GetComponent<Text> ().text += Text [i].ToString ();
			else if (Stage [Stage.Length - 2].passed == true) {
				Text.Clear ();
				for (int i = 0; i < Stage [Stage.Length - 1].Text.Length; i++) {
					Debug.Log (i);
					Text.Add (Stage [Stage.Length - 1].Text [i]);
				}
				dialog_window.Dialog_text.GetComponent<Text> ().text = Text [0].ToString ();
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