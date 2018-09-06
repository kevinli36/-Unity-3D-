using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
	private int score;
	private SceneController scene;

	public void CountScore(string name)
	{
		switch (name) {
		case "C1":
			score += 5;
			break;
		case "C2":
			score += 4;
			break;
		case "C3":
			score += 3;
			break;
		case "C4":
			score += 2;
			break;
		case "C5":
			score += 1;
			break;
		default:
			break;
		}
	}

	public int getScore(){
		return score;
	}

	public void setScore(int score)
	{
		this.score = score;
	}

	// Use this for initialization
	void Start () {
		score = 0;
		scene = SceneController.get_instance ();
		scene.setScoreController (this);
	}
}


