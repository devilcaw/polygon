using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class car_hit : MonoBehaviour {
	private GameObject npc;

	void Start () {
		
	}
	

	void Update () {
		
	}


	void OnTriggerEnter(Collider col) {

		if ((col.transform.tag == "NPC_child") | (col.transform.tag == "NPC"))
			npc = col.transform.gameObject;
		if (npc != null) {
			while (npc.transform.parent != null) {
				npc = npc.transform.parent.gameObject;
			}
			if (npc.tag == "NPC") {
				npc.GetComponent<NPC_system> ().fight.damage_get = true;
				npc.GetComponent<NPC_system> ().fight.health -= 1;
				if (npc.GetComponent<NPC_system> ().fight.health == 0) {
					npc.GetComponent<NPC_system> ().enabled = false;
					npc.GetComponent<NavMeshAgent> ().enabled = false;
					npc.GetComponent<Animator> ().enabled = false;
					for (int i = 0; i < npc.GetComponent<NPC_system> ().Npc_body.ragdoll.Length; i++) {
						npc.GetComponent<NPC_system> ().Npc_body.ragdoll [i].isTrigger = false;
						npc.GetComponent<NPC_system> ().Npc_body.ragdoll [i].gameObject.GetComponent<Rigidbody> ().useGravity = true;
						npc.GetComponent<CapsuleCollider> ().enabled = false;
					}
				}
			}
		}
	}
}
