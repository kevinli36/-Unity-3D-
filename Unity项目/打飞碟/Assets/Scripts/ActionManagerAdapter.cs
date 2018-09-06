using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class ActionManagerAdapter : MonoBehaviour{

	private SceneController scene;

	public void playdisk(bool isPhy)
	{
		scene.getModel ().prepareemit (isPhy);
	}

	void Start()
	{
		scene = SceneController.get_instance ();
		scene.setActionManagerAdpter (this);
	}
}
