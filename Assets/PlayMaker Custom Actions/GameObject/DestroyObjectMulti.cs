// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys a Game Object.")]
	public class DestroyObjectMulti : FsmStateAction
	{
        [CompoundArray("Count", "game Object", "Detach Children")]
        [RequiredField]
		[Tooltip("The GameObject to destroy.")]
		public FsmGameObject[] gameObject;

        [Tooltip("Detach children before destroying the Game Object.")]
        public FsmBool[] detachChildren;


		public override void Reset()
		{
			gameObject = new FsmGameObject[1];
            detachChildren = new FsmBool[1];
		}

		public override void OnEnter()
		{
            for (int i = 0; i < gameObject.Length; i++)
            {
                var go = gameObject[i].Value;

                if (go != null)
                {
                    if (detachChildren[i].Value)
                    {
                        go.transform.DetachChildren();
                    }
                    Object.Destroy(go);
                }
            }
			Finish();
		}
	}
}