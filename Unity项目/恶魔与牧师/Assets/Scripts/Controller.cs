using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDGame;
using ActionManager;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour, SceneController, UserActions {

	public CCActionManager actionManager;

	Stack<GameObject> priest_left = new Stack<GameObject>();
	Stack<GameObject> priest_right = new Stack<GameObject>();
	Stack<GameObject> devil_left = new Stack<GameObject>();
	Stack<GameObject> devil_right = new Stack<GameObject>();

	GameObject[] boat = new GameObject[2];
	GameObject boat_obj;
	public float speed = 100f;

	Director game;

	Vector3 shoreleft = new Vector3(-16f, 0, 0);
	Vector3 shoreright = new Vector3(16f, 0, 0);
	Vector3 boatleft = new Vector3(-8f, -1f, 0);
	Vector3 boatright = new Vector3(8f, -1f, 0);

	public void LoadResources(){
		Instantiate (Resources.Load ("Prefabs/shore"), shoreleft, Quaternion.identity);
		Instantiate (Resources.Load ("Prefabs/shore"), shoreright, Quaternion.identity);
		boat_obj = Instantiate (Resources.Load ("Prefabs/boat"), boatleft, Quaternion.identity) as GameObject;

		for (int i = 0; i < 3; i++) {
			devil_left.Push (Instantiate (Resources.Load ("Prefabs/devil")) as GameObject);
			priest_left.Push (Instantiate(Resources.Load("Prefabs/priest")) as GameObject);
		}
	}
	
	public void setPositions(Stack<GameObject> stack, Vector3 pos_start){
		GameObject[] all = stack.ToArray ();
		for (int i = 0; i < stack.Count; i++)
			all [i].transform.position = new Vector3 (pos_start.x + 1.5f * i, pos_start.y, pos_start.z);
	}

	public int boatNum(){
		int sum = 0;
		for (int i = 0; i < 2; i++) {
			if (boat [i] != null)
				sum++;
		}
		return sum;
	}

	public void Onboat(GameObject obj){
		if (boatNum () != 2) {
			obj.transform.parent = boat_obj.transform;
			if (boat [0] == null) {
				boat [0] = obj;
				obj.transform.localPosition = new Vector3 (-0.2f, 0.8f, 0);
			} else {
				boat [1] = obj;
				obj.transform.localPosition = new Vector3 (0.2f, 0.8f, 0);
			}
		}
	}

	public void Offboat(int side){
		if (boat [side] != null) {
			boat [side].transform.parent = null;
			if (game.state == State.START) {
				if (boat [side].tag == "priest")
					priest_left.Push (boat [side]);
				else if (boat [side].tag == "devil")
					devil_left.Push (boat [side]);				
			} 
			else if (game.state == State.END) {
				if (boat [side].tag == "priest")
					priest_right.Push (boat [side]);
				else if (boat [side].tag == "devil")
					devil_right.Push (boat [side]);
			}
			boat [side] = null;
		}
	}

	public void judge(){
		int p_boat = 0, d_boat = 0;
		int p_left = 0, p_right = 0, d_left = 0, d_right = 0;
		for (int i = 0; i < 2; i++) {
			if (boat [i] != null && boat [i].tag == "priest")
				p_boat++;
			else if (boat [i] != null && boat [i].tag == "devil")
				d_boat++;
		}
		if (game.state == State.START) {
			p_left = priest_left.Count + p_boat;
			d_left = devil_left.Count + d_boat;
			p_right = priest_right.Count;
			d_right = devil_right.Count;
		} 
		else if (game.state == State.END) {
			p_left = priest_left.Count;
			d_left = devil_left.Count;
			p_right = priest_right.Count + p_boat;
			d_right = devil_right.Count + d_boat;		
		}
		if ((p_left != 0 && p_left < d_left) || (p_right != 0 && d_right > p_right))
			game.state = State.LOSE;
		else if (p_right == 3 && d_right == 3)
			game.state = State.WIN;
		else
			return;
	}

	public void pOnboat(){
		if (game.state == State.START && priest_left.Count != 0 && boatNum () != 2)
			Onboat (priest_left.Pop ());
		else if (game.state == State.END && priest_right.Count != 0)
			Onboat (priest_right.Pop ());
	}

	public void dOnboat(){
		if (game.state == State.START && devil_left.Count != 0 && boatNum () != 2)
			Onboat (devil_left.Pop ());
		else if (game.state == State.END && devil_right.Count != 0)
			Onboat (devil_right.Pop ());				
	}

	public void moveboat (){
		if (boatNum () != 0) {
			if (game.state == State.START) {
				actionManager.moveb (boat_obj, boatright, speed);
				game.state = State.END;
				Debug.Log (game.state);

			} 
			else if (game.state == State.END) {
				actionManager.moveb (boat_obj, boatleft, speed);
				game.state = State.START;
			}
		}
	}

	public void offboatleft (){
		Offboat (0);
	}

	public void offboatright (){
		Offboat (1);
	}

	public void nextturn()
	{
		clearBoat ();
		if (priest_left.Count == 3 && devil_left.Count == 3 && game.state == State.START) {
			if (random_num () == 1) {
				dOnboat ();
				dOnboat ();
				moveboat ();
			} else {
				pOnboat ();
				dOnboat ();
				moveboat ();
			}	
		} 
		else if (priest_left.Count == 3 && devil_left.Count == 2 && game.state == State.END) {
			dOnboat ();
			moveboat ();
		}
		else if (priest_left.Count == 3 && devil_left.Count == 1 && game.state == State.END) {
			dOnboat ();
			moveboat ();
		} else if (priest_left.Count == 2 && devil_left.Count == 2 && game.state == State.END) {
			pOnboat ();
			moveboat ();
		} else if (priest_left.Count == 3 && devil_left.Count == 2 && game.state == State.START) {
			dOnboat ();
			dOnboat ();
			moveboat ();
		} else if (priest_left.Count == 3 && devil_left.Count == 0 && game.state == State.END) {
			dOnboat ();
			moveboat ();
		} else if (priest_left.Count == 3 && devil_left.Count == 1 && game.state == State.START) {
			pOnboat ();
			pOnboat ();
			moveboat ();
		} else if (priest_left.Count == 1 && devil_left.Count == 1 && game.state == State.END) {
			pOnboat ();
			dOnboat ();
			moveboat ();
		} else if (priest_left.Count == 2 && devil_left.Count == 2 && game.state == State.START) {
			pOnboat ();
			pOnboat ();
			moveboat ();
		} else if (priest_left.Count == 0 && devil_left.Count == 2 && game.state == State.END) {
			dOnboat ();
			moveboat ();
		} else if (priest_left.Count == 0 && devil_left.Count == 3 && game.state == State.START) {
			dOnboat ();
			dOnboat ();
			moveboat ();
		} else if (priest_left.Count == 0 && devil_left.Count == 1 && game.state == State.END) {
			if (random_num() == 1) {
				dOnboat ();
				moveboat ();
			} else {
				pOnboat ();
				moveboat ();
			}
		} else if (priest_left.Count == 1 && devil_left.Count == 1 && game.state == State.START) {
			pOnboat ();
			dOnboat ();
			moveboat ();
		} else if (priest_left.Count == 0 && devil_left.Count == 2 && game.state == State.START) {
			dOnboat ();
			dOnboat ();
			moveboat ();
		}
	}

	int random_num()
	{
		float num = Random.Range (0f, 1f);
		if (num > 0.5f) {
			return 0;
		} else
			return 1;
	}

	void clearBoat()
	{
		offboatleft ();
		offboatright ();
	}

	public void restart(){
		SceneManager.LoadScene (Application.loadedLevelName);
		game.state = State.START;
	}


	// Use this for initialization
	void Start () {
		game = Director.GetInstance();  
		game.CurrentSceneController = this;  
		LoadResources(); 
		actionManager = gameObject.AddComponent<CCActionManager> () as CCActionManager;
	}
	
	// Update is called once per frame
	void Update () {
		setPositions (priest_left, new Vector3 (-15, 3, 0));
		setPositions (priest_right, new Vector3 (12, 3, 0));
		setPositions (devil_left, new Vector3 (-20, 3, 0));
		setPositions (devil_right, new Vector3 (17, 3, 0));
		judge ();
	}
		
}
