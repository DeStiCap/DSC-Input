using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Event;

namespace DSC.Input
{
    public abstract class DSC_Input : MonoBehaviour
    {
        #region Variable

        #region Variable - Property

        protected abstract GameInputData gameInputData { get; set; }
        protected abstract EventCallback<DSC_InputEventType, GameInputData> inputEvent { get; set; }

        #endregion

        protected static DSC_Input m_hBaseInstance;

        #endregion

        #region Main

        #region Init

        public static void InitInput(int nPlayerNumber)
        {
            MainInitInput(nPlayerNumber);
        }

        public static void InitInput(int nPlayerNumber, int nAxisNumber)
        {
            MainInitInput(nPlayerNumber, nAxisNumber);
        }

        public static void InitInput(int nPlayerNumber, float fSensitivity, float fGravity)
        {
            MainInitInput(nPlayerNumber, 2, fSensitivity, fGravity);
        }

        public static void InitInput(int nPlayerNumber, int nAxisNumber, float fSensitivity, float fGravity)
        {
            MainInitInput(nPlayerNumber, nAxisNumber, fSensitivity, fGravity);
        }

        static void MainInitInput(int nPlayerNumber, int nAxisNumber = 2, float fSensitivity = 3f, float fGravity = 3f)
        {
            if (!HasBaseInstance())
                return;

            if (!m_hBaseInstance.gameInputData.isCreate)
                m_hBaseInstance.gameInputData = new GameInputData(nPlayerNumber, nAxisNumber, fSensitivity, fGravity);
            else
                m_hBaseInstance.gameInputData.ChangePlayerNumber(nPlayerNumber);
        }

        #endregion

        #region Events

        public static void AddEventListener(DSC_InputEventType eEvent, UnityAction<GameInputData> hAction)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.MainAddEventListener(eEvent, hAction);
        }

        void MainAddEventListener(DSC_InputEventType eEvent, UnityAction<GameInputData> hAction)
        {
            inputEvent?.Add(eEvent, hAction);
        }

        public static void RemoveEventListener(DSC_InputEventType eEvent, UnityAction<GameInputData> hAction)
        {
            if (m_hBaseInstance == null)
                return;

            m_hBaseInstance.MainRemoveEventListener(eEvent, hAction);
        }

        void MainRemoveEventListener(DSC_InputEventType eEvent, UnityAction<GameInputData> hAction)
        {
            inputEvent?.Remove(eEvent, hAction);
        }

        /// <summary>
        /// Add axis event listener to player id 0.
        /// </summary>
        /// <param name="nAxisID"></param>
        /// <param name="eDirection"></param>
        /// <param name="eEvent"></param>
        /// <param name="hAction"></param>
        public static void AddAxisEventListener(int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction)
        {
            MainAddAxisEventListener(0, nAxisID, eDirection, eEvent, hAction);
        }

        /// <summary>
        /// Add axis event listener to player id 0.
        /// </summary>
        /// <param name="nAxisID"></param>
        /// <param name="eDirection"></param>
        /// <param name="eEvent"></param>
        /// <param name="hAction"></param>
        /// <param name="eOrder"></param>
        public static void AddAxisEventListener(int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction
            , EventOrder eOrder)
        {
            MainAddAxisEventListener(0, nAxisID, eDirection, eEvent, hAction, eOrder);
        }

        public static void AddAxisEventListener(int nPlayerID, int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction)
        {
            MainAddAxisEventListener(nPlayerID, nAxisID, eDirection, eEvent, hAction);
        }

        public static void AddAxisEventListener(int nPlayerID, int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction
            , EventOrder eOrder)
        {
            MainAddAxisEventListener(nPlayerID, nAxisID, eDirection, eEvent, hAction, eOrder);
        }

