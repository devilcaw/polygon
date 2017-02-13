﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(MeshBuilder))]
public class MeshBuilder_gui : Editor {
	public bool bl;
	static bool sbl;
	public GameObject point_mesh;
	public GameObject go;
	public float c;


	void OnEnable() {
		var meshbuilder = target  as MeshBuilder;
	
		go = Resources.Load<GameObject> ("Prefabs/MeshBuilder/empty");
		point_mesh = Resources.Load<GameObject>("Prefabs/MeshBuilder/point_mesh");
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		var meshbuilder = target  as MeshBuilder;


		if (GUILayout.Button ("Create new mesh")) {

			GameObject obj = Instantiate<GameObject> (go);

			obj.transform.position = meshbuilder.transform.position;
			obj.transform.SetParent(meshbuilder.transform);
			
		}

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
		c += Time.deltaTime;


		if ((e.keyCode == KeyCode.F) & (c >= 1)) {
			RaycastHit hit;
			Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000, 1))
				CreatePoint (hit.point);
			c = 0;		
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