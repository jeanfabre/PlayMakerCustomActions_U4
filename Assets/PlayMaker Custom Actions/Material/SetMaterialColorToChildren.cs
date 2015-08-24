// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// Updated by : sebaslive
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Material)]
    [Tooltip("Sets a named color value in a game object's  material and its children.")]
    public class SetMaterialColorToChildren : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmInt materialIndex;

        [UIHint(UIHint.NamedColor)]
        public FsmString namedColor;

        [RequiredField]
        public FsmColor color;

        public FsmBool affectAllHierarchy;


        public FsmBool revertOnExit;


        public bool everyFrame;


        private Hashtable _originalColors;
        private string colorName;

        public override void Reset()
        {
            gameObject = null;
            materialIndex = 0;
            namedColor = "_Color";
            color = Color.black;

            affectAllHierarchy = true;
            revertOnExit = true;

            everyFrame = false;
        }

        public override void OnExit()
        {
            if (revertOnExit.Value)
            {
                foreach (DictionaryEntry Item in _originalColors)
                {
                    GameObject go = (GameObject)Item.Key;
                    Color color = (Color)Item.Value;

                    if (materialIndex.Value == 0)
                    {
                        go.GetComponent<Renderer>().material.SetColor(colorName, color);
                    }
                    else if (go.GetComponent<Renderer>().materials.Length > materialIndex.Value)
                    {
                        var materials = go.GetComponent<Renderer>().materials;
                        materials[materialIndex.Value].SetColor(colorName, color);
                        go.GetComponent<Renderer>().materials = materials;
                    }
                }
            }
        }

        public override void OnEnter()
        {

            colorName = namedColor.Value;
            if (colorName == "") colorName = "_Color";

            _originalColors = new Hashtable();

            DoSetMaterialColor(revertOnExit.Value);

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoSetMaterialColor(false);
        }

        void DoSetMaterialColor(bool storeOriginalColor)
        {
            if (color.IsNone)
            {
                return;
            }
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (go == null)
            {
                return;
            }
            if (affectAllHierarchy.Value)
            {
                ApplyColorToHierarchy(go.transform, storeOriginalColor);
            }
            else
            {
                DoSetMaterialColorToGO(Fsm.GetOwnerDefaultTarget(gameObject), storeOriginalColor);
            }

        }

        void ApplyColorToHierarchy(Transform parent, bool storeOriginalColor)
        {
            DoSetMaterialColorToGO(parent.gameObject, storeOriginalColor);

            for (int i = 0; i < parent.childCount; i++)
            {
                ApplyColorToHierarchy(parent.GetChild(i), storeOriginalColor);
            }

        }



        void DoSetMaterialColorToGO(GameObject go, bool storeOriginalColor)
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
            Color _originalColor = new Color(0f, 0f, 0f);


            if (materialIndex.Value == 0)
            {
                if (storeOriginalColor)
                {
                    _originalColor = _mat.GetColor(colorName);
                }

                _mat.SetColor(colorName, color.Value);
            }
            else if (go.GetComponent<Renderer>().materials.Length > materialIndex.Value)
            {
                var materials = go.GetComponent<Renderer>().materials;
                _mat = materials[materialIndex.Value];
                if (storeOriginalColor)
                {
                    _originalColor = _mat.GetColor(colorName);
                }
                _mat.SetColor(colorName, color.Value);
                go.GetComponent<Renderer>().materials = materials;
            }

            _mat.SetColor(colorName, color.Value);

            if (storeOriginalColor)
            {
                _originalColors.Add(go, _originalColor);
            }

        }
    }
}
