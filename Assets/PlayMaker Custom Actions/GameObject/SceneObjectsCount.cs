// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Gets the number of children that a GameObject has.")]
    public class SceneObjectsCount : FsmStateAction
    {

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the number of children in an int variable.")]
        public FsmInt storeResult;



        [ActionSection("Optional")]
        [Tooltip("Optional Find with tag")]
        [UIHint(UIHint.Tag)]
        public FsmString findWithTag;

        private GameObject[] theGameObjects;

        public override void Reset()
        {
            findWithTag = null;
            storeResult = null;
        }

        public override void OnEnter()
        {
            DoSceneObjectsCount();

            Finish();
        }

        void DoSceneObjectsCount()
        {
            if (string.IsNullOrEmpty(findWithTag.Value) || findWithTag.IsNone)
            {
                theGameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
                storeResult.Value = theGameObjects.Length;
            }
            else
            {
                theGameObjects = GameObject.FindGameObjectsWithTag(findWithTag.Value) as GameObject[];
                storeResult.Value = theGameObjects.Length;
            }
        }
    }
}