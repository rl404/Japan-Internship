using UnityEngine;
using System.Collections;

public class BoxSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (Random.Range(-200f, 200f), 0, Random.Range(-200f, 200f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
