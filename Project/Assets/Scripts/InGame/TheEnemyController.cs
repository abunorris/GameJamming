using UnityEngine;
using System.Collections;

public class TheEnemyController : TheCharacterController {

	public Vector2 attackIntervalRange = new Vector2(3, 7);

	public override void Init () {
		base.Init ();
	}
}
