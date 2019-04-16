using System;
using UnityEngine;

namespace Assets.Source
{
    public enum NailBoardStates
    {
        NAIL,
        MOVING_TO_NAIL,
        DESPAWNING,
        SPAWNING
    }

    public class NailBoardComponent : MonoBehaviour
    {
        [SerializeField] private AnimationCurve m_TransitionCurve;
        [SerializeField] private float m_TimeOfNailTransition = 3.0f;
        [SerializeField] private float m_TimeOfBoardTransition = 3.0f;
        private NailComponent[] m_NailComponents;

        private float m_CurrentTimeOfTransition;
        private Vector3 m_FromPosition;
        private Vector3 m_Destination;
        private int m_CurrentNailIndex;
        private NailBoardStates m_CurrentNailBoardState;


        void Awake()
        {
            m_NailComponents = GetComponentsInChildren<NailComponent>();
            m_CurrentNailIndex = 0;
            m_CurrentNailBoardState = NailBoardStates.NAIL;
            m_CurrentTimeOfTransition = 0.0f;
            m_FromPosition = transform.position;

            if (m_NailComponents.Length <= 0)
            {
                Debug.LogError("No Nails On Board");
            }
        }

        // Start is called before the first frame update
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

            float l_Ratio = 0.0f;

            switch (m_CurrentNailBoardState)
            {
                case NailBoardStates.NAIL:
                    // Don't Move
                    break;
                case NailBoardStates.MOVING_TO_NAIL:

                    l_Ratio = m_CurrentTimeOfTransition / m_TimeOfNailTransition;
                    transform.position = Vector3.Lerp(m_FromPosition, m_Destination, m_TransitionCurve.Evaluate(l_Ratio));

                    if (l_Ratio >= 1)
                    {
                        m_CurrentNailBoardState = NailBoardStates.NAIL;
                        m_CurrentTimeOfTransition = 0.0f;
                        return;
                    }

                    m_CurrentTimeOfTransition += Time.deltaTime;
                    break;
                case NailBoardStates.DESPAWNING:
                    l_Ratio = m_CurrentTimeOfTransition / m_TimeOfBoardTransition;
                    transform.position = Vector3.Lerp(m_FromPosition, m_Destination, m_TransitionCurve.Evaluate(l_Ratio));

                    if (l_Ratio >= 1)
                    {
                        Destroy(this.gameObject);
                        return;
                    }

                    m_CurrentTimeOfTransition += Time.deltaTime;
                    break;
                case NailBoardStates.SPAWNING:
                    l_Ratio = m_CurrentTimeOfTransition / m_TimeOfBoardTransition;
                    transform.position = Vector3.Lerp(m_FromPosition, m_Destination, m_TransitionCurve.Evaluate(l_Ratio));

                    if (l_Ratio >= 1)
                    {
                        m_CurrentNailBoardState = NailBoardStates.NAIL;
                        m_CurrentTimeOfTransition = 0.0f;
                        return;
                    }

                    m_CurrentTimeOfTransition += Time.deltaTime;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void MoveToNextNail()
        {

            if (m_CurrentNailIndex == m_NailComponents.Length - 1)
            {
                GameManager.Instance.MoveToNextBoard();
                return;
            }

            // Move to next nail
            m_CurrentNailBoardState = NailBoardStates.MOVING_TO_NAIL;



            NailComponent l_CurrentNail = m_NailComponents[m_CurrentNailIndex];
            NailComponent l_NextNai = m_NailComponents[++m_CurrentNailIndex];
            m_FromPosition = transform.position;
            m_Destination = new Vector3(m_FromPosition.x + (l_CurrentNail.transform.position.x - l_NextNai.transform.position.x), m_FromPosition.y, m_FromPosition.z);
        }


        public void DespawnBoard(Vector3 i_Position)
        {
            m_FromPosition = transform.position;
            m_Destination = i_Position;
            m_CurrentNailBoardState = NailBoardStates.DESPAWNING;
        }


        public void SpawnBoard(Vector3 i_NailPosition)
        {
            NailComponent l_CurrentNail = m_NailComponents[0];
            m_Destination = new Vector3(i_NailPosition.x - (l_CurrentNail.transform.position.x - transform.position.x), i_NailPosition.y, i_NailPosition.z);
            m_CurrentNailBoardState = NailBoardStates.SPAWNING;
        }
    }
}
