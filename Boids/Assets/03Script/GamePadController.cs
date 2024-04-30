using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Combat {
    public class GamePadController : MonoBehaviour {

        [SerializeField] private RectTransform stick;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform padBgRect;
        [SerializeField] private GameObject padObject;
        [SerializeField, Range(10f, 150f)] private float stickRange;

        public Vector2 inputVector;
        private bool _isActive;
        public bool isRun;

        public Camera mainCam;

        private void Awake() {
            Input.multiTouchEnabled = false;
            stickRange = padBgRect.sizeDelta.x / 2;
            padObject.SetActive(false);
        }

        void ResetStick() {
            stick.anchoredPosition = Vector2.zero;
            inputVector = Vector2.zero;
            isRun = false;
            _isActive = false;
        }
        
        void ControlStick(Vector2 pos) {
            var inputDir = pos - rectTransform.anchoredPosition;
            var clampDir = inputDir.magnitude < stickRange ? inputDir : inputDir.normalized * stickRange;
            stick.anchoredPosition = clampDir;

            inputVector = clampDir.normalized;
            isRun = Mathf.Abs(inputVector.x) > 0.5f || Mathf.Abs(inputVector.y) > 0.5f ? true : false;
        }
        
        public Canvas canvas;

        public void Update() {
            UpdateControl();
        }

        private void UpdateControl() {
            if (_isActive == false) {
                if (Input.GetMouseButtonDown(0)) {
                    SetControlState(true);
                }
            }
            else {
                if (Input.GetMouseButtonUp(0)) {
                    SetControlState(false);
                }
            }

            if (_isActive) {
                ControlStick(Input.mousePosition / canvas.scaleFactor);
            }
        }

        private void SetControlState(bool flag) {
            if (_isActive == flag) {
                return;
            }
            _isActive = flag;

            if (_isActive) {
                var inputPos = Input.mousePosition / canvas.scaleFactor;
                rectTransform.anchoredPosition = inputPos;
            }
            else {
                ResetStick();
            }
            padObject.SetActive(_isActive);
        }
        
        private bool IsPointerOverUIObject() {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        
        public string GetDebugStatusText() {
            return $"isActive: {_isActive}\ninputVector: {inputVector}\nTouchCount: {Input.touchCount}";
        }
    }
}
