using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTriggerScript : MonoBehaviour {
	public WallMoveScript[] walls;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.R)) {
			foreach(WallMoveScript wall in walls) {
				wall.reset();
			}
		}
	}

	void OnTriggerEnter(Collider collision) {
		GameObject collideObject = collision.gameObject;
		if (collideObject.layer == 13) {
			print("Move Dem Walls!");
			foreach(WallMoveScript wall in walls) {
				wall.moveWall();
			}
		}
	}
}