        static void MainAddAxisEventListener(int nPlayerID, int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction
            , EventOrder eOrder = EventOrder.Normal)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.gameInputData
                .AddAxisEventListener(nPlayerID, nAxisID, eDirection, eEvent, hAction, eOrder);
        }

        /// <summary>
        /// Remove axis event listener from player id 0.
        /// </summary>
        /// <param name="nAxisID"></param>
        /// <param name="eDirection"></param>
        /// <param name="eEvent"></param>
        /// <param name="hAction"></param>
        public static void RemoveAxisEventListener(int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction)
        {
            MainRemoveAxisEventListener(0, nAxisID, eDirection, eEvent, hAction);
        }

        /// <summary>
        /// Remove axis event listener from player id 0.
        /// </summary>
        /// <param name="nAxisID"></param>
        /// <param name="eDirection"></param>
        /// <param name="eEvent"></param>
        /// <param name="hAction"></param>
        /// <param name="eOrder"></param>
        public static void RemoveAxisEventListener(int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction
            , EventOrder eOrder)
        {
            MainRemoveAxisEventListener(0, nAxisID, eDirection, eEvent, hAction, eOrder);
        }

        public static void RemoveAxisEventListener(int nPlayerID, int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction)
        {
            MainRemoveAxisEventListener(nPlayerID, nAxisID, eDirection, eEvent, hAction);
        }

        public static void RemoveAxisEventListener(int nPlayerID, int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction
            , EventOrder eOrder)
        {
            MainRemoveAxisEventListener(nPlayerID, nAxisID, eDirection, eEvent, hAction, eOrder);
        }

        static void MainRemoveAxisEventListener(int nPlayerID, int nAxisID
            , DirectionType2D eDirection, AxisEventType eEvent, UnityAction hAction
            , EventOrder eOrder = EventOrder.Normal)
        {
            if (m_hBaseInstance == null)
                return;

            m_hBaseInstance.gameInputData
                .RemoveAxisEventListener(nPlayerID, nAxisID, eDirection, eEvent, hAction, eOrder);
        }

        /// <summary>
        /// Add input event listener for player id 0.
        /// </summary>
        /// <param name="nButtonID"></param>
        /// <param name="eInput"></param>
        /// <param name="hAction"></param>
        public static void AddInputEventListener(int nButtonID
            , GetInputType eInput, UnityAction hAction)
        {
            MainAddInputEventListener(0, nButtonID, eInput, hAction);
        }

        /// <summary>
        /// Add input event listener for player id 0.
        /// </summary>
        /// <param name="nButtonID"></param>
        /// <param name="eInput"></param>
        /// <param name="hAction"></param>
        /// <param name="eOrder"></param>
        public static void AddInputEventListener(int nButtonID
            , GetInputType eInput, UnityAction hAction, EventOrder eOrder)
        {
            MainAddInputEventListener(0, nButtonID, eInput, hAction, eOrder);
        }


        public static void AddInputEventListener(int nPlayerID, int nButtonID
            , GetInputType eInput, UnityAction hAction)
        {
            MainAddInputEventListener(nPlayerID, nButtonID, eInput, hAction);
        }

        public static void AddInputEventListener(int nPlayerID, int nButtonID
            , GetInputType eInput, UnityAction hAction, EventOrder eOrder)
        {
            MainAddInputEventListener(nPlayerID, nButtonID, eInput, hAction, eOrder);
        }

        public static void AddInputEventListener(int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction)
        {
            MainAddInputEventListener(0, nButtonID, GetInputType.Down, hDownAction);
            MainAddInputEventListener(0, nButtonID, GetInputType.Hold, hHoldAction);
            MainAddInputEventListener(0, nButtonID, GetInputType.Up, hUpAction);
        }

        public static void AddInputEventListener(int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction
            , EventOrder eOrder)
        {
            MainAddInputEventListener(0, nButtonID, GetInputType.Down, hDownAction, eOrder);
            MainAddInputEventListener(0, nButtonID, GetInputType.Hold, hHoldAction, eOrder);
            MainAddInputEventListener(0, nButtonID, GetInputType.Up, hUpAction, eOrder);
        }

        public static void AddInputEventListener(int nPlayerID, int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction)
        {
            MainAddInputEventListener(nPlayerID, nButtonID, GetInputType.Down, hDownAction);
            MainAddInputEventListener(nPlayerID, nButtonID, GetInputType.Hold, hHoldAction);
            MainAddInputEventListener(nPlayerID, nButtonID, GetInputType.Up, hUpAction);
        }

        public static void AddInputEventListener(int nPlayerID, int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction
            , EventOrder eOrder)
        {
            MainAddInputEventListener(nPlayerID, nButtonID, GetInputType.Down, hDownAction, eOrder);
            MainAddInputEventListener(nPlayerID, nButtonID, GetInputType.Hold, hHoldAction, eOrder);
            MainAddInputEventListener(nPlayerID, nButtonID, GetInputType.Up, hUpAction, eOrder);
        }

        static void MainAddInputEventListener(int nPlayerID, int nButtonID
            , GetInputType eInput, UnityAction hAction, EventOrder eOrder = EventOrder.Normal)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.gameInputData.AddInputEventListener(nPlayerID, nButtonID, eInput, hAction, eOrder);
        }

        /// <summary>
        /// Remove input event listener from player id 0.
        /// </summary>
        /// <param name="nButtonID"></param>
        /// <param name="eInput"></param>
        /// <param name="hAction"></param>
        public static void RemoveInputEventListener(int nButtonID
                    , GetInputType eInput, UnityAction hAction)
        {
            MainRemoveInputEventListener(0, nButtonID, eInput, hAction);
        }

        /// <summary>
        /// Remove input event listener from player id 0.
        /// </summary>
        /// <param name="nButtonID"></param>
        /// <param name="eInput"></param>
        /// <param name="hAction"></param>
        /// <param name="eOrder"></param>
        public static void RemoveInputEventListener(int nButtonID
                    , GetInputType eInput, UnityAction hAction, EventOrder eOrder)
        {
            MainRemoveInputEventListener(0, nButtonID, eInput, hAction, eOrder);
        }


        public static void RemoveInputEventListener(int nPlayerID, int nButtonID
            , GetInputType eInput, UnityAction hAction)
        {
            MainRemoveInputEventListener(nPlayerID, nButtonID, eInput, hAction);
        }

        public static void RemoveInputEventListener(int nPlayerID, int nButtonID
            , GetInputType eInput, UnityAction hAction, EventOrder eOrder)
        {
            MainRemoveInputEventListener(nPlayerID, nButtonID, eInput, hAction, eOrder);
        }

        public static void RemoveInputEventListener(int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction)
        {
            MainRemoveInputEventListener(0, nButtonID, GetInputType.Down, hDownAction);
            MainRemoveInputEventListener(0, nButtonID, GetInputType.Hold, hHoldAction);
            MainRemoveInputEventListener(0, nButtonID, GetInputType.Up, hUpAction);
        }

        public static void RemoveInputEventListener(int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction
            , EventOrder eOrder)
        {
            MainRemoveInputEventListener(0, nButtonID, GetInputType.Down, hDownAction, eOrder);
            MainRemoveInputEventListener(0, nButtonID, GetInputType.Hold, hHoldAction, eOrder);
            MainRemoveInputEventListener(0, nButtonID, GetInputType.Up, hUpAction, eOrder);
        }

        public static void RemoveInputEventListener(int nPlayerID, int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction)
        {
            MainRemoveInputEventListener(nPlayerID, nButtonID, GetInputType.Down, hDownAction);
            MainRemoveInputEventListener(nPlayerID, nButtonID, GetInputType.Hold, hHoldAction);
            MainRemoveInputEventListener(nPlayerID, nButtonID, GetInputType.Up, hUpAction);
        }

        public static void RemoveInputEventListener(int nPlayerID, int nButtonID
            , UnityAction hDownAction, UnityAction hHoldAction, UnityAction hUpAction
            , EventOrder eOrder)
        {
            MainRemoveInputEventListener(nPlayerID, nButtonID, GetInputType.Down, hDownAction, eOrder);
            MainRemoveInputEventListener(nPlayerID, nButtonID, GetInputType.Hold, hHoldAction, eOrder);
            MainRemoveInputEventListener(nPlayerID, nButtonID, GetInputType.Up, hUpAction, eOrder);
        }

        static void MainRemoveInputEventListener(int nPlayerID, int nButtonID
            , GetInputType eInput, UnityAction hAction, EventOrder eOrder = EventOrder.Normal)
        {
            if (m_hBaseInstance == null)
                return;

            m_hBaseInstance.gameInputData.RemoveInputEventListener(nPlayerID, nButtonID, eInput, hAction);
        }

        #endregion

        public static void UpdateInput()
        {
            m_hBaseInstance.inputEvent?.Run(DSC_InputEventType.PreUpdateInput, m_hBaseInstance.gameInputData);
            m_hBaseInstance.gameInputData.OnUpdate();
            m_hBaseInstance.inputEvent?.Run(DSC_InputEventType.PostUpdateInput, m_hBaseInstance.gameInputData);
        }

        public static void LateUpdateInput()
        {
            m_hBaseInstance.inputEvent?.Run(DSC_InputEventType.PreLateUpdateInput, m_hBaseInstance.gameInputData);
            m_hBaseInstance.gameInputData.OnLateUpdate();
            m_hBaseInstance.inputEvent?.Run(DSC_InputEventType.PostLateUpdateInput, m_hBaseInstance.gameInputData);
        }

        #region Get/Set Input

        public static Vector2 GetAnyRawAxis()
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetAnyRawAxis();
        }

        public static Vector2 GetAnyRawAxis(int nAxisID)
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetAnyRawAxis(nAxisID);
        }

        public static Vector2 GetRawAxis()
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetRawAxis(0);
        }

        public static Vector2 GetRawAxis(int nPlayerID)
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetRawAxis(nPlayerID);
        }

        public static Vector2 GetRawAxis(int nPlayerID, int nAxisID)
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetRawAxis(nPlayerID, nAxisID);
        }

        public static void SetRawAxis(Vector2 vAxis)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.gameInputData.SetRawAxis(0, vAxis);
        }

        public static void SetRawAxis(int nPlayerID, Vector2 vAxis)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.gameInputData.SetRawAxis(nPlayerID, vAxis);
        }

        public static void SetRawAxis(int nPlayerID, int nAxisID, Vector2 vAxis)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.gameInputData.SetRawAxis(nPlayerID, nAxisID, vAxis);
        }

        public static Vector2 GetAnyAxis()
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetAnyAxis();
        }

        public static Vector2 GetAnyAxis(int nAxisID)
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetAnyAxis(nAxisID);
        }

        public static Vector2 GetAxis()
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetAxis(0);
        }

        public static Vector2 GetAxis(int nPlayerID)
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetAxis(nPlayerID);
        }

        public static Vector2 GetAxis(int nPlayerID, int nAxisID)
        {
            if (!HasBaseInstance())
                return Vector2.zero;

            return m_hBaseInstance.gameInputData.GetAxis(nPlayerID, nAxisID);
        }

        public static DirectionType2D GetAnyAxisEvent(AxisEventType eEventType)
        {
            if (!HasBaseInstance())
                return 0;

            return m_hBaseInstance.gameInputData.GetAnyAxisEvent(eEventType);
        }

        public static DirectionType2D GetAnyAxisEvent(AxisEventType eEventType, int nAxisID)
        {
            if (!HasBaseInstance())
                return 0;

            return m_hBaseInstance.gameInputData.GetAnyAxisEvent(eEventType, nAxisID);
        }

        public static DirectionType2D GetAxisEvent(int nPlayerID, AxisEventType eEventType)
        {
            if (!HasBaseInstance())
                return 0;

            return m_hBaseInstance.gameInputData.GetAxisEvent(nPlayerID, eEventType);
        }

        public static DirectionType2D GetAxisEvent(int nPlayerID, AxisEventType eEventType, int nAxisID)
        {
            if (!HasBaseInstance())
                return 0;

            return m_hBaseInstance.gameInputData.GetAxisEvent(nPlayerID, eEventType, nAxisID);
        }

        public static GetInputType GetButtonInput(int nButtonID)
        {
            if (!HasBaseInstance())
                return GetInputType.None;

            return m_hBaseInstance.gameInputData.GetButtonInput(nButtonID);
        }

        public static GetInputType GetButtonInput(int nPlayerID, int nButtonID)
        {
            if (!HasBaseInstance())
                return GetInputType.None;

            return m_hBaseInstance.gameInputData.GetButtonInput(nPlayerID, nButtonID);
        }

        public static void SetButtonInput(int nPlayerID, int nButtonID, GetInputType eInput)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.gameInputData.SetButtonInput(nPlayerID, nButtonID, eInput);
        }

        public static void SetButtonInput(int nPlayerID, int nButtonID, bool bPress)
        {
            if (!HasBaseInstance())
                return;

            m_hBaseInstance.gameInputData.SetButtonInput(nPlayerID, nButtonID, bPress);
        }

        public static bool GetAnyButtonDown()
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetAnyButtonDown();
        }

        public static bool GetAnyButtonDown(int nPlayerID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetAnyButtonDown(nPlayerID);
        }

        public static bool GetButtonDown(int nButtonID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetButtonDown(nButtonID);
        }

        public static bool GetButtonDown(int nPlayerID, int nButtonID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetButtonDown(nPlayerID, nButtonID);
        }

        public static bool GetAnyButtonHold()
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetAnyButtonHold();
        }

        public static bool GetAnyButtonHold(int nPlayerID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetAnyButtonHold(nPlayerID);
        }

        public static bool GetButtonHold(int nButtonID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetButtonHold(nButtonID);
        }

        public static bool GetButtonHold(int nPlayerID, int nButtonID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetButtonHold(nPlayerID, nButtonID);
        }

        public static bool GetAnyButtonUp()
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetAnyButtonUp();
        }

        public static bool GetAnyButtonUp(int nPlayerID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetAnyButtonUp(nPlayerID);
        }

        public static bool GetButtonUp(int nButtonID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetButtonUp(nButtonID);
        }

        public static bool GetButtonUp(int nPlayerID, int nButtonID)
        {
            if (!HasBaseInstance())
                return false;

            return m_hBaseInstance.gameInputData.GetButtonUp(nPlayerID, nButtonID);
        }

        public static int GetAllButtonDownFlag(int nPlayerID)
        {
            if (!HasBaseInstance())
                return 0;

            return m_hBaseInstance.gameInputData.GetAllButtonDownFlag(nPlayerID);
        }

        public static int GetAllButtonHoldFlag(int nPlayerID)
        {
            if (!HasBaseInstance())
                return 0;

            return m_hBaseInstance.gameInputData.GetAllButtonHoldFlag(nPlayerID);
        }

        public static int GetAllButtonUpFlag(int nPlayerID)
        {
            if (!HasBaseInstance())
                return 0;

            return m_hBaseInstance.gameInputData.GetAllButtonUpFlag(nPlayerID);
        }

        #endregion

        #endregion

        #region Helper

        static bool HasBaseInstance()
        {
            bool bResult = m_hBaseInstance != null;

            if (!bResult)
            {
                Debug.LogWarning("Don't have DSC_Input derived class in scene.");
            }

            return bResult;
        }

        #endregion
    }
}