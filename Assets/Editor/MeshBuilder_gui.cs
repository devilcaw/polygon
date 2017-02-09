using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(MeshBuilder))]
public class MeshBuilder_gui : Editor {
	public bool bl;
	static bool sbl;

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		if (GUILayout.Button ("but")) { 
			if (sbl == false) {
				sbl = true;
				EditorApplication.update = Update;
			} else if (sbl == true) {
				sbl = false;
				EditorApplication.update = null;
			}
		}
	}



	static MeshBuilder_gui () {
		if (sbl == true)
			EditorApplication.update = Update;
	}


	static void Update () {
		Debug.Log (1);
	}
}