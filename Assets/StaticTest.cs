using UnityEngine;
using System.Collections;

public class StaticTest : MonoBehaviour {

	public static float test;

	public float value;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		value = StaticTest.test;
	}
}
