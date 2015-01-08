/**
 * @class MonoSingleton - Generic Based Singleton for MonoBehaviours
 * from url => http://wiki.unity3d.com/index.php?title=Singleton
 * submitted by Berenger
 */ 

/*
 * This has been edited. Not the original version
 */

using UnityEngine;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{

	public static bool inScene {
		get {
			T t = FindObjectOfType<T> ();
			//Debug.Log ("Checking if *" + typeof(T) + "* is in scene: " + (t != null));
			return t;
		}
	}

    private static T m_Instance = null;
    public static T instance
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if( m_Instance == null )
            {
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
 
                // Object not found, we create a temporary one
                if( m_Instance == null )
                {
#if true
					//return m_Instance;
					Debug.LogWarning ("No instance of " + typeof(T).ToString());
					m_Instance = Create(null);
#endif

#if false
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
 					
                    // Problem during the creation, this should not happen
                    if( m_Instance == null )
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
#endif
                }
				m_Instance.DoInitialize();
            }
            return m_Instance;
        }
    }

	private static bool _isCreated = false;
	public static bool isCreated {get {return _isCreated;}}
	public static T Create(Transform parent) {
		T component = default(T);
		if (!_isCreated) {
			component = new GameObject ("z" + typeof(T).ToString ()).AddComponent<T> ();
			component.transform.parent = parent;
			component.transform.localPosition = Vector3.zero;
			component.transform.localScale = Vector3.one;
			component.DoInitialize ();
			_isCreated = true;
		} else
			component = instance;
		return component;
	}
	
	protected bool _isInitialized = false;
	private Transform _cachedTransform = null;
	public Transform cachedTransform {
		get {
			if(_cachedTransform == null)
				_cachedTransform = transform;
			return _cachedTransform;
		}
	}
	
    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake() {
        if( m_Instance != null && m_Instance != this) {
            DestroyImmediate(gameObject);
        }else if( m_Instance == null) {
			m_Instance = this as T;
			m_Instance.DoInitialize();
		}
    }
 
    // This function is called when the instance is used the first time
    // Put all the initializations you need here, as you would do in Awake
    public virtual void Init(){}
	
	private void DoInitialize() {
		if(!_isInitialized) {
			Init();
			_isInitialized = true;
		}
	}
 
    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        m_Instance = null;
		_isCreated = false;
    }

	private void OnDestroy () {
		m_Instance = null;
		_isCreated = false;
	}
}