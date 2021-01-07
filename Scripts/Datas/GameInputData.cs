using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Input
{
    public struct GameInputData
    {
        #region Data

        struct AxisData
        {
            public Vector2 m_vRawAxis;
            public Vector2 m_vAxis;

            public DirectionType2D m_eHorizontalPress;
            public DirectionType2D m_eHorizontalDoublePress;
            public DirectionType2D m_eHorizontalTap;
            public DirectionType2D m_eHorizontalDoubleTap;

            public DirectionType2D m_eLastHorizontalTap;
            public float m_fHorizontalPressTime;
            public float m_fHorizontalReleaseTime;

            public DirectionType2D m_eVerticalPress;
            public DirectionType2D m_eVerticalDoublePress;
            public DirectionType2D m_eVerticalTap;
            public DirectionType2D m_eVerticalDoubleTap;

            public DirectionType2D m_eLastVerticalTap;
            public float m_fVerticalPressTime;
            public float m_fVerticalReleaseTime;
        }

        struct InputData
        {
            public AxisData[] m_arrAxis;
            public Dictionary<int, ButtonData> m_dicButton;
            public int m_nAllDownFlag;
            public int m_nAllHoldFlag;
            public int m_nAllUpFlag;
        }

        class ButtonData
        {
            public GetInputType m_eGetType;
        }

        #endregion

        #region Variable

        #region Variable - Property

        public bool isCreate { get; private set; }

        #endregion

        readonly List<InputData> m_lstPlayerInput;
        readonly float m_fSensitivity;
        readonly float m_fGravity;
        readonly int m_nAxisNumber;


        #endregion

        public GameInputData(int nPlayerNumber)
        {
            isCreate = true;

            m_lstPlayerInput = new List<InputData>();
            m_nAxisNumber = 2;
            m_fSensitivity = 3f;
            m_fGravity = 3f;

            InitData(nPlayerNumber);
        }

        public GameInputData(int nPlayerNumber, int nAxisNumber)
        {
            isCreate = true;

            m_lstPlayerInput = new List<InputData>();
            m_nAxisNumber = nAxisNumber;
            m_fSensitivity = 3f;
            m_fGravity = 3f;

            InitData(nPlayerNumber);
        }

        public GameInputData(int nPlayerNumber, float fSensitivity, float fGravity)
        {
            isCreate = true;

            m_lstPlayerInput = new List<InputData>();
            m_nAxisNumber = 2;
            m_fSensitivity = fSensitivity;
            m_fGravity = fGravity;

            InitData(nPlayerNumber);
        }

        public GameInputData(int nPlayerNumber, int nAxisNumber, float fSensitivity, float fGravity)
        {
            isCreate = true;

            m_lstPlayerInput = new List<InputData>();
            m_nAxisNumber = nAxisNumber;
            m_fSensitivity = fSensitivity;
            m_fGravity = fGravity;

            InitData(nPlayerNumber);
        }

        #region Main

        #region Main - Update

        public void OnUpdate()
        {
            float fTime = Time.unscaledTime;
            float fDeltaTime = Time.unscaledDeltaTime;
            float fDelayClear = 0.2f;
            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                var hInput = m_lstPlayerInput[i];
                for (int j = 0; j < hInput.m_arrAxis.Length; j++)
                {
                    var hAxis = hInput.m_arrAxis[j];
                    if (hAxis.m_eLastHorizontalTap != 0)
                    {
                        if (fTime >= hAxis.m_fHorizontalReleaseTime + fDelayClear)
                        {
                            hAxis.m_eLastHorizontalTap = 0;
                        }
                    }

                    if (hAxis.m_eLastVerticalTap != 0)
                    {
                        if (fTime >= hAxis.m_fVerticalReleaseTime + fDelayClear)
                        {
                            hAxis.m_eLastVerticalTap = 0;
                        }
                    }

                    if (hAxis.m_vAxis != hAxis.m_vRawAxis)
                    {
                        InputUtility.CalculateAxis(ref hAxis.m_vAxis.x, hAxis.m_vRawAxis.x, m_fSensitivity, m_fGravity, fDeltaTime);
                        InputUtility.CalculateAxis(ref hAxis.m_vAxis.y, hAxis.m_vRawAxis.y, m_fSensitivity, m_fGravity, fDeltaTime);
                    }

                    hInput.m_arrAxis[j] = hAxis;
                }

                m_lstPlayerInput[i] = hInput;
            }
        }

        public void OnLateUpdate()
        {
            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                var hInput = m_lstPlayerInput[i];

                for (int j = 0; j < hInput.m_arrAxis.Length; j++)
                {
                    var hAxis = hInput.m_arrAxis[j];
                    hAxis.m_eHorizontalPress = 0;
                    hAxis.m_eHorizontalDoublePress = 0;
                    hAxis.m_eHorizontalTap = 0;
                    hAxis.m_eHorizontalDoubleTap = 0;
                    hAxis.m_eVerticalPress = 0;
                    hAxis.m_eVerticalDoublePress = 0;
                    hAxis.m_eVerticalTap = 0;
                    hAxis.m_eVerticalDoubleTap = 0;
                    hInput.m_arrAxis[j] = hAxis;
                }

                foreach (var hButton in hInput.m_dicButton)
                {
                    var hButtonID = hButton.Key;
                    var hValue = hButton.Value;
                    switch (hValue.m_eGetType)
                    {
                        case GetInputType.Down:
                            hValue.m_eGetType = GetInputType.Hold;
                            hInput.m_nAllDownFlag &= ~hButtonID;
                            hInput.m_nAllHoldFlag |= hButtonID;
                            break;

                        case GetInputType.Up:
                            hValue.m_eGetType = GetInputType.None;
                            hInput.m_nAllUpFlag &= ~hButtonID;
                            break;
                    }
                }

                m_lstPlayerInput[i] = hInput;
            }
        }

        #endregion

        public void ChangePlayerNumber(int nPlayerNumber)
        {
            if (nPlayerNumber < 1)
            {
                Debug.LogError("Can't change player number in GameInputData to " + nPlayerNumber);
                return;
            }

            int nNeedAdd = m_lstPlayerInput.Count - nPlayerNumber;
            if (nNeedAdd == 0)
                return;

            bool bAdd = nNeedAdd > 0;
            int nLoop = bAdd ? nNeedAdd : -nNeedAdd;
            for (int i = 0; i < nLoop; i++)
            {
                if (bAdd)
                    m_lstPlayerInput.Add(new InputData());
                else
                    m_lstPlayerInput.RemoveAtLast();
            }
        }

        public Vector2 GetAnyRawAxis()
        {
            Vector2 vResult = Vector2.zero;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                vResult = MainGetRawAxis(i);
                if (vResult != Vector2.zero)
                    break;
            }

            return vResult;
        }

        public Vector2 GetAnyRawAxis(int nAxisID)
        {
            Vector2 vResult = Vector2.zero;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                vResult = MainGetRawAxis(i, nAxisID);
                if (vResult != Vector2.zero)
                    break;
            }

            return vResult;
        }

        public Vector2 GetRawAxis(int nPlayerID)
        {
            return MainGetRawAxis(nPlayerID);
        }

        public Vector2 GetRawAxis(int nPlayerID, int nAxisID)
        {
            return MainGetRawAxis(nPlayerID, nAxisID);
        }

        Vector2 MainGetRawAxis(int nPlayerID, int nAxisID = 0)
        {
            if (!HasPlayerID(nPlayerID) || !HasAxisID(nPlayerID, nAxisID))
                return default;

            return m_lstPlayerInput[nPlayerID].m_arrAxis[nAxisID].m_vRawAxis;
        }

        public void SetRawAxis(int nPlayerID, Vector2 vAxis)
        {
            MainSetRawAxis(nPlayerID, vAxis);
        }

        public void SetRawAxis(int nPlayerID, int nAxisID, Vector2 vAxis)
        {
            MainSetRawAxis(nPlayerID, vAxis, nAxisID);
        }

        void MainSetRawAxis(int nPlayerID, Vector2 vAxis, int nAxisID = 0)
        {
            if (!HasPlayerID(nPlayerID) || !HasAxisID(nPlayerID, nAxisID))
                return;

            float fTime = Time.unscaledTime;
            var hAxis = m_lstPlayerInput[nPlayerID].m_arrAxis[nAxisID];
            Vector2 vPreviousAxis = hAxis.m_vRawAxis;
            UpdateHorizontal();
            UpdateVertical();
            hAxis.m_vRawAxis = vAxis;
            m_lstPlayerInput[nPlayerID].m_arrAxis[nAxisID] = hAxis;


            #region Method

            void UpdateHorizontal()
            {
                var ePrevious = vPreviousAxis.x > 0 ? DirectionType2D.Right : DirectionType2D.Left;
                var eNext = vAxis.x > 0 ? DirectionType2D.Right : DirectionType2D.Left;

                if (vAxis.x != 0 && vPreviousAxis.x == 0)
                {
                    hAxis.m_eHorizontalPress = eNext;
                    hAxis.m_fHorizontalPressTime = fTime;

                    if (hAxis.m_eLastHorizontalTap != 0 && hAxis.m_eLastHorizontalTap == eNext)
                    {
                        hAxis.m_eHorizontalDoublePress = eNext;
                    }
                }
                else if (vAxis.x == 0 && vPreviousAxis.x != 0)
                {
                    hAxis.m_fHorizontalReleaseTime = fTime;

                    if (fTime < hAxis.m_fHorizontalPressTime + 0.2f)
                    {
                        if (hAxis.m_eLastHorizontalTap != 0 && hAxis.m_eLastHorizontalTap == ePrevious)
                        {
                            hAxis.m_eHorizontalDoubleTap = ePrevious;
                        }

                        hAxis.m_eHorizontalTap = ePrevious;
                        hAxis.m_eLastHorizontalTap = ePrevious;
                    }
                    else
                    {
                        hAxis.m_eLastHorizontalTap = 0;
                    }
                }
            }

            void UpdateVertical()
            {
                var ePrevious = vPreviousAxis.y > 0 ? DirectionType2D.Up : DirectionType2D.Down;
                var eNext = vAxis.y > 0 ? DirectionType2D.Up : DirectionType2D.Down;

                if (vAxis.y != 0 && vPreviousAxis.y == 0)
                {
                    hAxis.m_eVerticalPress = eNext;
                    hAxis.m_fVerticalPressTime = fTime;

                    if (hAxis.m_eLastVerticalTap != 0 && hAxis.m_eLastVerticalTap == eNext)
                    {
                        hAxis.m_eVerticalDoublePress = eNext;
                    }
                }
                else if (vAxis.y == 0 && vPreviousAxis.y != 0)
                {
                    hAxis.m_fVerticalReleaseTime = fTime;

                    if (fTime < hAxis.m_fVerticalPressTime + 0.2f)
                    {
                        if (hAxis.m_eLastVerticalTap != 0 && hAxis.m_eLastVerticalTap == ePrevious)
                        {
                            hAxis.m_eVerticalDoubleTap = ePrevious;
                        }

                        hAxis.m_eVerticalTap = ePrevious;
                        hAxis.m_eLastVerticalTap = ePrevious;
                    }
                    else
                    {
                        hAxis.m_eLastVerticalTap = 0;
                    }
                }
            }

            #endregion
        }

        public Vector2 GetAnyAxis()
        {
            Vector2 vResult = Vector2.zero;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                vResult = MainGetAxis(i);
                if (vResult != Vector2.zero)
                    break;
            }

            return vResult;
        }

        public Vector2 GetAnyAxis(int nAxisID)
        {
            Vector2 vResult = Vector2.zero;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                vResult = MainGetAxis(i, nAxisID);
                if (vResult != Vector2.zero)
                    break;
            }

            return vResult;
        }

        public Vector2 GetAxis(int nPlayerID)
        {
            return MainGetAxis(nPlayerID);
        }

        public Vector2 GetAxis(int nPlayerID, int nAxisID)
        {
            return MainGetAxis(nPlayerID, nAxisID);
        }

        Vector2 MainGetAxis(int nPlayerID, int nAxisID = 0)
        {
            if (!HasPlayerID(nPlayerID) || !HasAxisID(nPlayerID, nAxisID))
                return Vector2.zero;

            return m_lstPlayerInput[nPlayerID].m_arrAxis[nAxisID].m_vAxis;
        }

        public DirectionType2D GetAnyAxisEvent(AxisEventType eEventType)
        {
            return MainGetAnyAxisEvent(eEventType);
        }

        public DirectionType2D GetAnyAxisEvent(AxisEventType eEventType,int nAxisID)
        {
            return MainGetAnyAxisEvent(eEventType, nAxisID);
        }

        public DirectionType2D GetAxisEvent(int nPlayerID,AxisEventType eEventType)
        {
            return MainGetAxisEvent(nPlayerID, eEventType);
        }

        public DirectionType2D GetAxisEvent(int nPlayerID, AxisEventType eEventType,int nAxisID)
        {
            return MainGetAxisEvent(nPlayerID, eEventType, nAxisID);
        }

        public GetInputType GetButtonInput(int nButtonID)
        {
            var eResult = GetInputType.None;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                eResult = MainGetButtonInput(i, nButtonID);
                if (eResult != GetInputType.None)
                    break;
            }

            return eResult;
        }

        public GetInputType GetButtonInput(int nPlayerID, int nButtonID)
        {
            return MainGetButtonInput(nPlayerID, nButtonID);
        }

        GetInputType MainGetButtonInput(int nPlayerID, int nButtonID)
        {
            if (!HasPlayerID(nPlayerID))
                return default;

            var hInputData = m_lstPlayerInput[nPlayerID];
            if (hInputData.m_dicButton.TryGetValue(nButtonID, out var hButtonData))
                return hButtonData.m_eGetType;

            return default;
        }

        public void SetButtonInput(int nPlayerID, int nButtonID, GetInputType eInput)
        {
            MainSetButtonInput(nPlayerID, nButtonID, eInput);
        }

        public void SetButtonInput(int nPlayerID, int nButtonID, bool bPress)
        {
            var eInput = ParseButtonPress(bPress);
            MainSetButtonInput(nPlayerID, nButtonID, eInput);
        }

        void MainSetButtonInput(int nPlayerID, int nButtonID, GetInputType eInput)
        {
            if (!HasPlayerID(nPlayerID))
                return;

            var hInputData = m_lstPlayerInput[nPlayerID];
            ResetFlag();
            AddFlag();

            if (hInputData.m_dicButton.TryGetValue(nButtonID, out var hButtonData))
            {
                hButtonData.m_eGetType = eInput;
            }
            else
            {
                hInputData.m_dicButton.Add(nButtonID, new ButtonData
                {
                    m_eGetType = eInput
                });
            }

            m_lstPlayerInput[nPlayerID] = hInputData;

            #region Method

            void ResetFlag()
            {
                hInputData.m_nAllDownFlag &= ~nButtonID;
                hInputData.m_nAllHoldFlag &= ~nButtonID;
                hInputData.m_nAllUpFlag &= ~nButtonID;
            }

            void AddFlag()
            {
                switch (eInput)
                {
                    case GetInputType.Down:
                        hInputData.m_nAllDownFlag |= nButtonID;
                        break;

                    case GetInputType.Up:
                        hInputData.m_nAllUpFlag |= nButtonID;
                        break;
                }
            }

            #endregion
        }

        public bool GetAnyButtonDown()
        {
            bool bResult = false;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                foreach (var hButton in m_lstPlayerInput[i].m_dicButton)
                {
                    if (hButton.Value == null)
                        continue;

                    bResult = IsButtonDown(hButton.Value.m_eGetType);
                    if (bResult)
                        goto Finish;
                }
            }

        Finish:
            ;

            return bResult;
        }

        public bool GetAnyButtonDown(int nPlayerID)
        {
            bool bResult = false;
            if (!HasPlayerID(nPlayerID))
                goto Finish;

            foreach (var hButton in m_lstPlayerInput[nPlayerID].m_dicButton)
            {
                if (hButton.Value == null)
                    continue;

                bResult = IsButtonDown(hButton.Value.m_eGetType);
                if (bResult)
                    goto Finish;
            }

        Finish:
            ;

            return bResult;
        }

        public bool GetButtonDown(int nButtonID)
        {
            bool bResult = false;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                bResult = MainGetButtonDown(i, nButtonID);
                if (bResult)
                    break;
            }

            return bResult;
        }

        public bool GetButtonDown(int nPlayerID, int nButtonID)
        {
            return MainGetButtonDown(nPlayerID, nButtonID);
        }


        bool MainGetButtonDown(int nPlayerID, int nButtonID)
        {
            var eInput = MainGetButtonInput(nPlayerID, nButtonID);
            return IsButtonDown(eInput);
        }

        public bool GetAnyButtonHold()
        {
            bool bResult = false;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                foreach (var hButton in m_lstPlayerInput[i].m_dicButton)
                {
                    if (hButton.Value == null)
                        continue;

                    bResult = IsButtonHold(hButton.Value.m_eGetType);
                    if (bResult)
                        goto Finish;
                }
            }

        Finish:
            ;

            return bResult;
        }

        public bool GetAnyButtonHold(int nPlayerID)
        {
            bool bResult = false;
            if (!HasPlayerID(nPlayerID))
                goto Finish;

            foreach (var hButton in m_lstPlayerInput[nPlayerID].m_dicButton)
            {
                if (hButton.Value == null)
                    continue;

                bResult = IsButtonHold(hButton.Value.m_eGetType);
                if (bResult)
                    goto Finish;
            }

        Finish:
            ;

            return bResult;
        }

        public bool GetButtonHold(int nButtonID)
        {
            bool bResult = false;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                bResult = MainGetButtonHold(i, nButtonID);
                if (bResult)
                    break;
            }

            return bResult;
        }

        public bool GetButtonHold(int nPlayerID, int nButtonID)
        {
            return MainGetButtonHold(nPlayerID, nButtonID);
        }

        bool MainGetButtonHold(int nPlayerID, int nButtonID)
        {
            var eInput = MainGetButtonInput(nPlayerID, nButtonID);
            return IsButtonHold(eInput);
        }

        public bool GetAnyButtonUp()
        {
            bool bResult = false;
            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                foreach (var hButton in m_lstPlayerInput[i].m_dicButton)
                {
                    if (hButton.Value == null)
                        continue;

                    bResult = IsButtonUp(hButton.Value.m_eGetType);
                    if (bResult)
                        goto Finish;
                }
            }

        Finish:
            ;

            return bResult;
        }

        public bool GetAnyButtonUp(int nPlayerID)
        {
            bool bResult = false;
            if (!HasPlayerID(nPlayerID))
                goto Finish;

            foreach (var hButton in m_lstPlayerInput[nPlayerID].m_dicButton)
            {
                if (hButton.Value == null)
                    continue;

                bResult = IsButtonUp(hButton.Value.m_eGetType);
                if (bResult)
                    goto Finish;
            }

        Finish:
            ;

            return bResult;
        }

        public bool GetButtonUp(int nButtonID)
        {
            bool bResult = false;
            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                bResult = MainGetButtonUp(i, nButtonID);
                if (bResult)
                    break;
            }

            return bResult;
        }

        public bool GetButtonUp(int nPlayerID, int nButtonID)
        {
            return MainGetButtonUp(nPlayerID, nButtonID);
        }

        bool MainGetButtonUp(int nPlayerID, int nButtonID)
        {
            var eInput = MainGetButtonInput(nPlayerID, nButtonID);
            return IsButtonUp(eInput);
        }

        public int GetAllButtonDownFlag(int nPlayerID)
        {
            if (!HasPlayerID(nPlayerID))
                return 0;

            return m_lstPlayerInput[nPlayerID].m_nAllDownFlag;
        }

        public int GetAllButtonHoldFlag(int nPlayerID)
        {
            if (!HasPlayerID(nPlayerID))
                return 0;

            return m_lstPlayerInput[nPlayerID].m_nAllHoldFlag;
        }

        public int GetAllButtonUpFlag(int nPlayerID)
        {
            if (!HasPlayerID(nPlayerID))
                return 0;

            return m_lstPlayerInput[nPlayerID].m_nAllUpFlag;
        }

        #endregion

        #region Helper

        void InitData(int nPlayerNumber)
        {
            if (nPlayerNumber < 1)
            {
                Debug.LogError("Can't init GameInputData with " + nPlayerNumber + " Player number.");
                return;
            }

            for (int i = 0; i < nPlayerNumber; i++)
            {
                m_lstPlayerInput.Add(GetNewInputData(m_nAxisNumber));
            }
        }

        InputData GetNewInputData(int nAxisNumber)
        {
            return new InputData
            {
                m_arrAxis = new AxisData[nAxisNumber],
                m_dicButton = new Dictionary<int, ButtonData>()
            };
        }

        bool HasPlayerID(int nPlayerID)
        {
            if (nPlayerID < 0 && nPlayerID >= m_lstPlayerInput.Count)
            {
                Debug.LogError("Can't get button input by " + nPlayerID + " player ID");
                return false;
            }

            return true;
        }

        bool HasAxisID(int nPlayerID, int nAxisID)
        {
            if (nAxisID < 0 || nAxisID >= m_lstPlayerInput[nPlayerID].m_arrAxis.Length)
            {
                Debug.LogError("Don't have this Axis ID");
                return false;
            }

            return true;
        }

        GetInputType ParseButtonPress(bool bPress)
        {
            return bPress ? GetInputType.Down : GetInputType.Up;
        }

        bool IsButtonDown(GetInputType eInput)
        {
            return eInput == GetInputType.Down;
        }

        bool IsButtonHold(GetInputType eInput)
        {
            return eInput == GetInputType.Hold;
        }

        bool IsButtonUp(GetInputType eInput)
        {
            return eInput == GetInputType.Up;
        }

        DirectionType2D MainGetAnyAxisEvent(AxisEventType eEventType)
        {
            DirectionType2D eResult;

            eResult = MainGetAnyAxisEvent(AxisType.Horizontal, eEventType);
            eResult |= MainGetAnyAxisEvent(AxisType.Vertical, eEventType);

            return eResult;
        }

        DirectionType2D MainGetAnyAxisEvent(AxisType eAxis, AxisEventType eEventType)
        {
            DirectionType2D eResult = 0;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                for (int j = 0; j < m_lstPlayerInput[i].m_arrAxis.Length; j++)
                {
                    eResult = MainGetAxisEvent(i, eAxis, eEventType, j);
                    if (eResult != 0)
                        goto Finish;
                }
            }

        Finish:
            ;

            return eResult;
        }

        DirectionType2D MainGetAnyAxisEvent(AxisEventType eEventType, int nAxisID)
        {
            DirectionType2D eResult;

            eResult = MainGetAnyAxisEvent(AxisType.Horizontal, eEventType, nAxisID);
            eResult |= MainGetAnyAxisEvent(AxisType.Vertical, eEventType, nAxisID);

            return eResult;
        }

        DirectionType2D MainGetAnyAxisEvent(AxisType eAxis, AxisEventType eEventType, int nAxisID)
        {
            DirectionType2D eResult = 0;

            for (int i = 0; i < m_lstPlayerInput.Count; i++)
            {
                eResult = MainGetAxisEvent(i, eAxis, eEventType, nAxisID);
                if (eResult != 0)
                    goto Finish;
            }

        Finish:
            ;

            return eResult;
        }

        DirectionType2D MainGetAxisEvent(int nPlayerID, AxisEventType eEventType, int nAxisID = 0)
        {
            DirectionType2D eResult;

            eResult = MainGetAxisEvent(nPlayerID, AxisType.Horizontal, eEventType, nAxisID);
            eResult |= MainGetAxisEvent(nPlayerID, AxisType.Vertical, eEventType, nAxisID);

            return eResult;
        }

        DirectionType2D MainGetAxisEvent(int nPlayerID, AxisType eAxis, AxisEventType eEventType, int nAxisID = 0)
        {
            DirectionType2D eResult = 0;

            if (!HasPlayerID(nPlayerID) || !HasAxisID(nPlayerID, nAxisID))
                return eResult;

            var hAxisData = m_lstPlayerInput[nPlayerID].m_arrAxis[nAxisID];

            switch (eAxis)
            {
                case AxisType.Horizontal:
                    eResult = GetHorizontalResult();
                    break;

                case AxisType.Vertical:
                    eResult = GetVerticalResult();
                    break;
            }

            return eResult;

            #region Method

            DirectionType2D GetHorizontalResult()
            {
                switch (eEventType)
                {
                    case AxisEventType.Press:
                        return hAxisData.m_eHorizontalPress;

                    case AxisEventType.DoublePress:
                        return hAxisData.m_eHorizontalDoublePress;

                    case AxisEventType.Tap:
                        return hAxisData.m_eHorizontalTap;

                    case AxisEventType.DoubleTap:
                        return hAxisData.m_eHorizontalDoubleTap;
                }

                return 0;
            }

            DirectionType2D GetVerticalResult()
            {
                switch (eEventType)
                {
                    case AxisEventType.Press:
                        return hAxisData.m_eVerticalPress;

                    case AxisEventType.DoublePress:
                        return hAxisData.m_eVerticalDoublePress;

                    case AxisEventType.Tap:
                        return hAxisData.m_eVerticalTap;

                    case AxisEventType.DoubleTap:
                        return hAxisData.m_eVerticalDoubleTap;
                }

                return 0;
            }

            #endregion
        }


        #endregion
    }
}