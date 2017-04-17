using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

enum Direction {North, East, South, West, None};

public class MovementController : MonoBehaviour {

	public SkeletonAnimation skeletonAnimationWalkSouth;
	public SkeletonAnimation skeletonAnimationWalkNorth;
	public SkeletonAnimation skeletonAnimationWalkEastWest;
	private SkeletonAnimation currentAnimation;
	private SkeletonAnimation[] skeletonAnimations;

	public float speed;
	private float currentMoveSpeed;
	// Use this for initialization

	void Start () {
		skeletonAnimations = FindObjectsOfType<SkeletonAnimation> ();
		foreach (SkeletonAnimation skeletonAnimation in skeletonAnimations) {
			if (string.Equals(skeletonAnimation.name, "SouthWalk")) {
				skeletonAnimationWalkSouth = skeletonAnimation;
			} else if (string.Equals(skeletonAnimation.name, "NorthWalk")) {
				skeletonAnimationWalkNorth = skeletonAnimation;
			} else if (string.Equals(skeletonAnimation.name, "EastWestWalk")) {
				skeletonAnimationWalkEastWest = skeletonAnimation;
			}
		}
		currentAnimation = skeletonAnimationWalkSouth;
		skeletonAnimationWalkSouth.loop = true;
		skeletonAnimationWalkNorth.loop = true;
		skeletonAnimationWalkEastWest.loop = true;
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
//		print (moveHorizontal);
//		print (moveVertical);

		if (Mathf.Abs (moveHorizontal) > 0 || Mathf.Abs (moveVertical) > 0) {
			if (Mathf.Abs(moveHorizontal) > 0 && Mathf.Abs(moveVertical) > 0) {
				//moving diagonal
				if (moveHorizontal > 0 && moveHorizontal > 0) {
					//moving NE
				} else if (moveHorizontal > 0 && moveVertical < 0) {
					//moving SE
				} else if (moveHorizontal < 0 && moveVertical < 0) {
					//moving SW
				} else if (moveHorizontal < 0 && moveVertical > 0) {
					//moving NW
				}
			} else if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical)) {
				print (moveHorizontal);
				if (moveHorizontal > 0) {
					updateAnimationsWithMovementDirection(Direction.East);
				} else {
					updateAnimationsWithMovementDirection(Direction.West);
				}
			} else if (Mathf.Abs(moveHorizontal) < Mathf.Abs(moveVertical)) {
				if (moveVertical > 0) {
					updateAnimationsWithMovementDirection(Direction.North);
				} else {
					updateAnimationsWithMovementDirection(Direction.South);
				}
			}
		} else {
			updateAnimationsWithMovementDirection(Direction.None);
		}

		if (movement.sqrMagnitude > 0.5f)
			movement.Normalize();

		movement.Normalize();
		GetComponent<Rigidbody>().velocity = movement * Time.deltaTime * speed;
	}

	void updateAnimationsWithMovementDirection (Direction direction) {
		currentAnimation.timeScale = 1;
		switch (direction) {
		case Direction.East:
			if (skeletonAnimationWalkEastWest.transform.localScale.z < 1) {
				skeletonAnimationWalkNorth.transform.localScale = new Vector3 (0, 0, 0);
				skeletonAnimationWalkSouth.transform.localScale = new Vector3 (0, 0, 0);
				skeletonAnimationWalkEastWest.transform.localScale = new Vector3 (1, 1, 1);
			}
			skeletonAnimationWalkEastWest.AnimationName = "Ewalk";
			currentAnimation = skeletonAnimationWalkEastWest;
			print ("Ewalk");
			break;
		case Direction.West: 
			if (skeletonAnimationWalkEastWest.transform.localScale.z < 1) {
				skeletonAnimationWalkNorth.transform.localScale = new Vector3 (0, 0, 0);
				skeletonAnimationWalkSouth.transform.localScale = new Vector3 (0, 0, 0);
				skeletonAnimationWalkEastWest.transform.localScale = new Vector3 (1, 1, 1);
			}
			skeletonAnimationWalkEastWest.AnimationName = "Wwalk";
			currentAnimation = skeletonAnimationWalkEastWest;
			print ("Wwalk");
			break;
		case Direction.North:
			if (skeletonAnimationWalkNorth.transform.localScale.z < 1) {
				skeletonAnimationWalkNorth.transform.localScale = new Vector3 (1, 1, 1);
				skeletonAnimationWalkSouth.transform.localScale = new Vector3 (0, 0, 0);
				skeletonAnimationWalkEastWest.transform.localScale = new Vector3 (0, 0, 0);
			}
			skeletonAnimationWalkNorth.AnimationName = "Nwalk";
			currentAnimation = skeletonAnimationWalkNorth;
			break;
		case Direction.South:
			if (skeletonAnimationWalkSouth.transform.localScale.z < 1) {
				skeletonAnimationWalkNorth.transform.localScale = new Vector3 (0, 0, 0);
				skeletonAnimationWalkSouth.transform.localScale = new Vector3 (1, 1, 1);
				skeletonAnimationWalkEastWest.transform.localScale = new Vector3 (0, 0, 0);
			}
			skeletonAnimationWalkSouth.AnimationName = "Swalk";
			currentAnimation = skeletonAnimationWalkSouth;
			break;
		case Direction.None:
			currentAnimation.timeScale = 0;
			break;
		}
	}
//	void showAnimation (SkeletonAnimation animation) {
//		for (int i = 0; i < animations.length; i++) {
//			
//	}
}
