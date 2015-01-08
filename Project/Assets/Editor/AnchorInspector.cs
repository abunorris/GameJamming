using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Anchor))]
public class AnchorInspector : Editor {
	private Anchor _anchor = null;
	private Anchor.Pivot _pivot;
	public override void OnInspectorGUI () {
		if(_anchor == null) {
			_anchor = target as Anchor;
			_pivot = _anchor.pivot;
			_anchor.UpdateAnchor();
		}
		base.OnInspectorGUI ();
		if(_pivot != _anchor.pivot) {
			_pivot = _anchor.pivot;
			_anchor.UpdateAnchor();
		}
	}
}
