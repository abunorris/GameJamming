using UnityEngine;
using System.Collections;

public class TheProjectile : MonoElement {

	public Transform targetTransform;
	public float speed = 20f;
	public float distanceFromTarget = 0;

	public override void Init () {
		base.Init ();
	}

	public void UpdateValues() {
		if(targetTransform != null) {
			 distanceFromTarget = Vector2.Distance(cachedTransform.localPosition, targetTransform.localPosition);
		}
	}

}
