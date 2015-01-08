using UnityEngine;
using System.Collections;

public class MonoElement : MonoBehaviour {

	Transform _cachedTransform = null;
	public Transform cachedTransform {
		get {
			if(_cachedTransform == null)
				_cachedTransform = transform;
			return _cachedTransform;
		}
	}

	protected GameObject _cachedGameObject = null;
	public GameObject cachedGameObject {
		get {
				if (_cachedGameObject == null)
						_cachedGameObject = gameObject;
				return _cachedGameObject;
		}
	}

	protected Game _gameManager = null;
	public Game gameManager {
		get {
			if (_gameManager == null)
				_gameManager = Game.instance;
			return _gameManager;
		}
	}

	void Awake () {Init ();}
	public virtual void Init () {}


	public virtual void OnUpdate () {}
	void Update () {
		OnUpdate ();
	}


	public virtual void AfterDestroy () {}
	void OnDestroy () {
		AfterDestroy();
	}

}
