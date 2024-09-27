using System;
using System.Collections.Generic;
using System.Linq;
using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using UnityEngine;
using UnityEngine.UI;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(PlayerAuthoring))]
    public class MobileInput : MonoBehaviour
    {
        public Camera camera;
        Dictionary<int, TouchInstance> _touchInstances = new Dictionary<int, TouchInstance>();

        PlayerAuthoring _playerAuthoring;
        void Start()
        {
            _playerAuthoring = GetComponent<PlayerAuthoring>();
            _zoomManager = new ZoomManager(camera);
        }

        public void Update()
        {
#if UNITY_EDITOR
            ExecuteMobileInput();
#endif

#if MOBILE
            ExecuteMobileInput();
#endif
            
        }

        public void Attack()
        {
            _playerAuthoring.ExecutePrimaryAction();
        }

        public void Beagle()
        {
            _playerAuthoring.NextPoultry();
        }
        
        public void Cluck()
        {
            _playerAuthoring.PreviousPoultry();
        }

        void ExecuteMobileInput()
        {
            ProcessTouchChanges();
            DetermineTouchActions();
        }


        RotateInstance _rotateInstance;
        ZoomManager _zoomManager;
        
        void DetermineTouchActions()
        {
            TouchInstance[] liftedInstances = _touchInstances
                .Select(x=>x.Value)
                .Where(x => x.Lifted)
                .ToArray();
            
            foreach (TouchInstance liftedInstance in liftedInstances)
            {
                _touchInstances.Remove(liftedInstance.FingerId);
            }

            TouchInstance[] initializedInstances = _touchInstances
                .Select(x => x.Value)
                .Where(x=>x.Initialized)
                .ToArray();

            if (initializedInstances.Length == 0)
            {
                _rotateInstance = null;
            }

            if (initializedInstances.Length == 1)
            {
                TouchInstance moveTouchInstance = initializedInstances[0];
                Vector3 currentRotation = transform.localRotation.eulerAngles;
                if (_rotateInstance is null)
                {
                    _rotateInstance = new RotateInstance(currentRotation);
                    return;
                }
                Vector2 canvasOffset = (moveTouchInstance.CurrentPosition - moveTouchInstance.InitialPosition) * (0.05f * _zoomManager.Fraction);
                Vector3 newRotation = _rotateInstance.Origin - new Vector3(canvasOffset.y, canvasOffset.x, currentRotation.z);
                transform.localRotation = Quaternion.Euler(new Vector3(newRotation.x, newRotation.y, newRotation.z));
                
                // Debug.LogWarning($"Navigating - Offset: {canvasOffset}, Origin: {_moveInstance.Origin}, NewPosition: {newPosition}");
                _rotateInstance.UpdateOrigin(newRotation);
                moveTouchInstance.UpdateOrigin(moveTouchInstance.CurrentPosition);
            }

            // if (initializedInstances.Length == 2)
            // {
            //     TouchInstance first = initializedInstances[0];
            //     TouchInstance second = initializedInstances[1];
            //     float initialDistance = Vector2.Distance(first.InitialPosition, second.InitialPosition);
            //     float currentDistance = Vector2.Distance(first.CurrentPosition, second.CurrentPosition);
            //     float difference = initialDistance - currentDistance;
            //     first.UpdateOrigin(first.CurrentPosition);
            //     second.UpdateOrigin(second.CurrentPosition);
            //     _zoomManager.UpdateHeight(difference);
            // }
        }
        void ProcessTouchChanges()
        {
            Touch[] touches = Input.touches;
            foreach (Touch touch in touches)
            {
                //ignore touch presses not registered correctly due to gui buttons
                if(touch.phase != TouchPhase.Began && !_touchInstances.ContainsKey(touch.fingerId))
                    continue;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchInstances[touch.fingerId] = new TouchInstance(touch.fingerId);
                        break;
                    case TouchPhase.Moved:
                        TouchInstance instance = _touchInstances[touch.fingerId];
                        if(!instance.Initialized)
                            instance.UpdateOrigin(touch.position);
                        TouchInstance touchInstance = instance;
                        touchInstance.MoveFinger(touch.position);
                        break;
                    case TouchPhase.Stationary:
                        //_touchInstances[touch.fingerId].StartHold();
                        break;
                    case TouchPhase.Ended:
                        _touchInstances[touch.fingerId].Lift();
                        break;
                    case TouchPhase.Canceled:
                        _touchInstances.Remove(touch.fingerId);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class TouchInstance
    {
        public int FingerId { get; }
        public Vector2 InitialPosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public bool Lifted { get; private set; }
        public bool Initialized { get; private set; }

        public TouchInstance(int fingerId)
        {
            FingerId = fingerId;
        }
        public void MoveFinger(Vector2 touchPosition)
        {
            CurrentPosition = touchPosition;
        }
        public void Lift()
        {
            Lifted = true;
        }
        public void UpdateOrigin(Vector2 currentPosition)
        {
            InitialPosition = currentPosition;
            Initialized = true;
        }
    }

    public class RotateInstance
    {
        public Vector3 Origin { get; private set; }
        public RotateInstance(Vector3 origin) {
            this.Origin = origin;
        }
        public void UpdateOrigin(Vector3 rotation)
        {
            Origin = rotation;
        }
    }


    public class ZoomManager
    {
        readonly Camera _camera;
        public ZoomManager(Camera camera) {
            this._camera = camera;
        }
        const float MaxZoomHeight = 50f;
        const float MinZoomHeight = 10f;
        const float ZoomStrength = 0.05f;

        float _currentHeight = 50;

        public float Fraction => _currentHeight / MaxZoomHeight; 
        public void UpdateHeight(float difference)
        {
            Vector3 transformLocalPosition = _camera.transform.localPosition;
            _currentHeight = Math.Clamp(transformLocalPosition.y + difference * ZoomStrength, MinZoomHeight, MaxZoomHeight);
            _camera.transform.localPosition = new Vector3(transformLocalPosition.x, _currentHeight, transformLocalPosition.z);
        }
    }
}
