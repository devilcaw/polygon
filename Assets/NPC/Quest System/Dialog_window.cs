﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog_window : MonoBehaviour {
	public GameObject call_obj;

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
		OnDisble ();
	}
	void AcceptButton() {
		Component[] components = call_obj.GetComponents<Component> ();
		foreach (var script in components) {
			
			if ((script as Quest_system) != null) {
				Quest_system quest = call_obj.GetComponent<Quest_system> ();
				quest.quest_active = true;
				quest.Stage [0].passed = true;
			}
		}

		OnDisble ();
	}
	void RejectButton() {
		if (call_obj.GetComponent<Quest_system> () != null)
			call_obj.GetComponent<Quest_system> ().Ruined = true;
		
		OnDisble ();
	}
	void Scroll() {
		Dialog_text.transform.position = new Vector2 (Dialog_text.transform.position.x, pos.y + Dialog_scrollbar.GetComponent<Scrollbar> ().value * 1000);
	}

	void OnEnable() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	void OnDisble() {
		Dialog_text.GetComponent<Text> ().text = null;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		gameObject.SetActive (false);
	}
}