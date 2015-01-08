using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheCharacterController : MonoElement {
	public Transform pooledProjectilesContainer;
	public List<TheProjectile> pooledProjectiles;

	public TheCharacterController targetCharacterController; // for Projectile

	public override void Init () {
		base.Init ();
	}

	public void UpdatePooledProjectiles () {
		if(pooledProjectilesContainer == null)
			pooledProjectilesContainer = cachedTransform.FindChild("PooledProjectiles");
		if(pooledProjectilesContainer != null) {
			pooledProjectiles = new List<TheProjectile>(
				pooledProjectilesContainer.GetComponentsInChildren<TheProjectile>(true)
			);
		}
		for(int i = 0; i < pooledProjectiles.Count; i++)
			pooledProjectiles[i].targetTransform = targetCharacterController.cachedTransform;
	}

	public void Attack () {

	}
}
