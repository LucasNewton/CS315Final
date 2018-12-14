using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballObject : MonoBehaviour {

	public bool freezePosition;
	private PlayerObject player;
	private Vector2 velocity;
	private float rotateSpeed;

	public void respawn () {
		Vector3 newPosition = Vector3.zero;
        int side = Random.Range (1, 5);
        rotateSpeed = Random.Range (3f, 7f);
        int rotateNeg = Random.Range (1, 3);
        if (rotateNeg == 1) {
            rotateSpeed *= -1;
        }

        //  coming from top
        if (side == 1) {
            velocity.y = Random.Range (-3f, -0.5f);
            velocity.x = Random.Range (-3f, 3f);

            newPosition.x = Random.Range (0, Screen.width);
            newPosition.y = Screen.height + 64;
            newPosition.z = 10;
        }
        //  coming from bottom
        else if (side == 2) {
            velocity.y = Random.Range (3f, 0.5f);
            velocity.x = Random.Range (-3f, 3f);

            newPosition.x = Random.Range (0, Screen.width);
            newPosition.y = -64;
            newPosition.z = 10;
        }
        //  coming from left
        else if (side == 3) {
            velocity.y = Random.Range (-3f, 3f);
            velocity.x = Random.Range (0.5f, 3f);

            newPosition.x = -64;
            newPosition.y = Random.Range (0, Screen.height);
            newPosition.z = 10;
        }
        //  coming from right
        else if (side == 4) {
            velocity.y = Random.Range (-3f, 3f);
            velocity.x = Random.Range (-3f, -0.5f);

            newPosition.x = Screen.width + 64;
            newPosition.y = Random.Range (0, Screen.height);
            newPosition.z = 10;
        }

        newPosition = Camera.main.ScreenToWorldPoint (newPosition);
        transform.position = newPosition;
	}

	private void Start () {
		freezePosition = false;
		player = (PlayerObject) GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerObject));
        respawn ();
	}
	
	// Update is called once per frame
	private void Update () {
		if (!freezePosition)
		{
			moveEnemy ();
			checkForRespawn ();
			transform.Rotate (new Vector3 (0, 0, rotateSpeed));
		}
	}

	private void moveEnemy () {
        Vector2 newPosition = transform.position;
        newPosition.x += velocity.x * Time.deltaTime;
        newPosition.y += velocity.y * Time.deltaTime;

		transform.position = newPosition;

    	if (Camera.main.WorldToScreenPoint(transform.position).x > Camera.main.WorldToScreenPoint(player.transform.position).x - 36 && Camera.main.WorldToScreenPoint(transform.position).x < Camera.main.WorldToScreenPoint(player.transform.position).x + 36 && Camera.main.WorldToScreenPoint(transform.position).y < Camera.main.WorldToScreenPoint(player.transform.position).y + 100 && Camera.main.WorldToScreenPoint(transform.position).y > Camera.main.WorldToScreenPoint(player.transform.position).y - 100) {
            player.ignite ();
        }
	}

	private void checkForRespawn () {
        if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width + 75 || Camera.main.WorldToScreenPoint(transform.position).x < -75) {
            respawn ();
			player.score ++;
        }
        else if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height + 75 || Camera.main.WorldToScreenPoint(transform.position).y < -75) {
            respawn ();
			player.score ++;
        }
    }
}
