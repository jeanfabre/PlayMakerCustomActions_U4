// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Meshes")]
	[Tooltip("Sets the Mesh attached to a Game Object.")]
	public class SetMesh : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmOwnerDefault gameObject;

        [ObjectType(typeof(Mesh))]
        [Tooltip("Place the Spline Computer Object here")]
        public FsmObject meshObject;
        public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
            meshObject = null;
		}

		public override void OnEnter()
		{
			DoSetMesh();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate()
		{
            DoSetMesh();
		}
		
		void DoSetMesh()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
            go.GetComponent<MeshFilter>().mesh = meshObject.Value as Mesh;
		    
		}
	}
}
