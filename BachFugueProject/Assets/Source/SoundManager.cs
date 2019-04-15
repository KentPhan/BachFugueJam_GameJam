using UnityEngine;

namespace Assets.Source
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] m_NailHammeringSounds;

        public static SoundManager Instance;
        private AudioSource m_AudioSource;

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

        public void PlayRandomHammeredSound()
        {
            m_AudioSource.clip = m_NailHammeringSounds[Random.Range(0, m_NailHammeringSounds.Length)];
            m_AudioSource.Play();
        }

    }
}
