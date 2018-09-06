using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
	public int Score = 0;
	// Use this for initialization
	void OnEnable(){
		GateController.addScore += add;
	}

	void OnDisable(){
		GateController.addScore -= add;
	}

	void add(){
		Score++;
	}

	void Start ()
	{
		Director.get_instance ().current_score = this;
	}
}
