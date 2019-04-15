using System;
using UnityEngine;

namespace Assets.Source
{
    public enum NailBoardStates
    {
        NAIL,
        MOVING_TO_NAIL
    }

    public class NailBoardComponent : MonoBehaviour
    {
        [SerializeField] private AnimationCurve m_TransitionCurve;
        [SerializeField] private float m_TimeOfNailTransition = 3.0f;
        private NailComponent[] m_NailComponents;

        private float m_CurrentTimeOfNailTransition;
        private Vector3 m_FromPosition;
        private Vector3 m_Destination;
        private int m_CurrentNailIndex;
        private NailBoardStates m_CurrentNailBoardState;

        // Start is called before the first frame update
        void Start()
        {
            m_NailComponents = GetComponentsInChildren<NailComponent>();
            m_CurrentNailIndex = 0;
            m_CurrentNailBoardState = NailBoardStates.NAIL;
            m_CurrentTimeOfNailTransition = 0.0f;


            if (m_NailComponents.Length <= 0)
            {
                Debug.LogError("No Nails On Board");
            }

        }

        // Update is called once per frame
        void Update()
        {
            switch (m_CurrentNailBoardState)
            {
                case NailBoardStates.NAIL:
                    // Don't Move
                    break;
                case NailBoardStates.MOVING_TO_NAIL:

                    float l_Ratio = m_CurrentTimeOfNailTransition / m_TimeOfNailTransition;
                    transform.position = Vector3.Lerp(m_FromPosition, m_Destination, m_TransitionCurve.Evaluate(l_Ratio));

                    if (l_Ratio >= 1)
                    {
                        m_CurrentNailBoardState = NailBoardStates.NAIL;
                        m_CurrentTimeOfNailTransition = 0.0f;
                        return;
                    }

                    m_CurrentTimeOfNailTransition += Time.deltaTime;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void MoveToNextNail()
        {

            if (m_CurrentNailIndex == m_NailComponents.Length - 1)
            {
                Debug.Log("On Last Nail");
                return;
            }

            // Move to next nail
            m_CurrentNailBoardState = NailBoardStates.MOVING_TO_NAIL;



            NailComponent l_CurrentNail = m_NailComponents[m_CurrentNailIndex];
            NailComponent l_NextNai = m_NailComponents[++m_CurrentNailIndex];
            m_FromPosition = transform.position;
            m_Destination = new Vector3(m_FromPosition.x + (l_CurrentNail.transform.position.x - l_NextNai.transform.position.x), m_FromPosition.y, m_FromPosition.z);
        }
    }
}
