using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	private SceneController scene;

	// Use this for initialization
	void Start () {
		scene = SceneController.get_instance ();
	}

	void  OnGUI(){
		GUI.Label (new Rect (20, 20, 100, 100), "Score: " + scene.getScoreController ().getScore ());
		GUI.Label (new Rect (20, 60, 100, 100), "WindDirection: " + scene.getCurrentScene ().getwindDirection ());
		GUI.Label (new Rect (20, 100, 100, 100), "WindForce: " + scene.getCurrentScene ().getwindForce());
/*		if (GUI.Button (new Rect (20, 140, 100, 100), "Clear")) {
			ArrowFactory_Base.get_instance ().freeall ();
		}*/
		if (GUI.Button (new Rect (80, 140, 60, 60), "Restart")) {
			GameObject[] Arrow = GameObject.FindGameObjectsWithTag("arrow");
			foreach (GameObject one in Arrow) {
				Destroy (one);
			}
			ArrowFactory_Base.get_instance ().freeall ();
			scene.getScoreController ().setScore (0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			scene.shootArrow (mouseRay.direction);
		}
		ArrowFactory_Base.get_instance ().judgearrow ();
	}
}
