using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour {

	private float windForce;
	private SceneController scene;
	public float speed;

	public void shoot(GameObject arrow, Vector3 dir)
	{
		arrow.transform.up = dir;
		arrow.GetComponent<Rigidbody> ().velocity = dir * speed;
		windForce = Random.Range(-50, 50);
		arrow.GetComponent<Rigidbody>().AddForce(new Vector3(windForce, 0, 0), ForceMode.Force);
	}

	public float getwindForce()
	{
		return windForce;
	}

	public string getwindDirection(){
		if(windForce > 0)
			return "Right";
		else if (windForce < 0)
			return "Left";
		else 
			return "No wind";	
	}

	// Use this for initialization
	void Start () {
		scene = SceneController.get_instance ();
		scene.setCurrentScene (this);
	}

}

public class SceneController : System.Object{
	private static SceneController instance;
	private FirstSceneController current_scene;
	private ScoreController current_score;

	public static SceneController get_instance(){
		if (instance == null) {
			instance = new SceneController ();
		}
		return instance;
	}

	public FirstSceneController getCurrentScene(){
		return current_scene;
	}

	public void setCurrentScene(FirstSceneController current){
		current_scene = current;
	}

	public ScoreController getScoreController(){
		return current_score;
	}

	public void setScoreController(ScoreController _score)
	{
		current_score = _score;
	}

	public void shootArrow(Vector3 direction)
	{
		GameObject arrow = ArrowFactory_Base.get_instance ().get_arrow ();
		arrow.transform.position = new Vector3(0, 10, -10);
		current_scene.shoot (arrow, direction);
	}
}
