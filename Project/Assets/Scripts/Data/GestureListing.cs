using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GestureListing {
	public class GestureInformation { 
		public string name;
		public string moves;
		public float damage;
		public float mana;
	}

	private static Dictionary<string, GestureInformation> _gestureInformationMap = null;
	public static Dictionary<string, GestureInformation> gestureInformationMap {
		get {
			if(_gestureInformationMap == null) {
				_gestureInformationMap = new Dictionary<string, GestureInformation>();
			 	TextAsset textAsset = Resources.Load(Utility.ResoursePaths.GestureListingTextAssetPath) as TextAsset;
				string[] rows = textAsset.text.Split(new char[] {'\n'});
				for(int i = 1; i < rows.Length; i++) {
					GestureInformation gestureInformation = new GestureInformation();
					string[] values = rows[i].Split(new char[]{','});
					gestureInformation.name = values[0].Trim();
					gestureInformation.moves = values[1].Trim();
					gestureInformation.damage = float.Parse(values[2].Trim());
					gestureInformation.mana = float.Parse(values[3].Trim());
					_gestureInformationMap.Add(gestureInformation.name, gestureInformation);
				}
			}
			return _gestureInformationMap;
		}
	}

}
