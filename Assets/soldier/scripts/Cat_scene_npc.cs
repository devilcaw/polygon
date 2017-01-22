using UnityEngine;
using System.Collections;

public class Cat_scene_npc : MonoBehaviour {
	public GameObject Ccont;
	private int i;
	private bool ont;
	public Transform tr;
	private bool aprm;


	void Start () {
		
	}

	void FixedUpdate () {
		if (Ccont.GetComponent<Character_control>().aprm == true) {
			Ccont.transform.position = new Vector3 (tr.position.x, Ccont.transform.position.y, tr.position.z);
			Ccont.transform.forward = tr.forward * -5;

		}
		if ((i < 1) & (ont == true)) {
			Ccont.gameObject.GetComponent<Character_control> ().enabled = false;
			Ccont.gameObject.GetComponent<CharacterController> ().enabled = false;
			Ccont.transform.position = tr.position;
			Ccont.transform.forward = tr.forward * -5;
			i = 1;
			Ccont.gameObject.GetComponent<Character_control> ().animator.SetBool ("up", true);

		} else
			Ccont.gameObject.GetComponent<Animator> ().SetBool ("up", false);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			ont = true;
			GetComponent<Animator> ().SetBool ("up", true);

		}
	}
}
