using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GestureMatching {
	[System.Serializable]
	public class Pen {
		public GameObject container;
		public Transform containerTransform;

		public GameObject gameObject;
		public Transform transform;
		public ParticleSystem[] particleSystems;
		public Camera camera;
		public Transform transformBasis;

		public LineRenderer lineRenderer;
		public Vector3 lastPosition;
		public float distanceTolerance;
		public float zOffset = 0;
		public int lineRendererPositionCount;


		public Pen(){}
		public Pen (GameObject container, Camera camera, Transform transformBasis) {
			this.container = container;
			this.camera = camera;
			this.transformBasis = transformBasis;
			UpdateValues();
		}
		public void UpdateValues () {
			if(container != null) {
				this.containerTransform = container.transform;
				this.transform = containerTransform.FindChild("GesturePen");
				this.gameObject = transform.gameObject;
				this.particleSystems = this.transform.
					GetComponentsInChildren<ParticleSystem>(true);
				                                                                                        
				this.lineRenderer = this.containerTransform.
					FindChild("GPLineRenderer").GetComponent<LineRenderer>();

				if(camera != null)
					zOffset = camera.transform.localPosition.z;
			}
		}
		public void Enable (Vector3 position) {
			position.z = Utility.ScreenSupport.rootScale.x 
				* zOffset;
			transform.localPosition = lastPosition =
				transformBasis.InverseTransformPoint(camera.ScreenToWorldPoint(position));
			container.SetActive(true);
			lineRenderer.SetVertexCount(lineRendererPositionCount = 1);
			lineRenderer.SetPosition(lineRendererPositionCount - 1, 
			                         lastPosition = transform.localPosition);
		}
		public void Update (Vector3 position) {
			position.z = Utility.ScreenSupport.rootScale.x 
				* -zOffset;
			transform.localPosition =  transformBasis.InverseTransformPoint(camera.ScreenToWorldPoint(position));
			if(Vector2.Distance(transform.localPosition, lastPosition) >= distanceTolerance) {
				lineRenderer.SetVertexCount(++lineRendererPositionCount);
				lineRenderer.SetPosition(lineRendererPositionCount - 1, 
				                         lastPosition = transform.localPosition);
			}

		}
		public void Disable () {
			container.SetActive(false);
		}
	}

	[System.Serializable]
	public class Gesture {
		public string name;
		public System.Action matchHandler;
		public int[] moves;
		public Gesture (string name, System.Action matchHandler, params int[] gestureIndeces) {
			this.name = name;
			this.matchHandler = matchHandler;
			this.moves = gestureIndeces;
		}
		public Gesture (string name, string gestureStr, System.Action matchHandler = null) {
			this.name = name;
			this.matchHandler = matchHandler;
			List<int> tGestureIndeces = new List<int>();
			for(int i = 0; i < gestureStr.Length; i++) {
				tGestureIndeces.Add(gestureStr[i] == '.' ? -1: 
				                    int.Parse(
					string.Format("{0}", (gestureStr[i]))
					)
				                    );
			}
			moves = tGestureIndeces.ToArray();
		}
		
	}
	
	[System.Serializable]
	public class Action {
		public bool isActive = false;
		public List<int> moves = new List<int>();
		public List<Vector2> points = new List<Vector2>();
		public Vector2 lastPoint = Vector2.zero;
		public Rect rect = new Rect(float.PositiveInfinity,
		                            float.NegativeInfinity,
		                            float.PositiveInfinity,
		                            float.NegativeInfinity);

		private float _pixelPrecision = 0;
		private System.Func<Vector2, int> _getMoveFunc = null;

		public Action (float _pixelPrecision, System.Func<Vector2, int> _getMoveFunc) {
			this._pixelPrecision = _pixelPrecision;
			this._getMoveFunc = _getMoveFunc;
		}

		//Mouse Down
		public void Begin (Vector2 point) {
			Vector2 scaleFromUnityEditorScreenResolution = Utility.ScreenSupport.scaleFromUnityEditorScreenResolution;
			point.y = Utility.ScreenSupport.fixedUIManualHeight - (point.y * scaleFromUnityEditorScreenResolution.y);
			//point.y *= scaleFromUnityEditorScreenResolution.y;
			point.x *= scaleFromUnityEditorScreenResolution.x;
			lastPoint = point;
			points.Clear();
			moves.Clear();
			isActive = true;

			//Debug.LogError(lastPoint);

		}


		//Mouse hold
		public void Update (Vector2 point) {
			Vector2 scaleFromUnityEditorScreenResolution = Utility.ScreenSupport.scaleFromUnityEditorScreenResolution;
			point.y = Utility.ScreenSupport.fixedUIManualHeight - (point.y * scaleFromUnityEditorScreenResolution.y);
			//point.y *= scaleFromUnityEditorScreenResolution.y;
			point.x *= scaleFromUnityEditorScreenResolution.x;

			Vector2 diff = point - lastPoint; //lastPoint - point;
			float squareDistance = (diff.x * diff.x) + (diff.y * diff.y);
			float squarePrecicion = (_pixelPrecision * _pixelPrecision);
			//Debug.LogError(squareDistance + " " + squarePrecicion);
			if(squareDistance > squarePrecicion) {
				points.Add(point);
				moves.Add(_getMoveFunc(diff));
				lastPoint = point;
				if (point.x < rect.xMin) rect.xMin = point.x;
				if (point.x > rect.xMax) rect.xMax = point.x;
				if (point.y < rect.yMin) rect.yMin = point.y;
				if (point.y > rect.yMax) rect.yMax = point.y;
			}
		}
		//Mouse up
		public void End () {
			isActive = false;

			string movesStr = "";
			for(int i = 0; i < moves.Count; i++)
				movesStr += moves[i];
			Debug.LogWarning(movesStr);
		}
	}


	[System.Serializable]
	public class AnglesMap {
		public int count = 0;
		public float radPerIndex;
		public int stepPrecision;
		public List<int> values = new List<int>();

		public AnglesMap () {}
		public AnglesMap (int count, int stepPrecision) {
			this.count = count;
			this.stepPrecision = stepPrecision;

			radPerIndex = Mathf.PI * 2 / (float)count;


			float step = Mathf.PI * 2f / (float)stepPrecision;

			float fr = -radPerIndex / 2f;
			float to = (Mathf.PI * 2) + fr;
			//float to = -radPerIndex / 2f;
			//float fr = (Mathf.PI * 2) + to - step;
			values = new List<int>();
			for(float i = fr; i <= to; i += step) {
				float part = Mathf.Floor((i + (radPerIndex / 2.0f)) / radPerIndex);
				values.Add((int)part);
			}
			Debug.LogWarning(radPerIndex + " " + step + " " + fr + " " + to);

		}

		/// <summary>
		/// Gets the angle from position. Uses Atan2
		/// </summary>
		/// <returns>The angle from position.</returns>
		public int GetMove(float dx, float dy) {
			float angle = (Mathf.Atan2(dy, dx) + radPerIndex / 2f);

			if(angle < 0) angle += Mathf.PI * 2f;
			int aIndex = (int)Mathf.Floor(angle / (Mathf.PI * 2f) * stepPrecision);
			//Debug.LogWarning(dx + " " + dy + " " + angle + " " + aIndex);
			return values[aIndex];
		}
		public int GetMove (Vector2 d) {return GetMove(d.x, d.y);}
	}

	[System.Serializable]
	public class Information {
		public int angleCount;
		public int angleStepPrecision;
		public float pixelPrecision;
		public float fiability;
		public int maxCost;
		public Gesture[] gestures;
		public Information (int angleCount, int angleStepPrecision, float pixelPrecision,
		                    float fiability, int maxCost, params Gesture[] gestures) {
			this.angleCount = angleCount;
			this.angleStepPrecision = angleStepPrecision;
			this.pixelPrecision = pixelPrecision;
			this.fiability = fiability;
			this.maxCost = maxCost;
			this.gestures = gestures;
		}

		public static List<Information> GetInformations(TextAsset textAsset) {
			List<Information> informations = new List<Information>();
			string[] rows = textAsset.text.Split(new char[] {'\n'});
			for(int i = 1; i < rows.Length; i++) {

				string[] values = rows[i].Split(new char[] {','}); 

				List<Gesture> gestures = new List<Gesture>();
				int gestureStartIndex = 5;
				for(int j = gestureStartIndex; j < values.Length; j += 2) {
					gestures.Add(new Gesture(values[j].Trim(), values[j + 1].Trim()));
				}
				Information information = new Information(int.Parse(values[0]),
				                                          int.Parse(values[1]), 
				                                          float.Parse(values[2]),
				                                          float.Parse(values[3]),
				                                          int.Parse(values[4]), 
				                                          gestures.ToArray());
				informations.Add(information);
			}
			return informations;
		}

	}
}