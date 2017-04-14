using System.Collections;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour 
{
	private SkeletonAnimation skeletonAnimation;

	public float speed;
	private float currentMoveSpeed;
	// Use this for initialization

	void Start () {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		if (moveHorizontal > moveVertical) {
			skeletonAnimation.AnimationName = "Nwalk";
		} else if (moveVertical > moveHorizontal) {
			skeletonAnimation.AnimationName = "Swalk";
		} else {
			skeletonAnimation.AnimationName = "";
		}

		if (movement.sqrMagnitude > 0.5f)
			movement.Normalize();

		movement.Normalize();
		GetComponent<Rigidbody>().velocity = movement * Time.deltaTime * speed;
	}
		
}


