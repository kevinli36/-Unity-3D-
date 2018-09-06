using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UserAction
{
	void Restart();
}

public class UserInterface : MonoBehaviour {

	private UserAction ui;
	public GameObject player;
	public float speed = 1f;
	public SceneController currentscene;

	// Use this for initialization
	void Start () {
		ui = Director.get_instance ().current_scene as UserAction;
		currentscene = Director.get_instance ().current_scene;
		player = currentscene.player;
	}

	void OnGUI()
	{
		GUIStyle font = new GUIStyle ();
		font.fontSize = 30;
		font.normal.textColor = new Color (255, 255, 255);
		if (currentscene.game_state == 0) {
			GUI.Label (new Rect (220, 80, 120, 40), currentscene.result, font);
		} else {
			if (GUI.RepeatButton (new Rect (0, 0, 120, 40), "Rule")) {
					GUI.Label (new Rect(220, 50, 500, 500), "Use direction key to move your ball.\nTry you best to avoid the chaser to \nget one point. If you are caught, game over.", font);
			}
			GUI.Label (new Rect (0, 120, 120, 40), "Score: " + Director.get_instance ().current_score.Score, font);
		}
		if (GUI.Button (new Rect (0, 60, 120, 40), "Restart")) 
			ui.Restart ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentscene.game_state != 0) {
			float translationX = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
			float translationZ = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
			player.transform.Translate (translationX, 0, 0);
			player.transform.Translate (0, 0, translationZ);
		}
	}
}
