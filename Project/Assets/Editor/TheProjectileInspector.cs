using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TheProjectile))]
public class TheProjectileInspector : Editor {
	private TheProjectile _theProjectile = null;
	public override void OnInspectorGUI () {
		if(_theProjectile == null) {
			_theProjectile = target as TheProjectile;
			_theProjectile.UpdateValues();
		}
		base.OnInspectorGUI ();
		if(GUILayout.Button("Update Values")) {
			_theProjectile.UpdateValues();
		}
	}
}
