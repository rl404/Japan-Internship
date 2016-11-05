using UnityEngine;
using System.Collections;

public class ObjAHP : MonoBehaviour {

	private ObjChar m_Character;

	public float barDisplay; //current progress
	private Vector2 pos = new Vector2(20,40);
	private Vector2 size = new Vector2(100,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;

	void Start() {
		m_Character = GetComponent<ObjChar>();
	}

	void OnGUI() {
		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);

		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
		GUI.EndGroup();
		GUI.EndGroup();
	}

	void Update() {
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		barDisplay = m_Character.getHP() *1f/100;
		//        barDisplay = MyControlScript.staticHealth;
	}// Use this for initialization
}
