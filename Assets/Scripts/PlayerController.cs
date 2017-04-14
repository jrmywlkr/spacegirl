using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{


	public float speed;
	private float currentMoveSpeed;
	// Use this for initialization



	void FixedUpdate () 


	{
		

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		if (movement.sqrMagnitude > 0.5f)
			movement.Normalize();

		movement.Normalize();
		GetComponent<Rigidbody>().velocity = movement * Time.deltaTime * speed;



	}
		
}


