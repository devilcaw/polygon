using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Accept_func {
	passed,
	ruined
}
public enum Reject_func {
	passed,
	ruined
}
public class Quest_system_npc_role : MonoBehaviour {
	public int Stage; // this stage quest;

	private bool On_trig;
	private NPC_system npc_sys;
	public Quest_system quest;

	[System.Serializable]
	public class Can_speak {
		public bool canSpeak; // player can speak with npc
		public List<string> Text = new List<string>(); // dialog
		public GameObject Quest_window; // global window;
	}

	public GameObject Item; // quest item
	public Can_speak CanSpeak;
	public bool NeedKill; // need kill this npc
	public bool NeedBackStage; // need have passed early stage

	[SerializeField]
	public Accept_func AcceptFunction = Accept_func.passed;
	[SerializeField]
	public Reject_func RejectFunction = Reject_func.passed;


	void Start () {
		npc_sys = GetComponent<NPC_system> ();

		if (quest == null)
		while (quest == null) { // need when quest give parametr in void start
			
		}

		if (CanSpeak.canSpeak == true)
			for (int i = 0; i < quest.Stage [Stage].Text.Length; i++)
				CanSpeak.Text.Add (quest.Stage [Stage].Text [i]);
	}


	void Update () {

		if ((On_trig) & (Input.GetButtonDown ("Use")) & (quest.Quest_window.activeSelf == false) & (CanSpeak.canSpeak) & (quest.quest_active) & (!quest.Stage[Stage].passed)) {
			Dialog_window dialog_window = quest.Quest_window.GetComponent<Dialog_window> ();
			dialog_window.call_obj = gameObject;
			quest.Quest_window.SetActive (true);

			for (int i = 0; i < CanSpeak.Text.Count; i++)
				dialog_window.Dialog_text.GetComponent<Text> ().text += CanSpeak.Text [i].ToString ();
		}

		if (((NeedBackStage == true) & (quest.Stage [Stage - 1].passed == true)) | (NeedBackStage == false)) {
			
			if (NeedKill == true) {
				
				if ((npc_sys.fight.health == 0) & (quest.Stage [Stage].passed != true)) {
					quest.Stage [Stage].passed = true;
					GameObject obj = Instantiate<GameObject> (Item, transform.position + Vector3.up, Quaternion.identity);
					obj.AddComponent<Quest_system_item> ();
					Quest_system_item obj_item = obj.GetComponent<Quest_system_item> ();
					obj_item.Stage = Stage;
					obj_item.quest = quest;
					obj_item.str_c = Item.name.Length;
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
