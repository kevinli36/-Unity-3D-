﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject vb;

	// Use this for initialization
	void Start () {
		VirtualButtonAbstractBehaviour vbb = vb.GetComponent<VirtualButtonAbstractBehaviour> ();
		if (vbb) {
			vbb.RegisterEventHandler (this);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		vb.transform.localScale = vb.transform.localScale * 2;
		Debug.Log ("按钮按下");
	}
                                                                                  
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
	{
		vb.transform.localScale = vb.transform.localScale / 2;
		Debug.Log ("按钮释放");		
	}
}
