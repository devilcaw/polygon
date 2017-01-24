using UnityEngine;
using System.Collections;

public class Character_control : MonoBehaviour {
	
	private float speed;
	public CharacterController controller;
	public Transform pricel;
	public Animator animator;
	public bool celim;
	public Shoot shoot;
	public bool dev_mode;
	public float pushPower;
	public bool aprm;

	[Header("Speed")]
	public float walk_s;
	public float run_s;
	public float crouch_s;

	[Header("Ability")]
	private bool can_shoot;
	public bool can_walk;
	public bool can_ride;

	void Start() {
		animator = GetComponent<Animator> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		can_walk = true;
	}

	void Grab_end() {
		animator.SetBool ("grab", false);
	}

	void FixedUpdate () {
		if (can_walk == true) {
			controller.enabled = true;
			if (Input.GetKey (KeyCode.Q) & (Input.GetKey (KeyCode.W))) {
				animator.SetBool ("run", true);
				speed = run_s;
				can_shoot = false;

			} else {
				can_shoot = true;
				animator.SetBool ("run", false);
				if (Input.GetKey (KeyCode.C)) {
					speed = crouch_s;
					animator.SetBool ("crouch", true);
					controller.height = 1.5f;
					controller.center = new Vector3 (0, 0.85f, 0);
					can_shoot = false;
					if (Input.GetKey (KeyCode.W))
						animator.SetBool ("crouch_walk", true);
					else
						animator.SetBool ("crouch_walk", false);
				} else {
					speed = walk_s;
					animator.SetBool ("crouch_walk", false);
					animator.SetBool ("crouch", false);
					controller.height = 1.8f;
					controller.center = new Vector3 (0, 1, 0);
					can_shoot = true;
				}
			}

			if (Input.GetKey (KeyCode.Q) & (Input.GetKey (KeyCode.W))) {
				if ((animator.GetBool ("run_right") == true) & (animator.GetBool ("walk_right") == true))
					animator.SetBool ("walk_right", false);
				if ((animator.GetBool ("run_left") == true) & (animator.GetBool ("walk_left") == true))
					animator.SetBool ("walk_left", false);
				if ((animator.GetBool ("run_right") == true) & (animator.GetBool ("right") == true))
					animator.SetBool ("right", false);
				if ((animator.GetBool ("run_left") == true) & (animator.GetBool ("left") == true))
					animator.SetBool ("left", false);
			
				if (Input.GetKey (KeyCode.D))
					animator.SetBool ("run_right", true);
				else
					animator.SetBool ("run_right", false);

				if (Input.GetKey (KeyCode.A))
					animator.SetBool ("run_left", true);
				else
					animator.SetBool ("run_left", false);
			} else if (Input.GetKey (KeyCode.C)) {

			} else if (Input.GetKey (KeyCode.W)) {
				if ((animator.GetBool ("walk_right") == true) & (animator.GetBool ("run_right") == true))
					animator.SetBool ("run_right", false);
				if ((animator.GetBool ("walk_left") == true) & (animator.GetBool ("run_left") == true))
					animator.SetBool ("run_left", false);
				if ((animator.GetBool ("walk_right") == true) & (animator.GetBool ("right") == true))
					animator.SetBool ("right", false);
				if ((animator.GetBool ("walk_left") == true) & (animator.GetBool ("left") == true))
					animator.SetBool ("left", false);
			
				if (Input.GetKey (KeyCode.D))
					animator.SetBool ("walk_right", true);
				else
					animator.SetBool ("walk_right", false);
				if (Input.GetKey (KeyCode.A))
					animator.SetBool ("walk_left", true);
				else
					animator.SetBool ("walk_left", false);
			} else {
				animator.SetBool ("run_right", false);
				animator.SetBool ("run_left", false);
				if ((animator.GetBool ("right") == true) & (animator.GetBool ("walk_right") == true))
					animator.SetBool ("walk_right", false);
				if ((animator.GetBool ("left") == true) & (animator.GetBool ("walk_left") == true))
					animator.SetBool ("walk_left", false);
			
				if (Input.GetKey (KeyCode.D))
					animator.SetBool ("right", true);
				else {
					animator.SetBool ("right", false);
					animator.SetBool ("walk_right", false);
				}
				if (Input.GetKey (KeyCode.A))
					animator.SetBool ("left", true);
				else {
					animator.SetBool ("left", false);
					animator.SetBool ("walk_left", false);
				}
			}

			if ((Input.GetMouseButton (1) & (shoot.have_weapon == true) & can_shoot == true) | (dev_mode == true)) {
				animator.SetBool ("pistol_have", true);
				celim = true;
				//Vector3 rot = transform.eulerAngles;
				transform.forward = Vector3.Slerp (transform.forward, Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1)), 1f);
				//transform.LookAt (pricel.position);
				//float bodyY = Mathf.LerpAngle(rot.y, transform.eulerAngles.y, Time.deltaTime * 5);
				//transform.eulerAngles = new Vector3(rot.x, bodyY, rot.z);
			} else {
				animator.SetBool ("pistol_have", false);
				celim = false;
			}
			float x = Input.GetAxis ("Horizontal");
			float z = Input.GetAxis ("Vertical");

			if (x != 0) {
				Vector3 dir = transform.TransformDirection (new Vector3 (x * speed * Time.deltaTime, 0f, 0f));
				//transform.forward = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1));
				controller.Move (dir);
			}
			if (z != 0) {
				Vector3 dir = transform.TransformDirection (new Vector3 (0f, 0f, z * speed * Time.deltaTime));
				transform.forward = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1));
				controller.Move (dir);
				animator.SetBool ("walk", true);

			} else {
				animator.SetBool ("walk", false);

			}

			controller.Move (Physics.gravity * Time.deltaTime);
		} else {
			controller.enabled = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void OnAnimatorIK(int layerIndex) {
		if (celim == true) {
			animator.SetLookAtWeight (0.5f, bodyWeight: 1f);
			animator.SetLookAtPosition (pricel.position);
		}
	} 
	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;

		if (hit.moveDirection.y < -0.3F)
			return;

		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.AddForce( pushDir  * pushPower, ForceMode.Impulse);
	}
}