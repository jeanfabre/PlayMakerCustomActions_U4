using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Remove an item from an array")]
    public class arrayRemove : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Array Variable to use.")]
        public FsmArray array;

        [RequiredField]
        [MatchElementType("array")]
        [Tooltip("Item to add.")]
        public FsmVar value;

        public override void Reset()
        {
            array = null;
            value = null;
        }

        public override void OnEnter()
        {
            DoAddValue();
            Finish();
        }

        private void DoAddValue()
        {
            array.Resize(array.Length + 1);
            value.UpdateValue();
            array.Set(array.Length - 1, value.GetValue());
        }

    }

}

