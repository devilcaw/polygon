using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_obj : MonoBehaviour {
	public Transform target;


	void Update () {
		transform.position = target.position;
	}
}
