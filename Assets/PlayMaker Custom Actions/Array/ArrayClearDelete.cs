using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Clears all items from an array, leaving it empty.")]
    public class ArrayClearDelete : FsmStateAction
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