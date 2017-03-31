//Frankencopypastecoded by MDS
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scrollrect ugui gamepad button selection menu auto
//  // All the important code is from Twiik here http://forum.unity3d.com/threads/scroll-rect-and-scroll-bar-arrow-keys-control.339661/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("uGui")]
    [Tooltip("Makes a scroll rect filled with buttons autoscroll when they are selected. Put this on the GameObject with the Scroll Rect. ")]
    public class ButtonAutoScrollForGamePad : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(RectTransform))]
        [Tooltip("Game Object with the Scroll Rect component.")]
        public FsmOwnerDefault gameObject;
        [Tooltip("Use this to add padding when upper buttons are selected. If using a Grid Layout group on your content also use top padding.")]
        public FsmFloat upAdjust;
        [Tooltip("Try setting this to true if nested buttons are not working.")]
        public FsmBool nestedButton;
        RectTransform scrollRectTransform;
        RectTransform contentPanel;
        RectTransform selectedRectTransform;
        private GameObject lastSelected;

        public override void Reset()
        {
            gameObject = null;
            upAdjust = null;
            nestedButton = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (go != null)
            {
                scrollRectTransform = go.GetComponent<RectTransform>();
                contentPanel = go.GetComponent<ScrollRect>().content;

            }
          
        }

        public override void OnUpdate()
        {
            DoAutoScroll();
        }

        void DoAutoScroll()
        {


            if (nestedButton.Value != true)
            {
              //  Debug.Log(" != TRUE use GameObject selected1 = EventSystem.current.currentSelectedGameObject;");

                GameObject selected1 = EventSystem.current.currentSelectedGameObject;
                

                if (selected1 == null)
                {
                    return;
                }


                // Get the currently selected UI element from the event system.
                GameObject selected = EventSystem.current.currentSelectedGameObject;
             //   Debug.Log(selected + "SELECTED");

                // Return if there are none.
                if (selected == null)
                {
                    return;
                }
                // Return if the selected game object is not inside the scroll rect.
                if (selected.transform.parent != contentPanel.transform)
                {
                    return;
                }
                // Return if the selected game object is the same as it was last frame,
                // meaning we haven't moved.
                if (selected == lastSelected)
                {
                    return;
                }

                // Get the rect tranform for the selected game object.
                selectedRectTransform = selected.GetComponent<RectTransform>();
                // The position of the selected UI element is the absolute anchor position,
                // ie. the local position within the scroll rect + its height if we're
                // scrolling down. If we're scrolling up it's just the absolute anchor position.
                float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;

                // The upper bound of the scroll view is the anchor position of the content we're scrolling.
                float scrollViewMinY = contentPanel.anchoredPosition.y;
                // The lower bound is the anchor position + the height of the scroll rect.
                float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;

                // If the selected position is below the current lower bound of the scroll view we scroll down.
                if (selectedPositionY > scrollViewMaxY)
                {
                    // DOWN
                    float newY = selectedPositionY - scrollRectTransform.rect.height;
                    contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
                }
                // If the selected position is above the current upper bound of the scroll view we scroll up.
                else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
                {

                    // UP
                    contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y + upAdjust.Value));
                }

                lastSelected = selected;


            } 
            else
            {
             //   Debug.Log("ELSE GameObject selected = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject; ");
                GameObject selected1 = EventSystem.current.currentSelectedGameObject;
             

                if (selected1 == null)
                {
                    return;
                }

                // Get the currently selected UI element from the event system.
                GameObject selected = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            //    Debug.Log(selected + "SELECTED");
                // Return if there are none.
                if (selected == null)
                {
                    return;
                }
                // Return if the selected game object is not inside the scroll rect.
                if (selected.transform.parent != contentPanel.transform)
                {
                    return;
                }
                // Return if the selected game object is the same as it was last frame,
                // meaning we haven't moved.
                if (selected == lastSelected)
                {
                    return;
                }

                // Get the rect tranform for the selected game object.
                selectedRectTransform = selected.GetComponent<RectTransform>();
                // The position of the selected UI element is the absolute anchor position,
                // ie. the local position within the scroll rect + its height if we're
                // scrolling down. If we're scrolling up it's just the absolute anchor position.
                float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;

                // The upper bound of the scroll view is the anchor position of the content we're scrolling.
                float scrollViewMinY = contentPanel.anchoredPosition.y;
                // The lower bound is the anchor position + the height of the scroll rect.
                float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;

                // If the selected position is below the current lower bound of the scroll view we scroll down.
                if (selectedPositionY > scrollViewMaxY)
                {
                    // DOWN
                    float newY = selectedPositionY - scrollRectTransform.rect.height;
                    contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
                }
                // If the selected position is above the current upper bound of the scroll view we scroll up.
                else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
                {

                    // UP
                    contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y + upAdjust.Value));
                }

                lastSelected = selected;



            }



        }

    }
}
