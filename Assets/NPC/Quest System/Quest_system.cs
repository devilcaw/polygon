using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Reputation {
	Civil,
	test
}
public enum Quest_type {
	Killer,
	Item
}
public class Quest_system : MonoBehaviour {
	
	public Quest_type quest_type = Quest_type.Killer;

	[System.Serializable]
	public class Rep_prize {
		public Reputation reputation; // give reputation
		public int rep_value;
	}

	private bool On_trig;

	[System.Serializable]
	public class Quest_box	{
		public bool passed;
		public GameObject Quest_npc; 
		public GameObject Quest_item; //item for quest
		public GameObject item_have;
		public string[] Text; // Quest description
	}

	public GameObject Quest_window; // global window;


	public int GoldPrize;
	public Rep_prize[] RepPrize; // how many give reputation 

	public Quest_box[] Stage;
	[HideInInspector]
	public bool quest_active;
	[HideInInspector]
	public GameObject mark;
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

		
		mark = Instantiate<GameObject> (Resources.Load<GameObject> ("Sprites/exclamation"));
		mark.transform.position = transform.position + Vector3.up * 2.5f;
		mark.transform.parent = transform;
	}
	

	void Update () {
		if ((Stage [Stage.Length - 1].passed) | (Ruined))
			QuestEnd ();

		if ((On_trig) & (Input.GetButtonDown ("Use")) & (!Quest_window.activeSelf) & (!Stage [Stage.Length - 1].passed)) {
			Dialog_window dialog_window = Quest_window.GetComponent<Dialog_window> ();
			dialog_window.call_obj = gameObject;
			Quest_window.SetActive (true);

			if (Stage [0].passed == false)
				for (int i = 0; i < Stage [0].Text.Length; i++)
					dialog_window.Dialog_text.GetComponent<Text> ().text += Stage [0].Text [i].ToString ();
			else if (Stage [Stage.Length - 2].passed == true) {
				for (int i = 0; i < Stage [Stage.Length - 1].Text.Length; i++)
					dialog_window.Dialog_text.GetComponent<Text> ().text += Stage [Stage.Length - 1].Text [i].ToString ();
				
			}
		}

		if (quest_active == true) {
			if (quest_type == Quest_type.Killer) {
				if (Stage [Stage.Length - 1].passed == true) {
					Debug.Log ("WIN");
				}
			}

			if (quest_type == Quest_type.Item) {
				if (Stage [Stage.Length - 1].item_have != null) {
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
	public void QuestMark() {
		if (quest_active)
			mark.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/question");
	}

	void QuestEnd() {
		//give prize and reputation

		for (int i = 0; i < Stage.Length; i++) {
			if (Stage [i].Quest_npc != null) {
				Destroy (Stage [i].Quest_npc.GetComponent<Quest_system_npc_role> ());

				if ((Stage [i].Quest_npc.GetComponent<Quest_system_item> () as Quest_system_item) != null)
					Destroy (Stage [i].Quest_npc.GetComponent<Quest_system_item> ());
			}
		}

		Destroy (mark);
		Destroy (this);
	}
}