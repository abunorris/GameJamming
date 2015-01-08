using UnityEngine;
using System.Collections;
using Utility;

public class Anchor : MonoElement {
	public enum Pivot {
		Center, Left, Right, Top, Bottom,
		TopLeft, TopRight, BottomLeft, BottomRight
	}

	public Pivot pivot = Pivot.Center;


	public override void Init () {
		base.Init ();
		UpdateAnchor ();
	}

	public void UpdateAnchor () {
		switch(pivot) {
		case Pivot.Center:
			cachedTransform.localPosition = Vector3.zero;
			break;
		case Pivot.Bottom:
			cachedTransform.localPosition = new Vector3(0, -ScreenSupport.fixedUIManualHeight / 2f);
			break;
		case Pivot.Left:
			cachedTransform.localPosition = new Vector3(-ScreenSupport.fixedUIManualWidth / 2f, 0);

			break;
		case Pivot.Top:
			cachedTransform.localPosition = new Vector3(0, ScreenSupport.fixedUIManualHeight / 2f);
			break;
		case Pivot.Right:
			cachedTransform.localPosition = new Vector3(ScreenSupport.fixedUIManualWidth / 2f, 0);
			break;
		case Pivot.BottomLeft:
			cachedTransform.localPosition = new Vector3(-ScreenSupport.fixedUIManualWidth / 2f, -ScreenSupport.fixedUIManualHeight / 2f);
			break;
		case Pivot.BottomRight:
			cachedTransform.localPosition = new Vector3(ScreenSupport.fixedUIManualWidth / 2f, -ScreenSupport.fixedUIManualHeight / 2f);
			break;
		case Pivot.TopLeft:
			cachedTransform.localPosition = new Vector3(-ScreenSupport.fixedUIManualWidth / 2f, ScreenSupport.fixedUIManualHeight / 2f);
			break;
		case Pivot.TopRight:
			cachedTransform.localPosition = new Vector3(ScreenSupport.fixedUIManualWidth / 2f, ScreenSupport.fixedUIManualHeight / 2f);
			break;
		}
	}

}
