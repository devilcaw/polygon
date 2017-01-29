using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.Interpolate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
