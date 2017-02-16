using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest_system_npc_role : MonoBehaviour {
	public int Stage; // this stage quest;

	private bool On_trig;
	private NPC_system npc_sys;
	public Quest_system quest;
	private int c;

	public class Can_speak {
		public bool canSpeak; // player can speak with npc
		public string[] Text; // dialog
	}

	public GameObject Item; // quest item
	public Can_speak CanSpeak;
	public bool NeedKill; // need kill this npc
	public bool NeedBackStage; // need have passed early stage

	void Start () {
		npc_sys = GetComponent<NPC_system> ();

		if (quest == null)
		while (quest == null) {
			
		}
		if (quest.Stage.Length != 1)
			c = 1;
	}


	void Update () {

		if (((NeedBackStage == true) & (quest.Stage [Stage - c].passed == true)) | (NeedBackStage == false)) {
			
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
