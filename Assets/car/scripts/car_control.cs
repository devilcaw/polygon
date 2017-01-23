using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_control : MonoBehaviour {
	public WheelCollider[] frontCols;
	public Transform[] dataFront;
	public WheelCollider[] backCols;
	public Transform[] dataBack;
	public Transform centerOfMass;
	public Character_control Ccont;

	private float vAxis;
	private float hAxis;
	private bool brakeButton;
	private	Vector3 frame_this;
	private Vector3 frame_early;

	[Header("Speed")]
	public float speedMax;
	public float speedSide;
	public float speedBreak;

	void Start () {
		GetComponent<Rigidbody> ().centerOfMass = centerOfMass.localPosition;
		Physics.gravity = new Vector3 (0f, -29.4f, 0f);
		frame_early = transform.position;
	}
	

	void FixedUpdate () {

		frame_this = transform.position;

		if (Ccont.can_ride == true) {
			vAxis = Input.GetAxis ("Vertical");
			hAxis = Input.GetAxis ("Horizontal");
			brakeButton = Input.GetButton ("Jump");
		} else {
			vAxis = 0;
			hAxis = 0;
			brakeButton = true;
		}
		backCols [0].motorTorque = -vAxis * speedMax;
		backCols [1].motorTorque = -vAxis * speedMax;
		frontCols [0].motorTorque = -vAxis * speedMax;
		frontCols [1].motorTorque = -vAxis * speedMax;

		if (brakeButton == true) {
			backCols [0].brakeTorque = speedBreak;
			backCols [1].brakeTorque = speedBreak;
			frontCols [0].brakeTorque = speedBreak;
			frontCols [1].brakeTorque = speedBreak;
			if (frame_this != frame_early)
				GetComponent<Rigidbody> ().drag = 1;

		
		} else {
			backCols [0].brakeTorque = 0;
			backCols [1].brakeTorque = 0;
			frontCols [0].brakeTorque = 0;
			frontCols [1].brakeTorque = 0;
			GetComponent<Rigidbody> ().drag = 0;
		}

		if (frame_this != frame_early)
			frame_early = frame_this;

		frontCols [0].steerAngle = hAxis * speedSide;
		frontCols [1].steerAngle = hAxis * speedSide;

		dataFront [0].Rotate (0, 0, -frontCols [0].rpm * Time.deltaTime);
		dataFront [1].Rotate (0, 0, -frontCols [1].rpm * Time.deltaTime);
		dataBack [0].Rotate (0, 0, -backCols [0].rpm * Time.deltaTime);
		dataBack [1].Rotate (0, 0, -backCols [1].rpm * Time.deltaTime);

		dataFront [0].localEulerAngles = new Vector3 (dataFront [0].localEulerAngles.x, hAxis * speedSide - 90, dataFront [0].localEulerAngles.z);
		dataFront [1].localEulerAngles = new Vector3 (dataFront [1].localEulerAngles.x, hAxis * speedSide - 90, dataFront [1].localEulerAngles.z);

	}
}