using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPC_system))]
[CanEditMultipleObjects]
public class NPC_system_gui : Editor {
	public int h_slider;
	private int h_slider_c = 1;
	public int damage;

	void OnEnable() {
		var npc_sys = target as NPC_system;
		damage = npc_sys.damage;
	}
	
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();
		serializedObject.Update ();

		var npc_sys = target as NPC_system;

		if (GUILayout.Button ("Button"))
			Debug.Log (1);
		
		h_slider = EditorGUILayout.IntSlider (h_slider, 1, 10);
		ProgressBar (h_slider, "Slider");

		damage = EditorGUILayout.IntSlider (damage, 1, 10);

		if (h_slider == 4)
			npc_sys.Player_group.player.SetActive (false);
		else
			npc_sys.Player_group.player.SetActive (true);
	}

	void ProgressBar (float value, string label) {
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}

	void OnDisable() {
		var npc_sys = target as NPC_system;
		npc_sys.damage = damage;
	}
}