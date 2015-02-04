using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public GameObject player;
	public float angleDown;
	Vector3 offset;
	float rotation;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position;
		rotation = 0f;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		rotation = Input.GetAxis ("Rotate");
		offset = Quaternion.AngleAxis (rotation, Vector3.up) * offset;

		transform.position = player.transform.position + offset;
		transform.RotateAround (player.transform.position, Vector3.up, rotation);
	}
}
