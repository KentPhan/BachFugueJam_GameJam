using UnityEngine;

namespace Assets.Source
{
    public class NailComponent : MonoBehaviour
    {
        [SerializeField] private float m_MaxMovement = 100.0f;
        [SerializeField] private float m_VelocityCap = 15.0f;
        [SerializeField] private float m_MovementCap = 10.0f;
        [SerializeField] private float m_MinVelocity = 5.0f;



        private float m_CurrentMovement;

        // Start is called before the first frame update
        void Start()
        {
            m_CurrentMovement = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D i_Collision)
        {
            float l_RelativeVelocity = i_Collision.relativeVelocity.magnitude;
            Debug.Log("Collided Nail" + l_RelativeVelocity);

            if (m_MinVelocity > l_RelativeVelocity)
                return;

            float l_Movement = Mathf.Lerp(0.0f, m_MovementCap, l_RelativeVelocity / m_VelocityCap);



            Vector3 l_Position = transform.position;
            transform.position = new Vector3(l_Position.x, l_Position.y - l_Movement, l_Position.z);

            NailManager.Instance.PlayRandomHammeredSound();
        }
    }
}
