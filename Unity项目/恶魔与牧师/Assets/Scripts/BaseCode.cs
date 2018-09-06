using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDGame;

namespace PDGame{

	public enum State {START, END, WIN, LOSE};

	public interface UserActions{
		void pOnboat();
		void dOnboat();
		void moveboat ();
		void offboatleft ();
		void offboatright ();
		void restart();
		void nextturn();
	}

	public interface SceneController{
		void LoadResources();
	}


	public class Director : System.Object{

		private static Director instance;

		public SceneController CurrentSceneController;
		public State state = State.START;

		public static Director GetInstance(){
			if (instance == null)
				instance = new Director ();
			return instance;
		}
	}
}
