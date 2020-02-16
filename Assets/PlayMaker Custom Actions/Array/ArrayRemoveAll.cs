// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Remove all items from an array.")]
    public class ArrayRemoveAll : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("The Array Variable to clear.")]
        public FsmArray array;

        public override void Reset()
        {
            array = null;  
        }

        public override void OnEnter()
        {      
            array.Reset();       
            Finish();
        }
    }
}