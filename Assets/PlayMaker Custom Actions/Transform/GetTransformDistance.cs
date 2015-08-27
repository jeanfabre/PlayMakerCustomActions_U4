// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Gets the distance based on the Transform Values.")]
    public class GetTransformDistance : FsmStateAction
    {

        [RequiredField]
        public FsmOwnerDefault gameObject;
        [RequiredField]
        public FsmGameObject target;


        public enum distanceSelect
        {
            Vector3Distance,
            xDistance,
            yDistance,
            zDistance,
        };
        public distanceSelect selectDistanceType;
        [Tooltip("Gets a negative and positive values depending on the transform position.")]
        public FsmBool getNegative;
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmFloat storeResult;
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            target = null;
            getNegative = null;
            storeResult = null;
            everyFrame = true;
        }

        public override void OnEnter()
        {
            DoGetDistance();

            if (!everyFrame)
                Finish();
        }
        public override void OnUpdate()
        {
            DoGetDistance();
        }

        void DoGetDistance()
        {
            GameObject go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;

            if (go == null || target.Value == null || storeResult == null)
                return;
            switch (selectDistanceType)
            {
                case distanceSelect.Vector3Distance:
                    if (getNegative.Value == true)
                    {
                        var position = go.transform.position;
                        var newPos = target.Value.transform.TransformPoint(position);
                        storeResult.Value = (newPos.x + newPos.y + newPos.z);
                    }
                    else
                    {

                        storeResult.Value = Vector3.Distance(go.transform.position, target.Value.transform.position);
                    }
                    break;

                case distanceSelect.xDistance:
                    if (getNegative.Value == true)
                    {
                        var position = go.transform.position;
                        var newX = target.Value.transform.TransformPoint(position);
                        storeResult.Value = newX.x;
                    }
                    else
                    {
                        storeResult.Value = Mathf.Abs(go.transform.position.x - target.Value.transform.position.x);
                    }
                    break;

                case distanceSelect.yDistance:
                    if (getNegative.Value == true)
                    {
                        var position = go.transform.position;
                        var newY = target.Value.transform.TransformPoint(position);
                        storeResult.Value = newY.y;
                    }
                    else
                    {
                        storeResult.Value = Mathf.Abs(go.transform.position.y - target.Value.transform.position.y);
                    }
                    break;

                case distanceSelect.zDistance:
                    if (getNegative.Value == true)
                    {
                        var position = go.transform.position;
                        var newZ = target.Value.transform.TransformPoint(position);
                        storeResult.Value = newZ.z;
                    }
                    else
                    {
                        storeResult.Value = Mathf.Abs(go.transform.position.z - target.Value.transform.position.z);
                    }
                    break;

            }

        }
    }
}