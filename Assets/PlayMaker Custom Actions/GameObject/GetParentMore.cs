// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the Parent of a Game Object.")]
	public class GetParentMore : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeResult;
		public FsmInt repetitions;
        private int repeat;

        public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			repetitions = 0;
            repeat = 0;

        }
		
		public override void OnEnter()
		{
            repeat = repetitions.Value;


            var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				storeResult.Value = go.transform.parent == null ? null : go.transform.parent.gameObject;
				while (repeat > 0 ) 
				{
					var go2 = storeResult.Value;
                    repeat = repeat - 1;
					storeResult.Value = go2.transform.parent == null ? null : go2.transform.parent.gameObject;
				}
			}	
			else
			{
				storeResult.Value = null;
			}
			
			
			
			Finish();
		}
	}
}