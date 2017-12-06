using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

namespace Game.Scripts.CustomPlayMakerActions
{
    [ActionCategory("ArrayMaker/ArrayList")]
    [Tooltip("Add an item to a PlayMaker Array List Proxy component")]
    public class ArrayListCompare : FsmStateAction
    {
        [ActionSection("ArrayList 1")]
		
        [RequiredField]
        [Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
        [CheckForComponent(typeof(PlayMakerArrayListProxy))]
        public FsmOwnerDefault gameObject;
		
        [Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
        [UIHint(UIHint.FsmString)]
        public FsmString reference;
        
        [ActionSection("Compare With")]
        
        [RequiredField]
        [Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
        [CheckForComponent(typeof(PlayMakerArrayListProxy))]
        public FsmOwnerDefault gameObject2;
		
        [Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
        [UIHint(UIHint.FsmString)]
        public FsmString reference2;

        [ActionSection("Events")] 
        
        public FsmEvent Equal;

        public FsmEvent NotEqual;


        private PlayMakerArrayListProxy _proxy1;
        private PlayMakerArrayListProxy _proxy2;
        
        
        public override void Reset()
        {
            gameObject = null;
            reference = null;
            gameObject2 = null;
            reference2 = null;
            Equal = null;
            NotEqual = null;
            _proxy1 = null;
            _proxy2 = null;
        }
		
		
        public override void OnEnter()
        {
            CompareArrayLists();
            
            Finish();
        }

        public void CompareArrayLists()
        {
            _proxy1 = gameObject.GameObject.Value.GetComponents<PlayMakerArrayListProxy>()[0];
            if (_proxy1.referenceName != reference.Value)
            {
                LogError("Wrong reference name for first array (proxy1)");
            }

            _proxy2 = gameObject2.GameObject.Value.GetComponents<PlayMakerArrayListProxy>()[1];
            if (_proxy2.referenceName != reference2.Value)
            {
                LogError("Wrong reference name for second array (proxy2)");
            }

            if (_proxy1.arrayList.ToArray().SequenceEqual(_proxy2.arrayList.ToArray()))
            {
                Fsm.Event(Equal);
            }
            else
            {
                Fsm.Event(NotEqual);
            }
        }
    }
}