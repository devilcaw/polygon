using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(NPC_system))]
public class NPC_system_gui : Editor {
	
	public int h_slider;
	public GameObject cloth_now;
	public int balaklava_n;
	public int balaklava_c;


	void OnEnable() {
		var npc_sys = target as NPC_system;

		balaklava_n = npc_sys.Npc_body.balaklava_n;
		balaklava_c = balaklava_n;
	}
	
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();
		serializedObject.Update ();


		var npc_sys = target as NPC_system;

		if (GUILayout.Button ("Button"))
			Debug.Log (1);
		
		h_slider = EditorGUILayout.IntSlider (h_slider, 1, 10);
		ProgressBar (h_slider, "Slider");

		balaklava_n = EditorGUILayout.IntSlider ("Balaklava", balaklava_n, 0, npc_sys.Npc_body.balaklava.Length - 1);
		if (balaklava_c != balaklava_n) {
			balaklava_c = balaklava_n;

			addCloth (npc_sys.Npc_body.balaklava [balaklava_n], npc_sys.Npc_body.Npc_cloth_bottoms);
			npc_sys.Npc_body.Npc_cloth_bottoms = cloth_now;
			npc_sys.Npc_body.balaklava_n = balaklava_n;
		}
	}		

	void ProgressBar (float value, string label) {
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}


	void addCloth(GameObject cloth, GameObject cloth_old) {
		var npc_sys = target as NPC_system;

		GameObject obj = Instantiate (cloth);
		obj.transform.SetParent (npc_sys.Npc_body.Npc_skin.transform.parent);
		SkinnedMeshRenderer[] skin_mesh = obj.GetComponentsInChildren<SkinnedMeshRenderer> ();

		DestroyImmediate (cloth_old);
		cloth_now = obj;

		for (int i = 0; i < (npc_sys.Npc_body.balaklava.Length - 1); i++) {
			skin_mesh [i].bones = npc_sys.Npc_body.Npc_skin.bones;
			skin_mesh [i].rootBone = npc_sys.Npc_body.Npc_skin.rootBone;
		}
	
	}
}