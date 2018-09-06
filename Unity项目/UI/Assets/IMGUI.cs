using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IMGUI : MonoBehaviour {
	public float blood = 1f;
	public float current_blood = 0f;

	void Start()
	{
		current_blood = blood;
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (20, 30, 40, 30), "加血")) {
			blood = blood + 1f;
			blood = (blood > 10) ? 10f : blood;
		}
		if (GUI.Button (new Rect (20, 70, 40, 30), "减血")) {
			blood = blood - 1f;
			blood = (blood < 0) ? 0f : blood;
		}
		current_blood = Mathf.Lerp (current_blood, blood, 0.05f);
		GUI.HorizontalScrollbar (new Rect (100, 100, 150, 20), 0f, current_blood, 0f, 10f);  
	}
}
