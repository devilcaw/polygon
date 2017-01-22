using UnityEngine;
using System.Collections;

public class CamPos_Scr : MonoBehaviour {

	public GameObject[] camDots;
    public int camNum;
    public float speed;
	public Character_control Ccont;
	//public GameObject cam_pricel;

    void FixedUpdate() {
		if (Ccont.can_walk == true) {
			if (Input.GetMouseButton (1) & (Ccont.celim == true)) {
				camNum = 1;
				transform.position = camDots [camNum].transform.position;
			} else {
				camNum = 0;
				transform.position = camDots [camNum].transform.position + Vector3.up * 1.5f;
			}
		}
		if (Ccont.can_ride == true) {
			camNum = 2;
			transform.position = camDots [camNum].transform.position + Vector3.up * 1.5f;
		}
			//transform.position = camDots [camNum].transform.position;
			//transform.position = Vector3.Lerp(transform.position, camDots[camNum].transform.position, Time.deltaTime * speed);
			float rotX = Mathf.LerpAngle (transform.eulerAngles.x, camDots [camNum].transform.eulerAngles.x, Time.deltaTime * speed);
			float rotY = Mathf.LerpAngle (transform.eulerAngles.y, camDots [camNum].transform.eulerAngles.y, Time.deltaTime * speed);
			float rotZ = Mathf.LerpAngle (transform.eulerAngles.z, camDots [camNum].transform.eulerAngles.z, Time.deltaTime * speed);
			transform.eulerAngles = new Vector3 (rotX, rotY, rotZ);

		
	}
}
