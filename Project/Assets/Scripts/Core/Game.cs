using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoSingleton<Game> {
	public Transform[] roots;
#if UNITY_EDITOR
	public List<Camera> cameras;
#endif
	public Dictionary<string, Camera> cameraMap;

	public override void Init () {
		base.Init ();
		InitializeScreenSettings();
		InitializeCameras();
	}

	public void InitializeScreenSettings () {
		for(int i = 0; i < roots.Length; i++) {
			roots[i].localScale = Utility.ScreenSupport.rootScale;
		}

	}

	public void InitializeCameras () {
		Camera[] tCameras = Camera.allCameras;
		cameras = new List<Camera>();
		cameraMap = new Dictionary<string, Camera>();
		for(int i = 0; i < tCameras.Length; i++) {
			#if UNITY_EDITOR
			cameras.Add(tCameras[i]);
			#endif
			cameraMap.Add(tCameras[i].name, tCameras[i]);
		}
		if(cameraMap.ContainsKey("3DCamera"))
			cameraMap["3DCamera"].transform.localPosition = new Vector3(0, 0, Utility.ScreenSupport.zOffsetFor3Dcamera);
	}
}
