// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

#pragma warning disable 0162  

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Resources")]
    [Tooltip("Loads an asset stored at path in a Resources folder. The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
    public class ResourcesLoad : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
        public FsmString assetPath;

        [RequiredField]
        [Tooltip("The stored asset")]
        [UIHint(UIHint.Variable)]
        public FsmVar storeAsset;

        public FsmBool doNotSpawn;

        public FsmEvent successEvent;
        public FsmEvent failureEvent;



        public override void Reset()
        {
            assetPath = null;
            storeAsset = new FsmVar();
            storeAsset.Type = VariableType.Texture;
            doNotSpawn = false;
        }


        public override void OnEnter()
        {
            bool ok = false;
            try
            {
                ok = loadResource();
            }
            catch (UnityException e)
            {
                Debug.LogWarning(e.Message);
            }

            if (ok)
            {
                Fsm.Event(successEvent);
            }
            else
            {
                Fsm.Event(failureEvent);
            }

            Finish();
        }

        public override string ErrorCheck()
        {
            switch (storeAsset.Type)
            {
                case VariableType.GameObject:
                    break;
                case VariableType.Texture:
                    break;
                case VariableType.Material:
                    break;
                case VariableType.String:
                    break;
                case VariableType.Object:
                    break;
                default:
                    // not supported.
                    return "Only GameObject, Texture, Sprites, AudioClip and Material are supported";
            }

            return "";
        }

        public bool loadResource()
        {
            switch (storeAsset.Type)
            {
                case VariableType.GameObject:
                    GameObject source = (GameObject)Resources.Load(assetPath.Value, typeof(GameObject));
                    Debug.Log(source);
                    if (source == null)
                    {
                        return false;
                    }
                    if (doNotSpawn.Value)
                    {
                        
                        FsmGameObject _target = this.Fsm.Variables.GetFsmGameObject(storeAsset.variableName);
                        _target.Value = source;
                    }
                    else
                    {
                        GameObject _go = (GameObject)Object.Instantiate(source);
                        if (_go == null)
                        {
                            return false;
                        }
                        else
                        {
                            FsmGameObject _target = this.Fsm.Variables.GetFsmGameObject(storeAsset.variableName);
                            _target.Value = _go;
                        }
                    }
                    break;
                case VariableType.Texture:
                    Texture2D _texture = (Texture2D)Resources.Load(assetPath.Value, typeof(Texture2D));
                    if (_texture == null)
                    {
                        return false;
                    }
                    else
                    {
                        FsmTexture _target = this.Fsm.Variables.GetFsmTexture(storeAsset.variableName);
                        _target.Value = _texture;
                    }
                    break;
                case VariableType.Material:
                    Material _material = (Material)Resources.Load(assetPath.Value, typeof(Material));
                    if (_material == null)
                    {
                        return false;
                    }
                    else
                    {
                        FsmMaterial _target = this.Fsm.Variables.GetFsmMaterial(storeAsset.variableName);
                        _target.Value = _material;
                    }
                    break;
                case VariableType.String:
                    TextAsset _asset = (TextAsset)Resources.Load(assetPath.Value, typeof(TextAsset));
                    if (_asset == null)
                    {
                        return false;
                    }
                    else
                    {
                        FsmString _target = this.Fsm.Variables.GetFsmString(storeAsset.variableName);
                        _target.Value = _asset.text;
                    }
                    break;
                case VariableType.Object:

                    FsmObject _var = this.Fsm.Variables.GetFsmObject(storeAsset.variableName);

                    _var.Value = Resources.Load(assetPath.Value, _var.ObjectType);

                    if (_var.Value != null && _var.Value.GetType() == _var.ObjectType)
                    {
                        return true;
                    }
                    else
                    {
                        _var.Value = null;
                        return false;
                    }

                    break;
                default:
                    // not supported.
                    return false;
            }
            return true;
        }

    }
}

