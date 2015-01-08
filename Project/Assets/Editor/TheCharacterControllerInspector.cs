using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TheCharacterController),true)]
public class TheCharacterControllerInspector : Editor {
	private TheCharacterController _theCharacterController = null;
	public override void OnInspectorGUI () {
		if(_theCharacterController == null) {
			_theCharacterController = target as TheCharacterController;
			_theCharacterController.UpdatePooledProjectiles();
		}
		base.OnInspectorGUI ();
	}
}
