// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: ugui first selected set
using UnityEngine;

using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("uGui")]
    [Tooltip("Enables an Event System ")]
    public class EnableEventSystem : FsmStateAction
    {
        public FsmBool enableES;
        public FsmBool everyFrame;

        private EventSystem ES;

        public override void Reset()
        {

            enableES = null;
            everyFrame = false;


        }

        public override void OnEnter()
        {
            if (!everyFrame.Value)
            {
                SF();
                Finish();
            }

        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                SF();
            }
        }

        void SF()
        {
            ES = EventSystem.current;
            Debug.Log(ES);
            ES.enabled = enableES.Value;
          
        }

    }
}
