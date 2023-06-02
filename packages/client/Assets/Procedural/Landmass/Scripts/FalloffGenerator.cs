using UnityEngine;
using System.Collections;

public class FalloffGenerator : MonoBehaviour {

	[Range(-2f, 2f)]
	public float weight = 1f;
	public bool invert;
	public float falloff = 3;
	public float start = 4f;

	public float[,] GenerateFalloffMap(int width, int height) {
		float[,] map = new float[width,height];

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				float x = i / (float)width * 2 - 1;
				float y = j / (float)height * 2 - 1;

				float value = Mathf.Max (Mathf.Abs (x), Mathf.Abs (y));

				value = Evaluate(value, falloff, start);

				if(invert) {
					value = 1f - value;
				}

				map [i, j] = value;
				
			}
		}

		return map;
	}

	static float Evaluate(float value, float falloff, float start) {
		return Mathf.Pow (value, falloff) / (Mathf.Pow (value, falloff) + Mathf.Pow (start - start * value, falloff));
	}
}
