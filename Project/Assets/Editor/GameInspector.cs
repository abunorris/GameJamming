using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Game))]
public class GameInspector : Editor {
	private Game _gameManager = null;
	public override void OnInspectorGUI () {
		if(_gameManager == null) {
			_gameManager = target as Game;
			
		}
		base.OnInspectorGUI ();
		if(GUILayout.Button("Update")) {
			Utility.ScreenSupport.ResetScreenSupport();
			_gameManager.InitializeCameras();
			_gameManager.InitializeScreenSettings();
		}
	}
}
