using UnityEngine;
using System.Collections;

public class PlaneSet : MonoBehaviour {

	public Plane myPlane;
	public GameObject box;

	void Start () {
		for (int i = 0; i < 1500; i++) {
			Instantiate(box);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
