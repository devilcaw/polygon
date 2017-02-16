using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_window : MonoBehaviour {
	public Quest_system quest;

	public GameObject Dialog_exit;
	public GameObject Dialog_accept;
	public GameObject Dialog_reject;
	public GameObject Dialog_scrollbar;
	public GameObject Dialog_text;

	private Vector2 pos;


	void Start () {
		Dialog_exit.GetComponent<Button> ().onClick.AddListener (ExitButton);
		Dialog_accept.GetComponent<Button> ().onClick.AddListener (AcceptButton);
		Dialog_reject.GetComponent<Button> ().onClick.AddListener (RejectButton);
		Dialog_scrollbar.GetComponent<Scrollbar> ().onValueChanged.AddListener (delegate {
			Scroll ();
		});

		pos = Dialog_text.transform.position;
	}


	void Update () {
		
	}


	void ExitButton() {
		Dialog_text.GetComponent<Text> ().text = null;
		gameObject.SetActive (false);
	}
	void AcceptButton() {
		quest.quest_active = true;
	}
	void RejectButton() {
		quest.Ruined = true;
	}
	void Scroll() {
		Dialog_text.transform.position = new Vector2 (Dialog_text.transform.position.x, pos.y + Dialog_scrollbar.GetComponent<Scrollbar> ().value * 1000);
	}

	void OnEnable() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	void OnDisble() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}