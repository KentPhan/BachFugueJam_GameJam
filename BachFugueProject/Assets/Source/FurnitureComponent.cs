using UnityEngine;

namespace Assets.Source
{
    public class FurnitureComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform m_FinalPosition;
        [SerializeField]
        private int m_RequiredBoardCount = 2;
        [SerializeField]
        private float m_TimeToMove = 3.0f;

        private Vector3 m_OriginalPosition;
        private bool m_Move;
        private bool m_Moved;
        private float m_CurrentTimeToMove;

        private void Awake()
        {
            m_Move = false;
            m_Moved = false;
            m_CurrentTimeToMove = 0.0f;
            m_OriginalPosition = transform.position;
        }

        private void Update()
        {
            if (m_Move)
            {
                transform.position = Vector3.Lerp(m_OriginalPosition, m_FinalPosition.position, m_CurrentTimeToMove / m_TimeToMove);

                if (m_CurrentTimeToMove >= m_TimeToMove)
                {
                    m_Move = false;
                    return;
                }

                m_CurrentTimeToMove += Time.deltaTime;
            }
        }


        public int GetRequiredCount()
        {
            return m_RequiredBoardCount;
        }

        public void Move()
        {
            if (m_Moved)
                return;

            m_Move = true;
            m_Moved = true;
        }

        public bool AlreadyMoved()
        {
            return m_Moved;
        }
    }
}
