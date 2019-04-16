using System;
using UnityEngine;

namespace Assets.Source
{
    [Serializable]
    public class FurnitureComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_SpirteToSpawn;
        [SerializeField]
        private Vector3 m_FinalPosition;
        [SerializeField]
        private int m_RequiredBoardCount;
        [SerializeField]
        private float m_TimeToMove;

        private void Awake()
        {

        }

        private void Update()
        {

        }
    }
}
