using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(GestureMatchingController))]
public class GestureMatchingControllerInspector : Editor {
	private GestureMatchingController _gestureMatchingController = null;
	public override void OnInspectorGUI () {
		if(_gestureMatchingController == null) {
			_gestureMatchingController = target as GestureMatchingController;

		}
		base.OnInspectorGUI ();
		if(GUILayout.Button("Update Pen")) {
			_gestureMatchingController.UpdatePenValues();
		}
	}
}
