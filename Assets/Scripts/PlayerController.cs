using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public Camera playerCamera;
	public float speed;
	public float gravity;
	public float jumpSpeed;
	public GUIText countText;
	public GUIText winText;

	int count;
	bool grounded;

	void Start()
	{
		count = 0;
		SetCountText();
		winText.text = "";
		grounded = false;
	}

	void FixedUpdate()
	{
		var movement = Vector3.zero;
		if (grounded) 
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			movement.Set(moveHorizontal, 0.0f, moveVertical);


			if (Input.GetButton ("Jump")) 
			{
				movement.y = jumpSpeed;
				grounded = false;
			}
		}

		movement = Quaternion.AngleAxis (playerCamera.transform.rotation.eulerAngles.y, Vector3.up) * movement;

		rigidbody.AddForce (movement * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickup") 
		{
			other.gameObject.SetActive (false);
			count++;
			SetCountText(); 
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "OutOfBounds") 
		{
			transform.position = new Vector3(0, 4, 0);
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ground") 
		{
			grounded = true;
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 4) 
		{
			winText.text = "YOU WIN";
		}
	}
}
