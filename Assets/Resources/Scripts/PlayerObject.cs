using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {
	public int score;
	private GameObject[] fireballs;
	private GUIStyle cardStyle;
	private bool frozenGame;
	private Sprite aliveSprite;
	private Sprite deadSprite;
	
	public void ignite () {
		for (int i = 0; i < fireballs.Length; i++) {
			FireballObject fireball = fireballs [i].GetComponent <FireballObject> ();

			fireball.respawn ();
			fireball.freezePosition = true;

			cardStyle.alignment = TextAnchor.MiddleCenter;
			cardStyle.fontSize = 156;

		}
		frozenGame = true;
		GetComponent <SpriteRenderer> ().sprite = deadSprite;
	}

	private void Start () {
		score = 0;
		
		aliveSprite = Resources.Load<Sprite>("Images/Player");
		deadSprite = Resources.Load<Sprite>("Images/PlayerBurning");

		GetComponent <SpriteRenderer> ().sprite = aliveSprite;
		fireballs = GameObject.FindGameObjectsWithTag("Fireball");

		cardStyle = new GUIStyle();
		cardStyle.alignment = TextAnchor.UpperLeft;
		cardStyle.fontSize = 92;

		incrementScoreLoop ();
	}
	
	private	void Update () {
		InputControls ();
	}

	private void OnGUI()
    {
        GUI.Box(new Rect(0, 0 , Screen.width, Screen.height), "Score: " + score, cardStyle);
    }

	private void incrementScoreLoop () {
		score += 5;
		if (!frozenGame) {
			Invoke ("incrementScoreLoop", 1);
		}
	}

	private void InputControls () {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			if (frozenGame) {
				if (touch.phase == TouchPhase.Began) {
					for (int i = 0; i < fireballs.Length; i++) {
						FireballObject fireball = fireballs [i].GetComponent <FireballObject> ();
						fireball.freezePosition = false;

						cardStyle.alignment = TextAnchor.UpperLeft;
						cardStyle.fontSize = 92;

					}
					frozenGame = false;
					GetComponent <SpriteRenderer> ().sprite = aliveSprite;
					score = 0;
				}
			}
			else {
				Vector3 newPosition = Camera.main.ScreenToWorldPoint (touch.position);
				newPosition.z = 10;
				transform.position = newPosition;
			}
		}
	}
}
