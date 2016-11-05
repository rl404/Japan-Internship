using UnityEngine;
using System.Collections;

public class respawn : MonoBehaviour {

	private Vector3 startPos;
	private Quaternion startRot;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		startRot = transform.rotation;
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag == "death") {
			transform.position = startPos;
			transform.rotation = startRot;
			GetComponent<Animator>().Play("LOSE00",-1,0f);
			GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
			GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);

		}
	}
}
