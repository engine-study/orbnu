using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		MapGenerator mapGen = (MapGenerator)target;

		if (DrawDefaultInspector ()) {
			if (mapGen.autoUpdate) {
				mapGen.Create ();
			}
		}

		if (GUILayout.Button ("Create")) {
			mapGen.Create();
		}
	}

	void OnValidate() {

		Debug.Log("Editor validate");
	}
}
