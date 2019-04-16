using UnityEngine;

namespace Assets.Source
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private NailBoardComponent m_BoardPrefab;
        [SerializeField] private Transform m_BoardSpawnPosition;
        [SerializeField] private Transform m_BoardDeSpawnPosition;
        [SerializeField] private Transform m_BoardNailPosition;


        public static GameManager Instance;
        private FurnitureComponent[] m_Furniture;
        private AudioSource m_AudioSource;


        private NailBoardComponent m_CurrentBoard;


        private int m_FinishedBoardCount;

        public void Awake()
        {
            Instance = this;
            m_FinishedBoardCount = 0;
            m_AudioSource = GetComponent<AudioSource>();
            m_CurrentBoard = FindObjectOfType<NailBoardComponent>();
        }

        // Start is called before the first frame update
        void Start()
        {
            SpawnBoard();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveToNextNail()
        {
            m_CurrentBoard.MoveToNextNail();
        }

        public void MoveToNextBoard()
        {
            m_FinishedBoardCount++;

            // Remove Old Board
            m_CurrentBoard.DespawnBoard(m_BoardDeSpawnPosition.position);

            // Spawn New Board
            SpawnBoard();

            // Spawn Furniture
        }

        private void SpawnBoard()
        {
            m_CurrentBoard = Instantiate(m_BoardPrefab, m_BoardSpawnPosition.position, Quaternion.identity);
            m_CurrentBoard.SpawnBoard(m_BoardNailPosition.position);
        }
    }
}
