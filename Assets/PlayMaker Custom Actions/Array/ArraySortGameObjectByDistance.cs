// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Sort GameObjects within an array based on the distance of a transform or position.")]
	public class ArraySortGameObjectByDistance : FsmStateAction
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
		
		GameObject _rootGo;
		Vector3 root;
		public override void Reset()
		{
			array = null;
			distanceFrom = null;
			orDistanceFromVector3 = null;

			everyframe = true;
		}
		
		
		public override void OnEnter()
		{
			DoSortByDistance();
			
			if (!everyframe)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoSortByDistance();
		}
		
		void DoSortByDistance()
		{

			root = orDistanceFromVector3.Value;
			
			_rootGo = Fsm.GetOwnerDefaultTarget (distanceFrom);
			if (_rootGo!=null)
			{
				root += _rootGo.transform.position;
			}
			
			// bubble-sort transforms
			for ( int e = 0; e < array.Length - 1; e ++ )
			{
				GameObject _item_e0 = (GameObject)array.objectReferences[e + 0];
				GameObject _item_e1 = (GameObject)array.objectReferences[e + 1];

				float sqrMag1  = ( _item_e0.transform.position - root ).sqrMagnitude;
				float sqrMag2 = ( _item_e1.transform.position - root ).sqrMagnitude;

				UnityEngine.Debug.Log(sqrMag1+" "+sqrMag2);
				if ( sqrMag2 < sqrMag1 )
				{
					GameObject tempStore = (GameObject)array.objectReferences[e];
					array.objectReferences[e] = array.objectReferences[e + 1];
					array.objectReferences[e + 1] = tempStore;
					e = 0;
				}


			}

			array.SaveChanges();

		}

	}
}