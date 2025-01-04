using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.GUI
{
    public class ButtonInputUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public SimpleInput.ButtonInput Button = new SimpleInput.ButtonInput();
        
        private void Awake()
        {
            Graphic graphic = GetComponent<Graphic>();

            if (graphic != null) 
                graphic.raycastTarget = true;
        }

        private void OnEnable()
        {
            Button.StartTracking();
        }

        private void OnDisable()
        {
            Button.StopTracking();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Button.value = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Button.value = false;
        }
    }
}