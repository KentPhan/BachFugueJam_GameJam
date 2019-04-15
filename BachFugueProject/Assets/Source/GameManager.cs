using UnityEngine;

namespace Assets.Source
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private AudioSource m_AudioSource;

        [SerializeField]
        private NailBoardComponent m_CurrentBoard;
        public void Awake()
        {
            Instance = this;
            m_AudioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveToNextNail()
        {
            m_CurrentBoard.MoveToNextNail();
            Debug.Log("Moving");
        }
    }
}
