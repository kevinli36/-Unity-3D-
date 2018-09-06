using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDGame;

public class UserInterface : MonoBehaviour {

	Director game = Director.GetInstance (); 
	UserActions action;
	string GameName = "恶魔与牧师";
	string GameRule  = "鼠标点击操作，帮助三个牧师和三个魔鬼过河。正方形代表魔鬼，圆形代表牧师。任意一边魔鬼数量大于牧师数量则游戏失败。";


	void Start () {
		
		action = Director.GetInstance ().CurrentSceneController as UserActions;
	}

	void OnGUI(){
		float width = Screen.width / 12;
		float height = Screen.height / 6;
		if (game.state == State.WIN) {
			if (GUI.Button (new Rect (width * 6f, height, width, height), "WIN"))
				action.restart ();
		} 
		else if (game.state == State.LOSE) {
			if (GUI.Button (new Rect (width * 6f, height, width, height), "LOSE"))
				action.restart ();
		} 
		else {
			if (GUI.RepeatButton (new Rect (10, 10, 120, 20), GameName))
				GUI.TextArea (new Rect (10, 40, 240, 80), GameRule);
			else if (GUI.Button (new Rect (width * 2f, height * 4f, 60f, 60f), "下一步"))
				action.nextturn ();
			else if (game.state == State.START || game.state == State.END) {
				if (GUI.Button (new Rect (width * 6f + 15f, height * 4f, 40f, 40f), "开船"))
					action.moveboat ();
				if (GUI.Button (new Rect (width * 4f + 10f, height * 4f, 60f, 60f), "左侧\n下船"))
					action.offboatleft ();
				if (GUI.Button (new Rect (width * 8f, height * 4f, 60f, 60f), "右侧\n下船"))
					action.offboatright ();
				if (GUI.Button (new Rect (width, height, 60f, 60f), "牧师\n上船"))
					action.pOnboat ();
				if (GUI.Button (new Rect (width * 4f, height, 60f, 60f), "恶魔\n上船"))
					action.dOnboat ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
