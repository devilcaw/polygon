using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_camera : MonoBehaviour {
	public GameObject player;

	private Vector3 offset;
	private float X;
	private float Y;

	void Start ()
	{
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {

		X = Input.GetAxis ("Mouse X");
		Y = -Input.GetAxis ("Mouse Y");

		transform.RotateAround(player.transform.position, transform.up, X);
		transform.RotateAround(player.transform.position, transform.right, Y);
		transform.rotation = Quaternion.LookRotation (player.transform.position - transform.position);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

		//transform.position = player.transform.position + offset;

	}
}