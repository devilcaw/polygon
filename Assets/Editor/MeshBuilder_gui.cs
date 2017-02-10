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


	void OnSceneGUI() {
		Event e = Event.current;
		if (e.keyCode == KeyCode.Alpha3)
			Debug.Log (e.keyCode);
		/*if (Event.current.isMouse == true) {
			Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
			Debug.Log(ray.ToString());
		}*/
	}

	static MeshBuilder_gui () {
		if (sbl == true)
			EditorApplication.update = Update;
	}


	static void Update () {
		Debug.Log (1);
	}
}