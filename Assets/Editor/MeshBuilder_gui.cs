using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(MeshBuilder))]
public class MeshBuilder_gui : Editor {
	public bool bl;
	static bool sbl;
	public GameObject point_mesh;

	void OnEnable() {
		var meshbuilder = target  as MeshBuilder;

		point_mesh = meshbuilder.point_mesh;
	}

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
		if (e.keyCode == KeyCode.F) {
			RaycastHit hit;
			Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000, 1))
				CreatePoint (hit.point);
		
		}
	}

	void CreatePoint(Vector3 point_pos) {
		var meshbuilder = target  as MeshBuilder;

		GameObject obj = Instantiate(point_mesh);
		obj.transform.position = point_pos;
		obj.transform.SetParent(meshbuilder.gameObject.transform);
	}

	static MeshBuilder_gui () {
		if (sbl == true)
			EditorApplication.update = Update;
	}


	static void Update () {
		Debug.Log (1);
	}
}