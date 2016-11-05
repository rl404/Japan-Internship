using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ObjChar))]
public class ObjAControl : MonoBehaviour
{
	private Transform target;
	public Renderer tr;

	private ObjChar m_Character; // A reference to the ObjAChar on the object
	private ObjChar m_Enemy;
	public Transform m_Cam;   	// A reference to the main camera in the scenes transform
	public Camera cam;
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump = false;  					// the world-relative desired move direction, calculated from the camForward and user input.
	private bool m_Crouch = false;
	private bool m_Attacking;

	private int atkDamage = 20;
	private float h,v;
	private float timerRandom = 3, timer = 1, atkTimer = 0;
	private bool isAttacking = false;
	private float distance;


	void Start()
	{
		h = 0; v = 0; 

		// get the third person character ( this should never be null due to require component )
		target = GameObject.FindWithTag ("playerB").transform;
		m_Character = GetComponent<ObjChar>();
		m_Enemy = target.GetComponent<ObjChar> ();

		m_Character.changeRole (1);

	}

	void OnGUI() {
		string role;
		if (m_Character.getRole () == 0) {
			role = "Runner";
		} else {
			role = "Catcher";
		}

		Vector2 pos = new Vector2(20,10);
		Vector2 size = new Vector2(100,20);
		GUI.Label(new Rect(pos.x, pos.y, size.x, size.y), role);
	}
		
	// Fixed update is called in sync with physics
	void FixedUpdate()
	{
		if (m_Character.getReady ()) {
			if (m_Character.getRole () == 1) {
				catchRole ();
			} else {
				runRole ();
			}
		} else {
			m_Move = v*Vector3.forward + h*Vector3.right;
			v = 0; h = 0;
			m_Attacking = false;
			timer = 1;
		}


		m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
		m_Move = v * m_CamForward + h * m_Cam.right;

		m_Character.Move (m_Move, m_Crouch, m_Jump, m_Attacking);
	}

	void searchCatch(){
		if (tr.IsVisibleFrom (cam)) {
			timer -= Time.deltaTime;

			var q = Quaternion.LookRotation (target.position - transform.position);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 200f * Time.deltaTime);

			if (timer <= 0) {
				v = 1;
				h = 0;
			}

		} else {
			v = 0; h = 0;
			transform.Rotate (0, 100 * Time.deltaTime, 0);
			timer = 1;
		}
	}

	void attack() {
		if (distance < 1.5) {
			v = 0; h = 0;

			atkTimer -= Time.deltaTime;
			if (atkTimer <= 0) {
				if (isAttacking) {
					isAttacking = false;
					m_Attacking = false;
				} else if (!isAttacking && m_Enemy.m_HP > 0) {
					isAttacking = true;
					m_Attacking = true;
					m_Enemy.reduceHP (atkDamage);
				}
				atkTimer = 1;
			} 
		}else{
			m_Attacking = false;
			isAttacking = false;
			atkTimer = 0;
		}
	}

	// Catch role
	void catchRole() {
		distance = Vector3.Distance(target.position, transform.position);

		searchCatch ();
		attack ();
	}
		
	void randomRun(){
		timerRandom -= Time.deltaTime;
		if (timerRandom < 0) {
			v = 1f;
			h = UnityEngine.Random.Range (-0.5f, 0.5f);
			timerRandom = 3;
		}
	}

	void attacked(){
		distance = Vector3.Distance(target.position, transform.position);
		if (distance > 10 && m_Character.getNearEnemy()) {
			m_Character.changeNearEnemy (false);
		}
	}

	void avoidBox(){
		RaycastHit hit;
		Ray cameraRay = new Ray (transform.position + 0.5f*Vector3.up, m_Move);
		Debug.DrawRay (transform.position + 0.5f*Vector3.up, m_Move * 3);

		if(Physics.Raycast(cameraRay, out hit, 3)){
			if (hit.collider.tag == "box") {
				h = UnityEngine.Random.Range (-0.5f, 0.5f);
				timerRandom = 0;
			} 
		}
	}

	// Run role
	void runRole() {
		isAttacking = false;
		m_Attacking = false;

		timer -= Time.deltaTime;
		if (timer >= 0) {
			var q = Quaternion.LookRotation (-target.position + transform.position);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 200f * Time.deltaTime);
		}

		randomRun ();
		attacked ();
		avoidBox ();
	}
}

