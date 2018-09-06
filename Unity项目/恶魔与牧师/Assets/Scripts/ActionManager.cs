using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDGame;

namespace ActionManager{

	public enum SSActionEventType:int{Started, Completed}

	public interface ISSActionCallBack
	{
		void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Completed, 
			int intParam = 0, string strParam = null, Object objParam = null);
	}

	public class SSAction : ScriptableObject{
		public bool destroy = false;
		public bool enable = true;
		public GameObject gameobject;
		public Transform transform;
		public ISSActionCallBack callback;

		protected SSAction(){}

		public virtual void Start(){}

		public virtual void Update(){}
	}

	public class SequenceAction : SSAction, ISSActionCallBack{
		public List<SSAction> sequence;
		public int repeat = -1;
		public int start = 0;

		public static SequenceAction GetSSAction(int repeat, int start, List<SSAction>sequence){
			SequenceAction action = ScriptableObject.CreateInstance<SequenceAction> ();
			action.repeat = repeat; // repeat forever
			action.sequence = sequence;
			action.start = start;
			return action;
		}

		public override void Update(){
			if (sequence.Count == 0)
				return;
			if (start < sequence.Count)
				sequence [start].Update ();
		}

		public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Completed, 
			int intParam = 0, string strParam = null, Object objParam = null){
		
			source.destroy = false;
			this.start++;
			if (this.start >= sequence.Count) {
				this.start = 0;
				if (repeat > 0)
					repeat--;
				if (repeat == 0) {
					this.destroy = true;
					this.callback.SSActionEvent (this);
				}
			}
		}

		public override void Start ()
		{
			foreach (SSAction action in sequence) {
				action.gameobject = this.gameobject;
				action.transform = this.transform;
				action.callback = this;
				action.Start ();
			}
		}

		public void OnDestroy(){}
	}

	public class SSActionManager : MonoBehaviour, ISSActionCallBack{
		private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
		private List<SSAction> waitingAdd = new List<SSAction>();
		private List<int> waitingDelete = new List<int>();

		protected void Update(){

			foreach (SSAction ac in waitingAdd) 
				actions [ac.GetInstanceID ()] = ac;
			waitingAdd.Clear ();

			foreach (KeyValuePair<int, SSAction>kv in actions) {
				SSAction ac = kv.Value;
				if (ac.destroy)
					waitingDelete.Add (ac.GetInstanceID ());
				else if (ac.enable)
					ac.Update ();
			}

			foreach (int key in waitingDelete) {
				SSAction ac = actions [key];
				actions.Remove (key);
				DestroyObject (ac);
			}
			waitingDelete.Clear ();
		}

		public void RunAction(GameObject gameobject, SSAction action, ISSActionCallBack callback){
			action.gameobject = gameobject;
			action.transform = gameobject.transform;
			action.callback = callback;
			waitingAdd.Add (action);
			action.Start ();
		}

		protected void Start(){
		}

		public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Completed, 
			int intParam = 0, string strParam = null, Object objParam = null){
		}			//As there will be only "moving" this action, after this, no more action will be done. Thus callback can be null.
	}

	public class MoveToAction : SSAction{
		public Vector3 target;
		public float speed;

		private MoveToAction(){}
		public static MoveToAction GetSSAction(Vector3 target, float speed)
		{
			MoveToAction action = ScriptableObject.CreateInstance<MoveToAction> ();
			action.target = target;
			action.speed = speed;
			return action;
		}

		public override void Update ()
		{
			Debug.Log ("in update");
			this.transform.position = Vector3.MoveTowards (this.transform.position, target, speed * Time.deltaTime);
			if (this.transform.position == target) {
				this.destroy = true;
				this.callback.SSActionEvent (this);
			}
		}

		public override void Start(){
		}
	}

	public class CCActionManager : SSActionManager{
		public Controller controll;
		public SSAction moveboat;

		protected void Start(){
			controll = (Controller)Director.GetInstance ().CurrentSceneController;
			controll.actionManager = this;
		}

		public void moveb(GameObject obj, Vector3 target, float speed){
			moveboat = MoveToAction.GetSSAction (target, speed);
			this.RunAction (obj, moveboat, this);
		}

		protected new void Update(){
			base.Update ();
		}
	}


}
