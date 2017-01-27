using UnityEngine;
using System.Collections;

public class Cam_Scr : MonoBehaviour {

    [Header("Цель камеры")]
    public Transform target;
    [Header("Чувствительность")]
    public float sensitivity = 3f;
    [Header("Минимум по высоте")]
    public float minLimit = 80f;
    [Header("Максимум по высоте")]
    public float maxLimit = 80f;
    [Header("Минимум по приближению камеры")]
    public float minZ;
    [Header("Максимум по приближению камеры")]
    public float maxZ;

    float Y;
	float X;
    Vector3 offset;

	void Start() {
		
	}
	void LateUpdate ()
	{
        offset.z += Input.GetAxis("Mouse ScrollWheel") * 3f;
        offset.z = Mathf.Clamp(offset.z, maxZ, minZ);

		X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

		Y += Input.GetAxis("Mouse Y") * sensitivity;
        Y = Mathf.Clamp(Y, -maxLimit, minLimit);
		transform.localEulerAngles = new Vector3 (-Y, X, 0);
        Vector3 position = transform.localRotation * offset + target.position;
		transform.position = Vector3.Slerp (transform.position, position, 0.4f);
    }
}
