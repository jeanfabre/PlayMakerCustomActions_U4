using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMakerEditor
{
    	[CustomActionEditor(typeof (HutongGames.PlayMaker.Actions.CircleCast2d))]

	public class CircleCast2dActionEditor : CustomActionEditor
    	{
	        public override bool OnGUI()
	        {
			_action = (HutongGames.PlayMaker.Actions.CircleCast2d)target;

			bool _changed =  DrawDefaultInspector();
			if (_changed)
			{
				_action.ComputeRayCastProperties(out _start,out _direction,out _length);
			}

			return _changed;
	        }

		CircleCast2d _action;

		private Vector3 _start;
		private Vector3 _direction;
		private float _length;

		Vector3[] startCircle = new Vector3[45];
		Vector3[] endCircle = new Vector3[45];

	        public override void OnSceneGUI()
	        {

			_action = (HutongGames.PlayMaker.Actions.CircleCast2d)target;

			if (!_action.debug.Value)
			{
				return;
			}

			_action.ComputeRayCastProperties(out _start,out _direction,out _length);

			Handles.color = _action.debugColor.Value;


			GetWireDiscPoints(_start,_direction,_action.radius.Value,startCircle);

			DrawPoints(startCircle);

			GetWireDiscPoints(_start+_direction*_length,_direction,_action.radius.Value,endCircle);

			DrawPoints(endCircle);

			Handles.DrawLine(startCircle[0],endCircle[0]);
			Handles.DrawLine(startCircle[11],endCircle[11]);
			Handles.DrawLine(startCircle[22],endCircle[22]);
			Handles.DrawLine(startCircle[33],endCircle[33]);

			Handles.DrawLine(_start,_start+_direction*_length);

	        }

		void DrawPoints(Vector3[] points)
		{

			for(int i=0;i<points.Length-1;i++)
			{
				Handles.DrawLine(points[i],points[i+1]);
			}
			Handles.DrawLine(points[points.Length-1],points[0]);

		}



		void GetWireDiscPoints (Vector3 center, Vector3 normal, float radius,Vector3[] points)
		{
			Vector3 from = Vector3.Cross (normal, Vector3.up);
			if (from.sqrMagnitude < 0.001f)
			{
				from = Vector3.Cross (normal, Vector3.right);
			}
	
			SetDiscSectionPoints (points, center, normal, from, 360, radius);
		}

		void SetDiscSectionPoints (Vector3[] dest, Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
		{
			from.Normalize ();
			Quaternion rotation = Quaternion.AngleAxis (angle / (float)(dest.Length - 1), normal);
			Vector3 vector = from * radius;
		
			for (int i = 0; i < dest.Length; i++)
			{
				dest [i] = center + vector;
				vector = rotation * vector;

			}
		}

    }
}
