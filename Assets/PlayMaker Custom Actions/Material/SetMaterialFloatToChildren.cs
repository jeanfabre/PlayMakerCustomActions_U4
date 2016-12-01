// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Updated by : sebaslive


using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Material)]
    [Tooltip("Sets a named float value in a game object's  material and its children.")]
	public class SetMaterialFloatToChildren : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmInt materialIndex;

        public FsmString namedFloat;

        [RequiredField]
        public FsmFloat value;

        public FsmBool affectAllHierarchy;


        public FsmBool revertOnExit;


        public bool everyFrame;


        private Hashtable _originalFloats;
        private string floatName;

        public override void Reset()
		{
            gameObject = null;
            materialIndex = 0;
	namedFloat = "_Shininess";
		value = null;

            affectAllHierarchy = true;
            revertOnExit = true;

            everyFrame = false;
        }

        public override void OnExit()
        {
            if (revertOnExit.Value)
            {
                foreach (DictionaryEntry Item in _originalFloats)
                {
                    GameObject go = (GameObject)Item.Key;
                    

                    if (materialIndex.Value == 0)
                    {
			go.GetComponent<Renderer>().material.SetFloat(floatName, value.Value);
                    }
                    else if (go.GetComponent<Renderer>().materials.Length > materialIndex.Value)
                    {
                        var materials = go.GetComponent<Renderer>().materials;
						materials[materialIndex.Value].SetFloat(floatName, value.Value);
                        go.GetComponent<Renderer>().materials = materials;
                    }
                }
            }
        }

        public override void OnEnter()
        {

            floatName = namedFloat.Value;
			if (floatName == "") floatName = "_Shininess";

            _originalFloats = new Hashtable();

            DoSetMaterialColor(revertOnExit.Value);

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoSetMaterialColor(false);
        }

        void DoSetMaterialColor(bool storeOriginalFloat)
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (go == null)
            {
                return;
            }
            if (affectAllHierarchy.Value)
            {
                ApplyColorToHierarchy(go.transform, storeOriginalFloat);
            }
            else
            {
                DoSetMaterialColorToGO(Fsm.GetOwnerDefaultTarget(gameObject), storeOriginalFloat);
            }

        }

        void ApplyColorToHierarchy(Transform parent, bool storeOriginalFloat)
        {
            DoSetMaterialColorToGO(parent.gameObject, storeOriginalFloat);

            for (int i = 0; i < parent.childCount; i++)
            {
                ApplyColorToHierarchy(parent.GetChild(i), storeOriginalFloat);
            }

        }



        void DoSetMaterialColorToGO(GameObject go, bool storeOriginalFloat)
        {

            if (go == null) return;

            if (go.GetComponent<Renderer>() == null)
            {
                LogWarning("Missing Renderer!");
                return;
            }

            if (go.GetComponent<Renderer>().material == null)
            {
                LogWarning("Missing Material!");
                return;
            }


            Material _mat = go.GetComponent<Renderer>().material;
            float _originalFloat = 0f;


            if (materialIndex.Value == 0)
            {
                if (storeOriginalFloat)
                {
                    _originalFloat = _mat.GetFloat(floatName);
                }

                _mat.SetFloat(floatName, value.Value);
            }
            else if (go.GetComponent<Renderer>().materials.Length > materialIndex.Value)
            {
                var materials = go.GetComponent<Renderer>().materials;
                _mat = materials[materialIndex.Value];
                if (storeOriginalFloat)
                {
                    _originalFloat = _mat.GetFloat(floatName);
                }
		_mat.SetFloat(floatName, value.Value);
                go.GetComponent<Renderer>().materials = materials;
            }

            _mat.SetFloat(floatName, value.Value);

            if (storeOriginalFloat)
            {
                _originalFloats.Add(go, _originalFloat);
            }

        }
    }
}
