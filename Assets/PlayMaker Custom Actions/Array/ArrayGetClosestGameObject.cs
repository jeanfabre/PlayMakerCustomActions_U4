// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Return the closest GameObject within an array from a transform or position.")]
	public class ArrayGetClosestGameObject : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;
		
		[Tooltip("Compare the distance of the items in the list to the position of this gameObject")]
		public FsmOwnerDefault distanceFrom;
		
		[Tooltip("If DistanceFrom declared, use OrDistanceFromVector3 as an offset")]
		public FsmVector3 orDistanceFromVector3;
		
		public bool everyframe;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmGameObject closestGameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmInt closestIndex;

		GameObject _rootGo;
		
		public override void Reset()
		{
		
			array = null;
			distanceFrom = null;
			orDistanceFromVector3 = null;
			closestGameObject = null;
			closestIndex = null;
			
			everyframe = true;
		}
		
		
		public override void OnEnter()
		{

			DoFindClosestGo();
			
			if (!everyframe)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoFindClosestGo();
		}
		
		void DoFindClosestGo()
		{

			Vector3 root = orDistanceFromVector3.Value;
			
			_rootGo = Fsm.GetOwnerDefaultTarget (distanceFrom);
			if (_rootGo!=null)
			{
				root += _rootGo.transform.position;
			}
			
			float sqrDist = Mathf.Infinity;
		
			int _index = 0;
			float sqrDistTest;
			foreach(GameObject _go in array.objectReferences)
			{
				
				if (_go!=null) 
				{
					sqrDistTest = (_go.transform.position - root).sqrMagnitude;
					if (sqrDistTest<= sqrDist)
					{
						sqrDist = sqrDistTest;
						closestGameObject.Value = _go;
						closestIndex.Value = _index;
					}
				}
				_index++;
			}

		}
		
	}
}