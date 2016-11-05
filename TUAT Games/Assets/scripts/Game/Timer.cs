using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	private ObjChar m_CharA, m_CharB;
	public double gameTimer = 60, timer, readyTimer = 5; 
	private Vector2 pos = new Vector2(Screen.width/2 - 50,40);
	private Vector2 size = new Vector2(100,20);
	private string timerStr;
	private bool end, ready;

	private int scoreA = 0, scoreB = 0;
	private string scoreAStr, scoreBStr;

	public Text t;

	void Start () {
		timer = gameTimer;
		end = false; 
		ready = false;

		m_CharA = GameObject.FindWithTag ("playerA").GetComponent<ObjChar>();
		m_CharB = GameObject.FindWithTag ("playerB").GetComponent<ObjChar>();

	}

	void OnGUI() {
		GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), timerStr);

		GUI.Box(new Rect(Screen.width/2 - 100, 40, 40, 20), scoreAStr);
		GUI.Box(new Rect(Screen.width/2 + 60, 40, 40, 20), scoreBStr);
	}

	void Update () {
		if (m_CharA.getHP () <= 0 || m_CharB.getHP () <= 0)
			end = true;
		
		if(!end && ready)
			timer -= Time.deltaTime;

		if (end || timer <= 0) {
			if (m_CharA.getRole () == 0) {
				if (m_CharA.getHP () > 0) {
					m_CharA.End (1);
					m_CharB.End (0);
					scoreA++;
				} else {
					m_CharA.End (0);
					m_CharB.End (1);
					scoreB++;
				}
			} else {
				if (m_CharA.getHP () > 0) {
					m_CharA.End (0);
					m_CharB.End (1);
					scoreB++;
				} else {
					m_CharA.End (1);
					m_CharB.End (0);
					scoreA++;
				}
			}
			end = false;
			ready = false;
			timer = gameTimer;
		}
			
		timerStr = timer.ToString("F1");

		if (!ready) {
			if (readyTimer <= 0) {
				ready = true;
				m_CharA.changeReady (true);
				m_CharB.changeReady (true);
				readyTimer = 5;
			} else {
				readyTimer -= Time.deltaTime;
				timerStr = readyTimer.ToString("F1");
			}
		}

		scoreAStr = scoreA.ToString ();
		scoreBStr = scoreB.ToString ();
	}

}
