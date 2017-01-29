using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPC_system))]
public class NPC_system_gui : Editor {
	public int h_slider;

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		var npc_sys = target as NPC_system;

		if (GUILayout.Button ("Button"))
			Debug.Log (1);
		
		h_slider = EditorGUILayout.IntSlider (h_slider, 1, 10);

		if (h_slider == 4)
			npc_sys.Player_group.player.SetActive (false);
		else
			npc_sys.Player_group.player.SetActive (true);
	}
}