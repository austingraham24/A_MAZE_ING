using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoveScript : MonoBehaviour {
	private Vector3 initialPostion;
	public Vector3 secondPosition;
	private bool shouldMove;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		initialPostion = transform.position;
		shouldMove = false;
		target = secondPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldMove) {
			transform.position = Vector3.MoveTowards(transform.position, target, (1 * Time.deltaTime));
			if (transform.position == initialPostion || transform.position == secondPosition) {
				shouldMove = false;
				if (transform.position == initialPostion) {
					target = secondPosition;
				}
				else {
					target = initialPostion;
				}
				return;
			}	
		}
		
	}

	public void reset() {
		transform.position = initialPostion;
		shouldMove = false;
		target = secondPosition;
	}

	public void moveWall() {
		shouldMove = true;
	}
}
