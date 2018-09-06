using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface scene_interface
{
	void LoadResources();
}

public class SceneController : MonoBehaviour, UserAction, scene_interface {

	public string result = "";
	public int game_state = 1;
	public GameObject player;
	private Director director;

	void Awake()
	{
		director = Director.get_instance ();
		director.current_scene = this;
		director.current_scene.LoadResources ();
	}

	public void LoadResources ()
	{
		player = Instantiate (Resources.Load ("Prefabs/Player")) as GameObject;
		int x = 0, z = 0;
		int temp = 1;
		Instantiate (Resources.Load ("Prefabs/maze"), new Vector3 (x, 0, z), Quaternion.identity);
		Instantiate (Resources.Load ("Prefabs/Patrol"), new Vector3 (x - 3, 1.5f, z), Quaternion.identity);
		for (int i = 0; i < 2; i++) {
			for (int k = 0; k < 2; k++) {
				temp = -temp;
				x = 15 * i * temp;
				z = (2 - i) * 5 * temp;
				Instantiate (Resources.Load ("Prefabs/maze"), new Vector3 (x, 0, z), Quaternion.identity);
				Instantiate (Resources.Load ("Prefabs/Patrol"), new Vector3 (x - 3, 1.5f, z), Quaternion.identity);
			}
		}
	}

	// Use this for initialization

	void OnEnable()
	{
		PlayerController.gameOver_message += GameOverEvent;
	}

	void OnDisable()
	{
		PlayerController.gameOver_message -= GameOverEvent;
	}

	void GameOverEvent()
	{
		result = "Game Over!!!";
		game_state = 0;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		game_state = 1;
	}

}

public class Director : System.Object{
	private static Director instance;
	public SceneController current_scene;
	public ScoreController current_score;

	public static Director get_instance(){
		if (instance == null) {
			instance = new Director ();
		}
		return instance;
	}

}