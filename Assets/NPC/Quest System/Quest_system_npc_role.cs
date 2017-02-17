using System.Collections;
using System.Collections.Generic;
using UnityEngine;


internal enum Accept_func {
	passed,
	ruined
}
internal enum Reject_func {
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
	private Accept_func AcceptFunction = Accept_func.passed;
	[SerializeField]
	private Reject_func RejectFunction = Reject_func.passed;


	void Start () {
		npc_sys = GetComponent<NPC_system> ();

		if (quest == null)
		while (quest == null) { // need when quest give parametr in void start
			
		}
	}


	void Update () {

		if (((NeedBackStage == true) & (quest.Stage [Stage - 1].passed == true)) | (NeedBackStage == false)) {
			
			if (NeedKill == true) {
				
				if ((npc_sys.fight.health == 0) & (quest.Stage [Stage].passed != true)) {
					quest.Stage [Stage].passed = true;
					GameObject obj = Instantiate<GameObject> (Item, transform.position + Vector3.up, Quaternion.identity);
					obj.AddComponent<Quest_system_item> ();
					obj.GetComponent<Quest_system_item> ().Stage = Stage;
					obj.GetComponent<Quest_system_item> ().quest = quest;
					obj.GetComponent<Quest_system_item> ().str_c = Item.name.Length;
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
