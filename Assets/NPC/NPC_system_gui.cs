using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPC_system))]
public class NPC_system_gui : Editor {
	public int h_slider;

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();
		NPC_system npc_sys;

		if (GUILayout.Button ("Button"))
			Debug.Log (1);
		
		h_slider = EditorGUILayout.IntSlider (h_slider, 1, 10);

	}
}