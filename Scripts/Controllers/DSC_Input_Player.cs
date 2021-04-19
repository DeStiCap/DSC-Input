using DSC.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace DSC.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class DSC_Input_Player : MonoBehaviour
    {
        #region Variable

        PlayerInput m_hInput;

        int m_nPlayerID;

        #endregion

        #region Unity

        private void Awake()
        {
            m_hInput = GetComponent<PlayerInput>();
            m_nPlayerID = m_hInput.playerIndex;
        }

        #endregion

        #region Main

        #region Events

        public void OnAxis(CallbackContext hValue)
        {
            DSC_Input.SetRawAxis(m_nPlayerID, hValue.ReadValue<Vector2>());
        }

        public void OnAxis2(CallbackContext hValue)
        {
            DSC_Input.SetRawAxis(m_nPlayerID, 1, hValue.ReadValue<Vector2>());
        }

        public void OnDPadUp(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadUp, hValue.ReadValueAsButton());
        }

        public void OnDPadDown(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadDown, hValue.ReadValueAsButton());
        }

        public void OnDPadLeft(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadLeft, hValue.ReadValueAsButton());
        }

        public void OnDPadRight(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.DPadRight, hValue.ReadValueAsButton());
        }

        public void OnNorth(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.North, hValue.ReadValueAsButton());
        }

        public void OnSouth(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.South, hValue.ReadValueAsButton());
        }

        public void OnWest(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.West, hValue.ReadValueAsButton());
        }

        public void OnEast(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.East, hValue.ReadValueAsButton());
        }

        public void OnL1(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.L1, hValue.ReadValueAsButton());
        }

        public void OnL2(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.L2, hValue.ReadValueAsButton());
        }

        public void OnL3(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.L3, hValue.ReadValueAsButton());
        }

        public void OnR1(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.R1, hValue.ReadValueAsButton());
        }

        public void OnR2(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.R2, hValue.ReadValueAsButton());
        }

        public void OnR3(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.R3, hValue.ReadValueAsButton());
        }

        public void OnStart(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Start, hValue.ReadValueAsButton());
        }

        public void OnSelect(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Select, hValue.ReadValueAsButton());
        }

        public void OnConfirm(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Confirm, hValue.ReadValueAsButton());
        }

        public void OnCancel(CallbackContext hValue)
        {
            DSC_Input.SetButtonInput(m_nPlayerID, (int)InputButtonType.Cancel, hValue.ReadValueAsButton());
        }

        #endregion

        #endregion
    }
}