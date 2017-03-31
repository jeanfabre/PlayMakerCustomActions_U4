//Frankencopypastecoded by MDS
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: UV map animate offset material sprite sheet
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
    public class AnimatedUvMap : FsmStateAction
    {

        [RequiredField]
        [CheckForComponent(typeof(MeshFilter))]
        public FsmOwnerDefault gameObject;

        //   public FsmMaterial materialToAnimate;



        //vars for the whole sheet
        public FsmInt columnCount = 4;
        public FsmInt rowCount = 4;


        private int rowNumber = 0; //Zero Indexed
        private int colNumber = 0; //Zero Indexed
        private int totalCells = 4;
        public FsmInt fps = 10;
        //Maybe this should be a private var
        private Vector2 offset;

        public FsmBool everyFrame;

        public override void Reset()
        {
            columnCount = 4;
            rowCount = 4;
            rowNumber = 0;
            colNumber = 0;
            totalCells = 12;
            fps = 15;
            // materialToAnimate = null;

            everyFrame = true;
        }


        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            Renderer renderer = go.GetComponent<Renderer>();

            totalCells = columnCount.Value * rowCount.Value;


            SetSpriteAnimation(columnCount.Value, rowCount.Value, rowNumber, colNumber, totalCells, fps.Value);

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SetSpriteAnimation(columnCount.Value, rowCount.Value, rowNumber, colNumber, totalCells, fps.Value);
        }

        void SetSpriteAnimation(FsmInt columnCount, FsmInt rowCount, FsmInt rowNumber, FsmInt colNumber, FsmInt totalCells, FsmInt fps)
        {
            // Calculate index
            int index = (int)(Time.time * fps.Value);
            // Repeat when exhausting all cells
            index = index % totalCells.Value;

            // Size of every cell
            float sizeX = 1.0f / columnCount.Value;
            float sizeY = 1.0f / rowCount.Value;
            Vector2 size = new Vector2(sizeX, sizeY);

            // split into horizontal and vertical index
            var uIndex = index % columnCount.Value;
            var vIndex = index / columnCount.Value;

            // build offset
            // v coordinate is the bottom of the image in opengl so we need to invert.
            float offsetX = (uIndex + colNumber.Value) * size.x;
            float offsetY = (1.0f - size.y) - (vIndex + rowNumber.Value) * size.y;
            Vector2 offset = new Vector2(offsetX, offsetY);

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            Renderer renderer = go.GetComponent<Renderer>();

            renderer.material.SetTextureOffset("_MainTex", offset);
            renderer.material.SetTextureScale("_MainTex", size);
        }
    }
}
