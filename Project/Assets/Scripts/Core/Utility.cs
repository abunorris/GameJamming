using System;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Utility {

	public static class ResoursePaths {
		public const string GestureValuesTextAssetPath = "TextAssets/GestureMatchingValues";
		public const string GestureListingTextAssetPath = "TextAssets/GestureListing";
		public const string EnemyListingTextAssetPath = "TextAssets/EnemyListing";
		public const string EquipmentListingTextAssetPath = "TextAssets/EquipmentListing";
		public const string ItemListingTextAssetPath = "TextAssets/ItemListing";
	}

	public static class ScreenSupport {
		public enum AspectRatioType {
			None = -1,
			Ratio_3x4,
			Ratio_2x3,
			Ratio_3x5,
			Ratio_5x8,
			Ratio_9x16,
			Others
		}

		/// <summary>
		/// The height of the base. Change this to your own base layout screen height
		/// </summary>

		public static float baseAspect = 0.5625f;
		public static Vector3 baseResolution = new Vector3(640f, 1136f, 1f);

		private static AspectRatioType _aspectRatioType = (AspectRatioType)(-1);
		public static AspectRatioType aspectRatioType {
			get {
				if(_aspectRatioType == AspectRatioType.None)
					InitializeScreenSupport();
				return _aspectRatioType;
			}
		}
		private static Vector2 _fixedScreenResoulution = Vector2.one * -1f;
		private static Vector2 fixedScreenResoulution {
			get {
				if(_fixedScreenResoulution == (Vector2.one * -1f))
					InitializeScreenSupport();
				return _fixedScreenResoulution;
			}
		}

		private static float _fixedUIManualWidth = -1f;
		public static float fixedUIManualWidth { 
			get {
				if(_fixedUIManualWidth == -1f)
					InitializeScreenSupport();
				return _fixedUIManualWidth;
			}
		}

		private static float _fixedUIManualHeight = -1f;
		public static float fixedUIManualHeight { 
			get {
				if(_fixedUIManualHeight == -1f)
					InitializeScreenSupport();
				return _fixedUIManualHeight;
			}
		}

		private static Vector3 _scaleFromBaseResolutionX = (Vector3.one * -1f);
		public static Vector3 scaleFromBaseResolutionX { 
			get {
				if(_scaleFromBaseResolutionX == (Vector3.one * -1f))
					InitializeScreenSupport();
				return _scaleFromBaseResolutionX;
			}
		}

		private static Vector3 _scaleFromBaseResolutionY = (Vector3.one * -1f);
		public static Vector3 scaleFromBaseResolutionY { 
			get {
				if(_scaleFromBaseResolutionY == (Vector3.one * -1f))
					InitializeScreenSupport();
				return _scaleFromBaseResolutionY;
			}
		}

		private static Vector2 _scaleFromUnityEditorScreenResolution = (Vector2.one * -1f);
		public static Vector2 scaleFromUnityEditorScreenResolution {
			get {
				return new Vector2(fixedUIManualWidth / Screen.width, fixedUIManualHeight / Screen.height);
			}
		}

		private static Vector3 _rootScale = (Vector3.one * -1f);
		public static Vector3 rootScale {
			get {
				if(_rootScale == (Vector3.one * -1f))
					InitializeScreenSupport();
				return _rootScale;
			}
		}

		private static float _zOffsetFor3Dcamera = default(float);
		public static float zOffsetFor3Dcamera {
			get {
				if(_zOffsetFor3Dcamera == default(float))
					InitializeScreenSupport();
				return _zOffsetFor3Dcamera;
			}
		}



		private static void InitializeScreenSupport () {
			float aspect = Camera.main.aspect;
			float magicError = 0.99f;
			
			float fAspect = baseAspect;
			if(aspect >= (0.75f * magicError)) { //3:4 ipad etc
				_aspectRatioType = AspectRatioType.Ratio_3x4;
				_fixedUIManualHeight = 1024;
			} else if(aspect >= (0.667f * magicError)) { //2:3 iphone4/4s
				_aspectRatioType = AspectRatioType.Ratio_2x3;
				_fixedUIManualHeight = 960;
			} else if(aspect >= (0.625f * magicError)) {//5:8 1200x1920 but must be base to lower
				_aspectRatioType = AspectRatioType.Ratio_5x8;
				_fixedUIManualHeight = 960;
			} else if(aspect >= (0.6f * magicError)) { //3:5 480x800 etc
				_aspectRatioType = AspectRatioType.Ratio_3x5;
				_fixedUIManualHeight = 800;
			} else if (aspect >= (0.5625f * magicError)) {
				_aspectRatioType = AspectRatioType.Ratio_9x16;
				_fixedUIManualHeight = 1136f;
			} else {
				_aspectRatioType = AspectRatioType.Others;
				_fixedUIManualHeight = Screen.height;
			}
			//Debug.Log(_fixedUIManualHeight + " " + fAspect + " " + Mathf.RoundToInt(_fixedUIManualHeight * baseAspect));
			_fixedScreenResoulution = new Vector2(Mathf.RoundToInt(_fixedUIManualHeight * aspect), _fixedUIManualHeight);
			_fixedUIManualWidth = _fixedScreenResoulution.x;
			_scaleFromBaseResolutionX = Vector3.one * (_fixedUIManualWidth / baseResolution.x);
			_scaleFromBaseResolutionY = Vector3.one * (_fixedUIManualHeight / baseResolution.y);
			_rootScale = Vector3.one * (2f / fixedUIManualHeight);
			_zOffsetFor3Dcamera = -984.5f * _scaleFromBaseResolutionY.y;
		}

		public static void ResetScreenSupport () {
			InitializeScreenSupport();
		}


	}
	

	/// <summary>
	/// Helper.
	/// </summary>
	public static class Helper {
		
		public static T CreateComponent<T>(string resourcePath, Transform parent) where T : Component {
			GameObject panelPrefab = Resources.Load(resourcePath) as GameObject;
			T component = default(T);
			if(panelPrefab != null) {
				GameObject panel = GameObject.Instantiate(panelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				if(parent != null)
					panel.transform.parent = parent;
				panel.transform.localScale = Vector3.one;
				component = panel.GetComponent<T>();
				if(component == null) component = panel.AddComponent<T>();
			}else{
				Debug.LogWarning("Can't find prefab (" + resourcePath + ") when creating " + typeof(T).Name);
			}
			return component;
		}
		
		
		public static T CreateComponentWithTransformOptions<T>(string resourcePath, GameObject parentObj, bool willPersistTransformEvenOnParent = true, bool isGlobal = true) where T : Component {
			GameObject tempGO = Resources.Load(resourcePath) as GameObject;
			T component = default(T);
			
			if(willPersistTransformEvenOnParent) {
				Vector3 pos = tempGO.transform.GetPosition(isGlobal);
				Quaternion rot = tempGO.transform.GetRotation(isGlobal);
				Vector3 scale = tempGO.transform.localScale;
				if(parentObj != null)
					tempGO.transform.parent = parentObj.transform;
				tempGO.transform.SetPosition(pos, isGlobal);
				tempGO.transform.SetRotation(rot, isGlobal);
				tempGO.transform.localScale = scale;
			} else {
				if(parentObj != null)
					tempGO.transform.parent = parentObj.transform;
				tempGO.transform.position = Vector3.zero;
				tempGO.transform.rotation = Quaternion.identity;
				tempGO.transform.localScale = Vector3.one;
			}
			if((component = tempGO.GetComponent<T> ()) == null)
				component = tempGO.AddComponent<T>();
			return component;
		}
		
		public static GameObject CreatePrefab(string resourceName, GameObject parentGO = null, bool willRetainTransform = false, bool isLocal = true, int layer = -1, bool dontDestroyOnLoad = false) {
			GameObject Prefab = Resources.Load(resourceName) as GameObject;
			GameObject panel = null;
			if(Prefab != null) {
				panel = GameObject.Instantiate (Prefab) as GameObject;
				if (parentGO != null)
					panel.transform.parent = parentGO.transform;
				if (!willRetainTransform) {
					panel.transform.SetPosition(Vector3.zero, !isLocal);
					panel.transform.rotation = Quaternion.identity;
					panel.transform.localScale = Vector3.one;
				}
				if (layer > 0) {
					Transform[] childrenTrans = panel.GetComponentsInChildren<Transform> ();
					for(int i = 0; i < childrenTrans.Length; i++)
						childrenTrans[i].gameObject.layer = layer;
				}
				if(dontDestroyOnLoad)
					GameObject.DontDestroyOnLoad (panel);
			}else{
				Debug.LogWarning("Can't find prefab \"" + resourceName);
			}
			return panel;
		}
		
		
		public static GameObject GetChild (this GameObject parent, string name) {
			Transform child = parent.transform.FindChild(name);
			if(child != null) {
				return child.gameObject;
			}else{
				Debug.LogWarning(parent.name + " has no child \"" + name + "\"");
				return null;
			}
		}
		
		public static GameObject CreateChild (this GameObject parent, string name, int layer = -1) {
			GameObject child = new GameObject (name);
			if (layer > -1)
				child.layer = layer;
			if (parent != null)
				child.transform.parent = parent.transform;
			else
				Debug.LogWarning ("GameObject is Created but has no Parent Game Object");
			return child;
		}
		
		
		public static void SetAlpha (this Material material, float value) {	
			Color color = material.color;
			color.a = value;
			material.color = color;
		}
		
		public static void SetGameObjectActive(this Component component, bool flag) {
			component.gameObject.SetActive (flag);
		}
		
		public static void SetPosition(this Transform mTransform, Vector3 mPosition, bool isGlobal = true) {
			//Debug.LogError (isGlobal + " " + mPosition);
			
			if (isGlobal)
				mTransform.position = mPosition;
			else
				mTransform.localPosition = mPosition;
		}
		
		public static Vector3 GetPosition(this Transform mTransform, bool isGlobal = true) {
			Vector3 tempPos = Vector3.zero;
			if (isGlobal)
				tempPos = mTransform.position;
			else
				tempPos = mTransform.localPosition;
			return tempPos;
		}
		
		public static Quaternion GetRotation(this Transform mTransform, bool isGlobal = true) {
			Quaternion tempRot = Quaternion.identity;
			if (isGlobal)
				tempRot = mTransform.rotation;
			else
				tempRot = mTransform.localRotation;
			return tempRot;
		}
		
		public static void SetRotation(this Transform mTransform, Vector3 mRotEuler, bool isGlobal = true) {
			if (isGlobal)
				mTransform.rotation = Quaternion.Euler(mRotEuler);
			else
				mTransform.localRotation = Quaternion.Euler(mRotEuler);
		}
		
		public static void SetRotation(this Transform mTransform, Quaternion mRotation, bool isGlobal = true) {
			if (isGlobal)
				mTransform.rotation = mRotation;
			else
				mTransform.localRotation = mRotation;
		}
		
		
		public static Vector3 getSineWavePoints (Vector3 moveFr, Vector3 moveT, float ratio, 
		                                         float ampli = 50f, float centerAmpli = 0f, float frequency = 1f, float phase = 0f) {
			Vector3 tempPos = Vector3.zero;
			if (Mathf.Abs (moveFr.x - moveT.x) > Mathf.Abs (moveFr.y - moveT.y)) {
				tempPos.x = Mathf.Lerp (moveFr.x, moveT.x, ratio);
				tempPos.y = Mathf.Lerp (moveFr.y, moveT.y, ratio) + (ampli * Mathf.Sin ((((Mathf.PI * 2f) * frequency) * ratio) - phase) - centerAmpli);
			} else {
				tempPos.x = Mathf.Lerp (moveFr.x, moveT.x, ratio) + (ampli * Mathf.Sin ((((Mathf.PI * 2f) * frequency) * ratio) - phase) - centerAmpli);
				tempPos.y = Mathf.Lerp (moveFr.y, moveT.y, ratio);
			}
			return tempPos;
		}
		
		public static Vector3 getCosineWavePoints (Vector3 moveFr, Vector3 moveT, float ratio, 
		                                           float ampli = 50f, float centerAmpli = 0f, float frequency = 1f, float phase = 0f) {
			Vector3 tempPos = Vector3.zero;
			if (Mathf.Abs (moveFr.x - moveT.x) > Mathf.Abs (moveFr.y - moveT.y)) {
				tempPos.x = Mathf.Lerp (moveFr.x, moveT.x, ratio);
				tempPos.y = Mathf.Lerp (moveFr.y, moveT.y, ratio) + (ampli * Mathf.Cos ((((Mathf.PI * 2f) * frequency) * ratio) - phase) - centerAmpli);
			} else {
				tempPos.x = Mathf.Lerp (moveFr.x, moveT.x, ratio) + (ampli * Mathf.Cos ((((Mathf.PI * 2f) * frequency) * ratio) - phase) - centerAmpli);
				tempPos.y = Mathf.Lerp (moveFr.y, moveT.y, ratio);
			}
			return tempPos;
		}
		
		public static Vector2 GetRandomPointFromPerpendicularLine (Vector2 p1, Vector2 p2, float thresholdX, float commonPointRatio = 0.5f) {
			Vector2 midpoint = (p2 - p1) * commonPointRatio;
			float origLineSlope = (p2.y - p1.y) / (p2.x - p1.x);
			float mSlope = -(1 / origLineSlope); 
			
			Vector2 tempVector = Vector2.zero;
			float x = UnityEngine.Random.Range (-thresholdX, thresholdX);
			float y = mSlope * (x - midpoint.x) + midpoint.y;
			return new Vector2 (x, y);
		}
		
		public static Vector3 GetRandomInBetweenPoint(Vector3 p1, Vector3 p2, Vector3 offset = default(Vector3)) {
			Vector3 resultVector = Vector3.zero;
			
			if (p1.x > p2.x)
				resultVector.x = UnityEngine.Random.Range (p2.x - offset.x, p1.x + offset.x);
			else
				resultVector.x = UnityEngine.Random.Range (p1.x - offset.x , p2.x + offset.x);
			
			if (p1.y > p2.y)
				resultVector.y = UnityEngine.Random.Range (p2.y - offset.y, p1.y + offset.y);
			else
				resultVector.y = UnityEngine.Random.Range (p1.y - offset.y, p2.y + offset.y);
			
			if (p1.z > p2.z)
				resultVector.z = UnityEngine.Random.Range (p2.z - offset.z, p1.z + offset.z);
			else
				resultVector.z = UnityEngine.Random.Range (p1.z - offset.z, p2.z + offset.z);
			
			return resultVector;
		}
		
		public static Vector2 GetRandomInBetweenPoint2D(Vector2 p1, Vector2 p2, Vector2 offset = default(Vector2)) {
			Vector2 resultVector = Vector3.zero;
			
			if (p1.x > p2.x)
				resultVector.x = UnityEngine.Random.Range (p2.x - offset.x, p1.x + offset.x);
			else
				resultVector.x = UnityEngine.Random.Range (p1.x - offset.x , p2.x + offset.x);
			
			if (p1.y > p2.y)
				resultVector.y = UnityEngine.Random.Range (p2.y - offset.y, p1.y + offset.y);
			else
				resultVector.y = UnityEngine.Random.Range (p1.y - offset.y, p2.y + offset.y);
			return resultVector;
		}
		
		public static string CompareMethods(params System.Action[] actions) {
			string result = default(string);
			int actionsLength = actions.Length;
			System.Diagnostics.Stopwatch[] stopWatches = new System.Diagnostics.Stopwatch[actionsLength];
			for(int i = 0; i < actionsLength; i++) {
				System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
				System.Action action = actions[i];
				stopWatch.Start();
				action();
				stopWatch.Stop();
				
				stopWatches[i] = stopWatch;
				result += action.Method.Name + " Ticks: " + stopWatch.ElapsedTicks + "\r\n";
			}
			Debug.Log(result);
			//Debug.LogError(result);
			return result;
		}
		
		
		public static void ChangeHierarchyLayer(Transform root, int layer) {
			root.gameObject.layer = layer;
			foreach(Transform child in root)
				ChangeHierarchyLayer(child, layer);
		}
		
		public static int[] GetRandomNonRepeatingNumbers(int count, int fromNum, int toNum, params int[] exceptNs) {
			List<int> numbers = new List<int>();
			List<int> exceptNums = new List<int> (exceptNs);
			for(int i = fromNum; i < toNum + 1; i++) {
				if(!exceptNums.Contains(i))
					numbers.Add(i);
			}
			int[] resultNumbers = new int[count];
			for(int i = 0; i < count; i++) {
				int randIndex = UnityEngine.Random.Range(0, numbers.Count);
				//Debug.LogError(randIndex);
				resultNumbers[i] = numbers[randIndex];
				numbers.RemoveAt(randIndex);
			}
			return resultNumbers;
		}
		
		
		public static object GetProperty<T>(this T obj, string name) where T : class
		{
			System.Type t = typeof(T);
			return t.GetProperty(name).GetValue(obj, null);
		}
		
		private static int _targetFrameRate = 0;
		public static int GetTargetFrameRate () {
			if (_targetFrameRate != Application.targetFrameRate) {
				_targetFrameRate = Application.targetFrameRate;
			}
			return _targetFrameRate;
		}
		
		public static float SmootherStep(float edge0, float edge1, float x)
		{
			// Scale, and clamp x to 0..1 range
			x = Mathf.Clamp((x - edge0)/(edge1 - edge0), 0.0f, 1.0f);
			// Evaluate polynomial
			return x*x*x*(x*(x*6 - 15) + 10);
		}
		public static T ParseEnum<T>(string value) {
			return (T) System.Enum.Parse(typeof(T), value, true);
		}
		
		public static T[] RemoveAt<T>(this T[] source, int index) {
			T[] dest = new T[source.Length - 1];
			if( index > 0 )
				Array.Copy(source, 0, dest, 0, index);
			
			if( index < source.Length - 1 )
				Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
			
			return dest;
		}
		
		public static float bounceUpdate(float start, float end, float value){
			value /= 1f;
			end -= start;
			if (value < (1 / 2.75f)){
				return end * (7.5625f * value * value) + start;
			}else if (value < (2 / 2.75f)){
				value -= (1.5f / 2.75f);
				return end * (7.5625f * (value) * value + .75f) + start;
			}else if (value < (2.5 / 2.75)){
				value -= (2.25f / 2.75f);
				return end * (7.5625f * (value) * value + .9375f) + start;
			}else{
				value -= (2.625f / 2.75f);
				return end * (7.5625f * (value) * value + .984375f) + start;
			}
		}
		
		public static string GetRandomString()
		{
			string path = Path.GetRandomFileName();
			path = path.Replace(".", ""); // Remove period.
			return path;
		}
		
		public static T ConvertObjectToGenericType<T>(object obj) {
			Type type = typeof(T);
			if (type.IsEnum || type == typeof(Int32)) {
				int result = -1;
				if (int.TryParse (obj.ToString (), out result)) {
					return (T)(object)Convert.ToInt32 (obj);
				} else {
					return ParseEnum<T> (obj.ToString ());
				}
			} else if (type == typeof(Single)) {
				return (T)(object)Convert.ToSingle (obj);
			} else if (type == typeof(Boolean)) {
				return (T)(object)Convert.ToBoolean (obj);
			} else if (type == typeof(string)) {
				return (T)(object)Convert.ToString (obj);
			} else if (type == typeof(DateTime)) {
				return (T)(object)Convert.ToDateTime (obj);
			} else if (type == typeof(Int64)) {
				return (T)(object)Convert.ToInt64 (obj);
			}
			return default(T);
		}
		
		public static bool CompareObjects<T>(object obj1, object obj2) {
			Type type = typeof(T);
			if (type.IsEnum || type == typeof(Int32)) {
				//return (Convert.ToInt32 (obj1) == Convert.ToInt32(obj2));
				int result1 = -1;
				int result2 = -1;
				if (int.TryParse (obj1.ToString (), out result1) && int.TryParse (obj2.ToString (), out result2))
					return Convert.ToInt32 (obj1) == Convert.ToInt32(obj2);
				else 
					return ParseEnum<T> (obj1.ToString ()).Equals(ParseEnum<T> (obj2.ToString ()));
			} else if (type == typeof(Single)) {
				return Convert.ToSingle (obj1) == Convert.ToSingle (obj2);
			} else if (type == typeof(Boolean)) {
				return Convert.ToBoolean (obj1) == Convert.ToBoolean (obj2);
			} else if (type == typeof(string)) {
				return Convert.ToString (obj1) == Convert.ToString (obj2);
			} else if (type == typeof(DateTime)) {
				return Convert.ToDateTime (obj1) == Convert.ToDateTime (obj2);
			} else if (type == typeof(Int64)) {
				return Convert.ToInt64 (obj1) == Convert.ToInt64 (obj2);
			}
			return false;
		}
		
		public static string AddCharactersBeforeLargeCaps (string val, string characters) {
			string tParStr = val;
			string newParStr = tParStr;
			int length = tParStr.Length;
			for (int j = 1; j < length; j++) {
				if (char.IsUpper (tParStr [j]) || char.IsDigit (tParStr [j])) {
					newParStr = tParStr.Insert (j, characters);	                     
				}
			}
			return newParStr; 
		}


		public static string ColorToHex(Color32 color)
		{
			string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
			return hex;
		}
		
		public static Color HexToColor(string hex)
		{
			byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
			return new Color32(r,g,b, 255);
		}

		public static string SetFloatToStringShortcut (float value, int shortCutThreshold) {
			string valStr = value.ToString("#");
			int valStrLength = valStr.Length;
			int dotIndex = 0;
			int minThreshold = 3;
			int maxThreshold = 15;
			shortCutThreshold = Mathf.Clamp(shortCutThreshold, minThreshold, maxThreshold);

			if(valStrLength > shortCutThreshold) {
				string[] units = new string[]{
					"K", "M", "B", "T", "Q", "Qui"
				};
				int diff = valStrLength - shortCutThreshold;
				//Debug.LogError(valStrLength + " " + shortCutThreshold + " " + diff);
				int mod = (diff /*- 1*/) % 3;
				valStr = value.ToString("#");
				int preDotIndex = mod == 0 ? 3 : mod;
				string unit = units[ Mathf.FloorToInt((diff / minThreshold) + ((shortCutThreshold / minThreshold) - 1))];
				valStr = valStr.Substring(0, preDotIndex) + "." + valStr.Substring(preDotIndex, 3) + " " + unit;
			} else if (value != 0) {
				valStr = value.ToString("#,###");
			} else {
				valStr = "0"; //BECAUSE 0 is not printed properly.
			}
			return valStr;
		}

		/// <summary>
		/// Factorial the specified val. Check for more information
		/// </summary>
		/// <param name="val">Value.</param>
		public static int Factorial (int val) {
			int threshold = 100;
			val = Mathf.Clamp(val, 1, threshold);
			int result = 1;
			for(int i = val; i > 0; i--) {
				result *= i;
			}
			return result;
		}
	}

	public static class UnityIOS {
		[DllImport ("__Internal")]
		private static extern void _unityIOSPause(bool flag);
		public static void Pause (bool flag) {
			_unityIOSPause (flag);
		}
	}
}
