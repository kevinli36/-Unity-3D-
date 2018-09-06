using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	private int turn = 1;
	private int[,] state = new int[3, 3];

	private int finish = 0;
	public Texture2D img;
	public Texture2D o;
	public Texture2D x;

	// Use this for initialization
	void Start () {
		Reset ();	
	}

	//reset
	void Reset(){
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++)
				state [i, j] = 0;
		}
		finish = 0;
	}

	void OnGUI() {

		//set style of elements
		GUIStyle title = new GUIStyle ();
		title.fontSize = 30;
		title.normal.textColor = Color.black;
		GUIStyle background = new GUIStyle ();
		background.normal.background = img;
		GUIStyle info = new GUIStyle ();
		info.fontSize = 20;
		title.normal.textColor = Color.black;

		//game view
		GUI.Label (new Rect (0, 0, 1024, 720), "", background);
		GUI.Label (new Rect (230, 0, 200, 50), "Welcome to Tic-Tac-Toe!", title);
		if (GUI.Button (new Rect (345, 220, 60, 30), "Reset"))
			Reset ();
		if (turn == 1)
			GUI.Label (new Rect (180, 60, 50, 50), "O's turn", info);
		else
			GUI.Label (new Rect (180, 60, 50, 50), "X's turn", info);

		//judge winner
		int result = check ();
		if (result == 1) {
			GUI.Label (new Rect (180, 100, 50, 50), "O win!", info);
			finish = 1;
		} else if (result == 2) {
			GUI.Label (new Rect (180, 100, 50, 50), "X win!", info);
			finish = 1;
		} else if (result == -1) {
			GUI.Label (new Rect (180, 100, 50, 50), "Draw game!", info);
			finish = 1;			
		}

		//paint buttons
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (state [i, j] == 1)
					GUI.Button (new Rect (300 + i * 50, 50 + j * 50, 50, 50), o);
				else if (state [i, j] == 2)
					GUI.Button (new Rect (300 + i * 50, 50 + j * 50, 50, 50), x);
				else if (GUI.Button (new Rect (300 + i * 50, 50 + j * 50, 50, 50), "")) {
					if (finish == 1)
						continue;
					if (turn == 1)
						state [i, j] = 1;
					else if (turn == -1)
						state [i, j] = 2;
					turn = -turn;
				}
			}
		}
	}

	//judge winner
	int check() {
		int i = 0, j = 0;
		for (i = 0; i < 3; i++){
			if (state [i, 0] != 0 && state [i, 0] == state [i, 1] && state [i, 1] == state [i, 2])
				return state [i, 1];
		}
		for (i = 0; i < 3; i++) {
			if (state [0, i] != 0 && state [0, i] == state [1, i] && state [1, i] == state [2, i])
				return state [1, i];	
		}
		if (state [1, 1] != 0 && ((state [0, 0] == state [1, 1] && state [1, 1] == state [2, 2]) || (state [0, 2] == state [1, 1] && state [1, 1] == state [2, 0])))
			return state [1, 1];
		int drawgame = 1;
		for (i = 0; i < 3; i++) {
			for (j = 0; j < 3; j++) {
				if (state [i, j] == 0) {
					drawgame = 0;
					break;
				}
			}
		}
		if (drawgame == 1)
			return -1;
		else
			return 0;
	}
}

