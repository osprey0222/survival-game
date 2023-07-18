using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour {
	public float speed = 3f;
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 1000f;

	// Use this for initialization
	void Awake () {
		floorMask = LayerMask.GetMask ("Floor");
		//Debug.Log("LayerMask " + LayerMask.GetMask ("Floor"));
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move (float h, float v){
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning(){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		//Debug.Log("camRay " + camRay);
		RaycastHit floorHit;
		//Debug.Log("LayerMask " + LayerMask.LayerToName(256));
		//Debug.Log("LayerMask " + LayerMask.NameToLayer("Floor"));
		//print (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask));
		//if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			//Debug.Log("floorHit " + floorHit);
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
			//print ("byebye");
		}
	}

	void Animating(float h, float v){
		bool running = h != 0f || v != 0f;
		anim.SetBool ("IsRunning", running);
	}
}
