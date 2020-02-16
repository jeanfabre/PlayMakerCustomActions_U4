// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
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
		CircleCast2d _action;
		
		private Vector2 _start;
		private Vector2 _direction;
		private float _length;
		private float _minDepth;
		private float _maxDepth; 
		
		Vector3[] startCircle;
		Vector3[] endCircle;
		
		Vector3 _startDepth;
		Vector3 _direction3d;

		Vector3 _min;
		Vector3 _max;

	        public override bool OnGUI()
	        {
			_action = (HutongGames.PlayMaker.Actions.CircleCast2d)target;

			bool _changed =  DrawDefaultInspector();
			if (_changed)
			{
				_action.ComputeRayCastProperties(out _start,out _direction,out _length, out _minDepth,out _maxDepth);
			}




			return _changed;
	        }



	        public override void OnSceneGUI()
	        {

			_action = (HutongGames.PlayMaker.Actions.CircleCast2d)target;

			if (!_action.debug.Value)
			{
				return;
			}

			_action.ComputeRayCastProperties(out _start,out _direction,out _length, out _minDepth,out _maxDepth);

			bool _maxInf = float.IsInfinity(_maxDepth);
			bool _minInf = float.IsNegativeInfinity(_minDepth);

			Handles.color = _action.debugColor.Value;
			_startDepth.x = _start.x;
			_startDepth.y = _start.y;
			_startDepth.z = 0f;
			_direction.Normalize ();
			_direction3d.x = _direction.x;
			_direction3d.y = _direction.y;

			_min = -Vector3.forward*2;
			_max = Vector3.forward*2;
			float dotSize = 5f;

			Handles.DrawLine(_startDepth,_startDepth+_direction3d*_length);

			if (_maxInf && _minInf)
			{
				startCircle = DrawCapsule2d(_startDepth,_startDepth+_direction3d*_length,_action.radius.Value,24);
			}

			if (! _maxInf)
			{
				_startDepth.z = _maxDepth;
				startCircle = DrawCapsule2d(_startDepth,_startDepth +_direction3d*_length,_action.radius.Value,24);
			}
			if (! _minInf)
			{
				_startDepth.z = _minDepth;
				endCircle = DrawCapsule2d(_startDepth,_startDepth+_direction3d*_length,_action.radius.Value,24);
			}


			if (_maxInf && _minInf)
			{
				Handles.DrawDottedLine(startCircle[0]+_min,startCircle[0]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[11]+_min,startCircle[11]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[23]+_min,startCircle[23]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[24]+_min,startCircle[24]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[35]+_min,startCircle[35]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[47]+_min,startCircle[47]+_max,dotSize);
			}else if (_maxInf && ! _minInf)
			{

				Handles.DrawDottedLine(endCircle[0],endCircle[0]+_max,dotSize);
				Handles.DrawDottedLine(endCircle[11],endCircle[11]+_max,dotSize);
				Handles.DrawDottedLine(endCircle[23],endCircle[23]+_max,dotSize);
				Handles.DrawDottedLine(endCircle[24],endCircle[24]+_max,dotSize);
				Handles.DrawDottedLine(endCircle[35],endCircle[35]+_max,dotSize);
				Handles.DrawDottedLine(endCircle[47],endCircle[47]+_max,dotSize);

			}else if (!_maxInf &&  _minInf)
			{
				Handles.DrawDottedLine(startCircle[0]+_min,startCircle[0],dotSize);
				Handles.DrawDottedLine(startCircle[11]+_min,startCircle[11],dotSize);
				Handles.DrawDottedLine(startCircle[23]+_min,startCircle[23],dotSize);
				Handles.DrawDottedLine(startCircle[24]+_min,startCircle[24],dotSize);
				Handles.DrawDottedLine(startCircle[35]+_min,startCircle[35],dotSize);
				Handles.DrawDottedLine(startCircle[47]+_min,startCircle[47],dotSize);
			}else{
				Handles.DrawLine(startCircle[0],endCircle[0]);
				Handles.DrawLine(startCircle[11],endCircle[11]);
				Handles.DrawLine(startCircle[23],endCircle[23]);
				Handles.DrawLine(startCircle[24],endCircle[24]);
				Handles.DrawLine(startCircle[35],endCircle[35]);
				Handles.DrawLine(startCircle[47],endCircle[47]);
			}


			/*
				startCircle = DrawCapsule2d(_startDepth,_startDepth+_direction3d*_length,_action.radius.Value,24);
				Handles.DrawDottedLine(startCircle[0]+_min,startCircle[0]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[11]+_min,startCircle[11]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[23]+_min,startCircle[23]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[24]+_min,startCircle[24]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[35]+_min,startCircle[35]+_max,dotSize);
				Handles.DrawDottedLine(startCircle[47]+_min,startCircle[47]+_max,dotSize);
			*/




		
	        }


		Vector3[] DrawCapsule2d(Vector3 start,Vector3 end, float radius, int resolution = 24)
		{
			Vector3[] dest = new Vector3[resolution*2];

			float _fromAngle = Angle(end-start,Vector3.right);

			SetDiscSectionPoints (dest,0, start,_fromAngle+90f, 180f, radius,resolution);

			SetDiscSectionPoints (dest,resolution, end,_fromAngle-90f, 180f, radius,resolution);
			

			DrawPoints(dest);

			Handles.DrawLine(dest[0],dest[dest.Length-1]);

			return dest;
		}


		void DrawPoints(Vector3[] points)
		{
			for(int i=0;i<points.Length-1;i++)
			{
				Handles.DrawLine(points[i],points[i+1]);
			}

		}

		void SetDiscSectionPoints (Vector3[] dest, int index, Vector3 center, float fromAngle, float totalAngle, float radius,int resolution)
		{



			float deltaAngle = totalAngle / (float)(resolution-1);

			for (int i = 0; i < resolution; i++)
			{
				dest [i+index].x = center.x + radius * Mathf.Cos((fromAngle + i * deltaAngle) * Mathf.Deg2Rad);
				dest [i+index].y = center.y + radius * Mathf.Sin((fromAngle+ i * deltaAngle )* Mathf.Deg2Rad);
				dest [i+index].z = center.z;

			}



		}

		public static float Angle(Vector3 from, Vector3 to)
		{
			float ang = Vector2.Angle(from, to);
			Vector3 cross = Vector3.Cross(from, to);
			
			if (cross.z > 0)
				ang = 360 - ang;

			return ang; //Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) *Mathf.Rad2Deg;
		}


    }
}
