// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// http://hutonggames.com/playmakerforum/index.php?topic=5844.msg32820;topicseen#new
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: switch

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on a Game Object's Layer.")]
	public class GameObjectLayerSwitch : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The GameObject to test.")]
		public FsmGameObject gameObject;

		[CompoundArray("Layer Switches", "Compare Layer", "Send Event")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] layer;
		public FsmEvent[] sendEvent;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			layer = new FsmInt[1];
			sendEvent = new FsmEvent[1];
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoLayerSwitch();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoLayerSwitch();
		}
		
		void DoLayerSwitch()
		{

            var go = gameObject.Value;
            if (go == null)
			{
			    return;
			}
			
			for (var i = 0; i < layer.Length; i++) 
			{
				if (go.layer == layer[i].Value)
				{
					Fsm.Event(sendEvent[i]);
					return;
				}
			}
		}
	}
}