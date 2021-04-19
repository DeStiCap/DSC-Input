using System.Collections;
using System.Collections.Generic;
using DSC.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace DSC.Input
{
    public class DSC_Input_MultiPlayer : MonoBehaviour
    {
        #region Data

        protected struct PlayerInputAction
        {
            int m_nPlayerID;
            InputActionAsset m_hInput;

            public PlayerInputAction(int nPlayerID, InputActionAsset hInput)
            {
                m_nPlayerID = nPlayerID;
                m_hInput = hInput;

                Enable();
            }

            #region Events

            void OnAxis(CallbackContext hValue)
            {
                DSC_Input.SetRawAxis(m_nPlayerID, hValue.ReadValue<Vector2>());
            }

            void OnAxis2(CallbackContext hValue)
            {
                DSC_Input.SetRawAxis(m_nPlayerID, 1, hValue.ReadValue<Vector2>());
            }

            void OnDPadUp(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadUp, hValue.ReadValueAsButton());
            }

            void OnDPadDown(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadDown, hValue.ReadValueAsButton());
            }

            void OnDPadLeft(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadLeft, hValue.ReadValueAsButton());
            }

            void OnDPadRight(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadRight, hValue.ReadValueAsButton());
            }

            void OnNorth(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.North, hValue.ReadValueAsButton());
            }

            void OnSouth(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.South, hValue.ReadValueAsButton());
            }

            void OnWest(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.West, hValue.ReadValueAsButton());
            }

            void OnEast(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.East, hValue.ReadValueAsButton());
            }

            void OnL1(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.L1, hValue.ReadValueAsButton());
            }

            void OnL2(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.L2, hValue.ReadValueAsButton());
            }

            void OnL3(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.L3, hValue.ReadValueAsButton());
            }

            void OnR1(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.R1, hValue.ReadValueAsButton());
            }

            void OnR2(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.R2, hValue.ReadValueAsButton());
            }

            void OnR3(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.R3, hValue.ReadValueAsButton());
            }

            void OnStart(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Start, hValue.ReadValueAsButton());
            }

            void OnSelect(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Select, hValue.ReadValueAsButton());
            }

            void OnConfirm(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Confirm, hValue.ReadValueAsButton());
            }

            void OnCancel(CallbackContext hValue)
            {
                DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Cancel, hValue.ReadValueAsButton());
            }

            #endregion

            #region Main

            public void Enable()
            {
                var hAction = m_hInput.FindAction(InputButtonName.axis);
                hAction.performed += OnAxis;
                hAction.canceled += OnAxis;

                hAction = m_hInput.FindAction(InputButtonName.axis2);
                hAction.performed += OnAxis2;
                hAction.canceled += OnAxis2;

                hAction = m_hInput.FindAction(InputButtonName.dPadUp);
                hAction.performed += OnDPadUp;
                hAction.canceled += OnDPadUp;

                hAction = m_hInput.FindAction(InputButtonName.dPadDown);
                hAction.performed += OnDPadDown;
                hAction.canceled += OnDPadDown;

                hAction = m_hInput.FindAction(InputButtonName.dPadLeft);
                hAction.performed += OnDPadLeft;
                hAction.canceled += OnDPadLeft;

                hAction = m_hInput.FindAction(InputButtonName.dPadRight);
                hAction.performed += OnDPadRight;
                hAction.canceled += OnDPadRight;

                hAction = m_hInput.FindAction(InputButtonName.north);
                hAction.performed += OnNorth;
                hAction.canceled += OnNorth;

                hAction = m_hInput.FindAction(InputButtonName.south);
                hAction.performed += OnSouth;
                hAction.canceled += OnSouth;

                hAction = m_hInput.FindAction(InputButtonName.west);
                hAction.performed += OnWest;
                hAction.canceled += OnWest;

                hAction = m_hInput.FindAction(InputButtonName.east);
                hAction.performed += OnEast;
                hAction.canceled += OnEast;

                hAction = m_hInput.FindAction(InputButtonName.l1);
                hAction.performed += OnL1;
                hAction.canceled += OnL1;

                hAction = m_hInput.FindAction(InputButtonName.l2);
                hAction.performed += OnL2;
                hAction.canceled += OnL2;

                hAction = m_hInput.FindAction(InputButtonName.l3);
                hAction.performed += OnL3;
                hAction.canceled += OnL3;

                hAction = m_hInput.FindAction(InputButtonName.r1);
                hAction.performed += OnR1;
                hAction.canceled += OnR1;

                hAction = m_hInput.FindAction(InputButtonName.r2);
                hAction.performed += OnR2;
                hAction.canceled += OnR2;

                hAction = m_hInput.FindAction(InputButtonName.r3);
                hAction.performed += OnR3;
                hAction.canceled += OnR3;

                hAction = m_hInput.FindAction(InputButtonName.start);
                hAction.performed += OnStart;
                hAction.canceled += OnStart;

                hAction = m_hInput.FindAction(InputButtonName.select);
                hAction.performed += OnSelect;
                hAction.canceled += OnSelect;

                hAction = m_hInput.FindAction(InputButtonName.confirm);
                hAction.performed += OnConfirm;
                hAction.canceled += OnConfirm;

                hAction = m_hInput.FindAction(InputButtonName.cancel);
                hAction.performed += OnCancel;
                hAction.canceled += OnCancel;

                m_hInput.Enable();
            }

            public void Disable()
            {
                var hAction = m_hInput.FindAction(InputButtonName.axis);
                hAction.performed -= OnAxis;
                hAction.canceled -= OnAxis;

                hAction = m_hInput.FindAction(InputButtonName.axis2);
                hAction.performed -= OnAxis2;
                hAction.canceled -= OnAxis2;

                hAction = m_hInput.FindAction(InputButtonName.dPadUp);
                hAction.performed -= OnDPadUp;
                hAction.canceled -= OnDPadUp;

                hAction = m_hInput.FindAction(InputButtonName.dPadDown);
                hAction.performed -= OnDPadDown;
                hAction.canceled -= OnDPadDown;

                hAction = m_hInput.FindAction(InputButtonName.dPadLeft);
                hAction.performed -= OnDPadLeft;
                hAction.canceled -= OnDPadLeft;

                hAction = m_hInput.FindAction(InputButtonName.dPadRight);
                hAction.performed -= OnDPadRight;
                hAction.canceled -= OnDPadRight;

                hAction = m_hInput.FindAction(InputButtonName.north);
                hAction.performed -= OnNorth;
                hAction.canceled -= OnNorth;

                hAction = m_hInput.FindAction(InputButtonName.south);
                hAction.performed -= OnSouth;
                hAction.canceled -= OnSouth;

                hAction = m_hInput.FindAction(InputButtonName.west);
                hAction.performed -= OnWest;
                hAction.canceled -= OnWest;

                hAction = m_hInput.FindAction(InputButtonName.east);
                hAction.performed -= OnEast;
                hAction.canceled -= OnEast;

                hAction = m_hInput.FindAction(InputButtonName.l1);
                hAction.performed -= OnL1;
                hAction.canceled -= OnL1;

                hAction = m_hInput.FindAction(InputButtonName.l2);
                hAction.performed -= OnL2;
                hAction.canceled -= OnL2;

                hAction = m_hInput.FindAction(InputButtonName.l3);
                hAction.performed -= OnL3;
                hAction.canceled -= OnL3;

                hAction = m_hInput.FindAction(InputButtonName.r1);
                hAction.performed -= OnR1;
                hAction.canceled -= OnR1;

                hAction = m_hInput.FindAction(InputButtonName.r2);
                hAction.performed -= OnR2;
                hAction.canceled -= OnR2;

                hAction = m_hInput.FindAction(InputButtonName.r3);
                hAction.performed -= OnR3;
                hAction.canceled -= OnR3;

                hAction = m_hInput.FindAction(InputButtonName.start);
                hAction.performed -= OnStart;
                hAction.canceled -= OnStart;

                hAction = m_hInput.FindAction(InputButtonName.select);
                hAction.performed -= OnSelect;
                hAction.canceled -= OnSelect;

                hAction = m_hInput.FindAction(InputButtonName.confirm);
                hAction.performed -= OnConfirm;
                hAction.canceled -= OnConfirm;

                hAction = m_hInput.FindAction(InputButtonName.cancel);
                hAction.performed -= OnCancel;
                hAction.canceled -= OnCancel;

                m_hInput.Disable();
            }

            #endregion
        }

        #endregion

        #region Variable

        #region Variable - Inspector

        [SerializeField] InputActionAsset[] m_arrInput;

        #endregion

        List<PlayerInputAction> m_lstPlayerInput = new List<PlayerInputAction>();

        #endregion

        #region Unity

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void OnEnable()
        {
            EnableAllInput();
        }

        protected virtual void OnDisable()
        {
            DisableAllInput();
        }

        #endregion

        #region Main

        protected virtual void Init()
        {
            for (int i = 0; i < m_arrInput.Length; i++)
            {
                var hInput = m_arrInput[i];
                if (hInput == null)
                    continue;

                m_lstPlayerInput.Add(new PlayerInputAction(i, hInput));
            }
        }

        public virtual void EnableAllInput()
        {
            foreach (var hInput in m_lstPlayerInput)
            {
                hInput.Enable();
            }
        }

        public virtual void DisableAllInput()
        {
            foreach (var hInput in m_lstPlayerInput)
            {
                hInput.Disable();
            }
        }

        #endregion
    }
}
