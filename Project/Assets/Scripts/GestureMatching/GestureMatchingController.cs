using UnityEngine;
using GestureMatching;
using System.Collections;
using System.Collections.Generic;

public class GestureMatchingController : MonoElement {

	public GestureMatching.Action action;
	public AnglesMap anglesMap = new AnglesMap();
	public List<Gesture> gestures = new List<Gesture>();
	public Information information;
	public int informationIndex = 0;
	public Pen pen = new Pen();

	public Gesture lastGesture = null;

	public override void Init () {
		base.Init ();
		information = Information.GetInformations(Resources.Load(Utility.ResoursePaths.GestureValuesTextAssetPath) as TextAsset) [informationIndex];
		anglesMap = new AnglesMap(information.angleCount, information.angleStepPrecision);
		action = new GestureMatching.Action(information.pixelPrecision, anglesMap.GetMove);
		gestures = new List<Gesture>(information.gestures);
		pen.camera = gameManager.cameraMap["RootCamera"];
	}

	public void UpdatePenValues () {
		pen.UpdateValues();
	}
	
	public override void AfterDestroy () {
		base.AfterDestroy ();
	}


	public void BeginGesture () {
		Vector3 mousePos = Input.mousePosition;
		action.Begin(mousePos);
		pen.Enable (mousePos);
	}

	public void UpdateGesture () {
		Vector3 mousePos = Input.mousePosition;
		action.Update(mousePos);
		pen.Update(mousePos);
	}

	public void EndGesture () {
		action.End();
		pen.Disable();
		MatchGesture();
	}

	public void MatchGesture () {
		int bestCost = information.maxCost;
		int cost = 0;
		int gesturesCount = gestures.Count;
		Gesture tGesture = null;
		int[] actionMoves = action.moves.ToArray();
		if(actionMoves.Length > 0) {
			for(int i = 0; i < gesturesCount; i++) {
				cost = CostLeven(gestures[i].moves, actionMoves);
				if(cost <= information.fiability) {
					if (cost < bestCost){
						bestCost = cost;
						tGesture = gestures[i];
					}
				}
			}
		}
		if(tGesture != null) {
			Debug.LogWarning("Gesture-Match: " + tGesture.name);
			lastGesture = tGesture;
			// MATCH
		} else {
			//NO MATCH
			lastGesture = null;
			Debug.LogWarning("Gesture-No Match");
		}
	}


	#region Computation levenshtein distance
	public int CostLeven (int[] array1, int[] array2) {
		int array1Length = array1.Length;
		int array2Length = array2.Length;
		if(array1[0] == -1) {
			return array2Length == 0 ? 0: 100000;
		}
		int[,] d = Fill2DTable(array1Length + 1, array2Length + 1, 0); //new List<int[]>();
		int[,] w = Fill2DTable(array1Length + 1, array2Length + 1, 0);
		for(int i = 1; i <= array1Length; i++) {
			for(int j = 1; j < array2Length; j++) {
				d[i, j] = GetAngleDifference(array1[i-1], array2[j-1]);
			}
		}

		for(int j = 1; j <= array2Length; j++) w[0, j] = 100000;
		for(int i = 1; i <= array1Length; i++) w[i, 0] = 100000;
		w[0, 0] = 0;

		int cost = 0;
		int pa, pb, pc;

		for (int i = 1; i <= array1Length; i++){
			for (int j = 1; j < array2Length; j++){
				cost= d[i, j];
				pa= w[i-1, j] + cost;
				pb= w[i, j - 1] + cost;
				pc= w[i-1, j - 1] + cost;
				w[i, j] = Mathf.Min(
					Mathf.Min(pa,pb), 
					pc);
			}
		}

		return w[array1Length - 1, array2Length - 1];
	}


	public int GetAngleDifference (int angle1, int angle2) {
		int diff = Mathf.Abs(angle1 - angle2);
		if(diff > information.angleCount / 2) 
			diff = information.angleCount - diff;
		return diff;
	}

	public int[,] Fill2DTable (int width, int height, int val)  {
		int[,] table = new int[width, height];
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {
				table[i, j]= val;
			}
		}
		return table;
	}
	#endregion
}
