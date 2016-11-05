using UnityEngine;
using System.Collections;

public class ObjASet : MonoBehaviour {

	public Transform target;
	public Renderer tr;

	public Camera cam;

	bool isRotating;

	void Start () {
		isRotating = false;
		//transform.Translate (0, 0, 10, Space.World);
	}

	void Update () {
		float init = transform.eulerAngles.y;

		if (tr.IsVisibleFrom(cam)) {
			var q = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 50f * Time.deltaTime);
			isRotating = true;

			if(init == transform.eulerAngles.y){
				isRotating = false;
			}

			if (!isRotating) {
				transform.position = Vector3.MoveTowards(transform.position, target.position, 5f * Time.deltaTime);
			}

		} else {
			transform.Rotate (0, 20 * Time.deltaTime, 0);
		}

	}
		
}
