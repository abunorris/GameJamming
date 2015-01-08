using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InGameController : MonoElement {
	public TheCharacterController player;
	public TheEnemyController enemy;
	public int gesturePenLayerMask;
	public Camera rootCamera;
	public bool isDrawGestureMode = false;
	public GestureMatchingController gestureMatchingController;

	public override void Init () {
		base.Init ();
		rootCamera = gameManager.cameraMap["RootCamera"];
	}

	public override void OnUpdate () {
		base.OnUpdate ();
		if(Input.GetMouseButtonDown(0)) {
			RaycastHit raycastHit;
			if(Physics.Raycast(rootCamera.ScreenToWorldPoint(Input.mousePosition), 
			                   Vector3.forward, out raycastHit, float.MaxValue, 1 << gesturePenLayerMask)) {
				isDrawGestureMode = true;
				gestureMatchingController.BeginGesture();
			}
		}
		if(Input.GetMouseButton(0)) {
			if(isDrawGestureMode) {
				Debug.DrawRay(rootCamera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 200f, Color.red);
				RaycastHit raycastHit;
				if(Physics.Raycast(rootCamera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out raycastHit, 
				                   float.MaxValue, 1 << gesturePenLayerMask)) {
					gestureMatchingController.UpdateGesture();
				} else {
					gestureMatchingController.EndGesture();
					isDrawGestureMode = false;
				}
			}
		}

		if(Input.GetMouseButtonUp(0)) {
			if(isDrawGestureMode) {
				gestureMatchingController.EndGesture();
				isDrawGestureMode = false;
			}
		}
	}

	private IEnumerator EnemyAttackCoroutine () {
		enemy.Attack();
		yield return new WaitForSeconds(
			Random.Range(enemy.attackIntervalRange.x, enemy.attackIntervalRange.y)
		);
	}


	public override void AfterDestroy () {
		base.AfterDestroy ();
	}

	public void Test () {
	}
	
}
