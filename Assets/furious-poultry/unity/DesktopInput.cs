using System;
using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(BallYeeterAuthoring))]
    public class DesktopInput: MonoBehaviour
    {
        public float sensX = 900;
        public float sensY = 900;
        
        BallYeeterAuthoring _ballYeeterAuthoring;
        void Start()
        {
            _ballYeeterAuthoring = GetComponent<BallYeeterAuthoring>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        public void Update()
        {
#if DESKTOP
            ExecuteDesktopInput();
#endif
            
// #if UNITY_EDITOR
//             ExecuteDesktopInput();
// #endif
        }

        void ExecuteDesktopInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _ballYeeterAuthoring.PreviousPoultry();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _ballYeeterAuthoring.NextPoultry();
            }
        
            if (Input.GetKeyDown(KeyCode.Mouse0)) _ballYeeterAuthoring.ExecutePrimaryAction();
            
            MouseLook();
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                _ballYeeterAuthoring.DestroyPoultry();
            }
        }
        
        private float _xRotation;
        private float _yRotation;

        private void MouseLook() // actual name for looking around with a mouse in a game source: wikipedia
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
       
        }
    }
}
