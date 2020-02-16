// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
					],
"version":"1.1.0"
}
EcoMetaEnd
 
EcoChangeLogStart
### 1.1.0
**Improvement**  
- new operation "SqrMagnitude" for faster operation then "distance".

### 1.0.0
**Note**  
Based on Vector3Operator official Action

**New**  
- Using PlayMakerActionsUtils flexible update selector system  
EcoChangeLogEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Performs most possible operations on 2 Vector3: Dot product, Cross product, Distance, Angle, Project, Reflect, Add, Subtract, Multiply, Divide, Min, Max, SqrMagnitude.\n Advanced features allows selection of update type.")]
	public class Vector3OperatorAdvanced : FsmStateAction
	{
		public enum Vector3Operation
		{
			DotProduct,
			CrossProduct,
			Distance,
			Angle,
			Project,
			Reflect,
			Add,
			Subtract,
			Multiply,
			Divide,
			Min,
			Max,
			SqrMagnitude
		}
		
		[RequiredField]
		public FsmVector3 vector1;
		[RequiredField]
		public FsmVector3 vector2;
		public Vector3Operation operation = Vector3Operation.Add;
		
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector3Result;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat storeFloatResult;
		
		public bool everyFrame;

		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;

		public override void Reset()
		{
			vector1 = null;
			vector2 = null;
			operation = Vector3Operation.Add;
			storeVector3Result = null;
			storeFloatResult = null;
			everyFrame = false;
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
		}

		public override void OnPreprocess()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				Fsm.HandleFixedUpdate = true;
			}
			
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate)
			{
				DoVector3Operator();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnLateUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				DoVector3Operator();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				DoVector3Operator();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		void DoVector3Operator()
		{
			var v1 = vector1.Value;
			var v2 = vector2.Value;
			
			switch (operation)
			{
			case Vector3Operation.DotProduct:
				storeFloatResult.Value = Vector3.Dot(v1, v2);
				break;
				
			case Vector3Operation.CrossProduct:
				storeVector3Result.Value = Vector3.Cross(v1, v2);
				break;
				
			case Vector3Operation.Distance:
				storeFloatResult.Value = Vector3.Distance(v1, v2);
				break;
				
			case Vector3Operation.Angle:
				storeFloatResult.Value = Vector3.Angle(v1, v2);
				break;
				
			case Vector3Operation.Project:
				storeVector3Result.Value = Vector3.Project(v1, v2);
				break;
				
			case Vector3Operation.Reflect:
				storeVector3Result.Value = Vector3.Reflect(v1, v2);
				break;
				
			case Vector3Operation.Add:
				storeVector3Result.Value = v1 + v2;
				break;
				
			case Vector3Operation.Subtract:
				storeVector3Result.Value = v1 - v2;
				break;
				
			case Vector3Operation.Multiply:
				// I know... this is a far reach and not usefull in 99% of cases. 
				// I do use it when I use vector3 as arrays recipients holding something else than a position in space.
				var multResult = Vector3.zero;
				multResult.x = v1.x * v2.x;
				multResult.y = v1.y * v2.y;
				multResult.z = v1.z * v2.z;
				storeVector3Result.Value = multResult;
				break;
				
			case Vector3Operation.Divide: // I know... this is a far reach and not usefull in 99% of cases.
				// I do use it when I use vector3 as arrays recipients holding something else than a position in space.
				var divResult = Vector3.zero;
				divResult.x = v1.x / v2.x;
				divResult.y = v1.y / v2.y;
				divResult.z = v1.z / v2.z;
				storeVector3Result.Value = divResult;
				break;
				
			case Vector3Operation.Min:
				storeVector3Result.Value = Vector3.Min(v1, v2);
				break;
				
			case Vector3Operation.Max:
				storeVector3Result.Value = Vector3.Max(v1, v2);
				break;
				
			case Vector3Operation.SqrMagnitude:
       			storeFloatResult.Value = (v1 - v2).sqrMagnitude;
				break;
			}
		}
	}
}
