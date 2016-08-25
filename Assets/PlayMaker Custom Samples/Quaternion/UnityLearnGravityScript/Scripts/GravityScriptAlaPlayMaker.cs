// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Ecosystem.Samples
{
	/// <summary>
	/// Gravity script ala PlayMaker. This script is the same as https://unity3d.com/learn/tutorials/topics/scripting/quaternions 
	/// but reformated to be following how Actions where created to reproduce the exact same behaviour in PlayMaker.
	/// So each action in the Gravity Fsm State are commented here for clarity.
	/// </summary>
	public class GravityScriptAlaPlayMaker : MonoBehaviour 
	{

		public GameObject Target;
		public float Speed = 3f;
		public Vector3 Target_Offset = new Vector3(0,1.5f,0);

		Vector3 Current_Position;
		Quaternion Current_Rotation;
		float Delta_Time;
		Quaternion Look_Rotation;
		Quaternion New_Rotation;
		Vector3 Relative_Position;
		Vector3 Target_Position;
		
		void Update () 
		{

			// ACTION: Get Position
			Target_Position = Target.transform.position;

			// ACTION: Get Position
			Current_Position = transform.position;

			// ACTION: Vector3 Add
			Target_Position += Target_Offset;

			// ACTION: Vector3 Operator
			Relative_Position = Target_Position - Current_Position;

			// ACTION: Quaternion Look Rotation
			Look_Rotation = Quaternion.LookRotation(Relative_Position);

			// ACTION: Get Rotation
			Current_Rotation = transform.localRotation;

			// ACTION: GetTime
			Delta_Time = Time.deltaTime;

			// ACTION: Quaternion Slerp
			New_Rotation = Quaternion.Slerp(Current_Rotation, Look_Rotation, Delta_Time );

			// ACTION: Set Rotation
			transform.localRotation = New_Rotation;

			// ACTION: Translate
			transform.Translate(0, 0, Speed * Delta_Time);
		}
	}
}