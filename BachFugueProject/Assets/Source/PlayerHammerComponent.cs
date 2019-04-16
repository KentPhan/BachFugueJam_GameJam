using UnityEngine;

public class PlayerHammerComponent : MonoBehaviour
{
    [SerializeField] private float m_Acceleration = 1.0f;
    [SerializeField] private float m_HorizontalCorrectionSpeed = 2.0f;
    [SerializeField] private float m_HammerPositionBuffer = 0.5f;



    private Vector3 m_CurrentMousePosition;
    private Rigidbody2D m_RigidBody;
    private bool m_ApplingForceUp;

    private Vector3 m_OriginalHorizontalPosition;
    private bool m_CollidingWithNail;

    public void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_ApplingForceUp = false;
        m_OriginalHorizontalPosition = transform.position;
        m_CollidingWithNail = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (transform.position.x != m_OriginalHorizontalPosition.x)
        if (!m_CollidingWithNail)
        {
            // Apply velocity towards center if needed
            if ((m_RigidBody.position.x > (m_OriginalHorizontalPosition.x + m_HammerPositionBuffer)) ||
                (m_RigidBody.position.x < (m_OriginalHorizontalPosition.x - m_HammerPositionBuffer))
                )
            {

                float X_Velocity = (m_OriginalHorizontalPosition.x - m_RigidBody.position.x) > 0.0f ? 1.0f : -1.0f;
                X_Velocity *= m_HorizontalCorrectionSpeed;
                m_RigidBody.velocity = new Vector2(X_Velocity, m_RigidBody.velocity.y);
            }
            else
            {
                m_RigidBody.velocity = new Vector2(0.0f, m_RigidBody.velocity.y);
            }



            //if (m_RigidBody.position.x > m_OriginalHorizontalPosition.x)
            //{
            //    m_RigidBody.MovePosition(m_RigidBody.transform.position + Vector3.left * Time.deltaTime * m_HorizontalCorrectionSpeed);
            //    if (m_RigidBody.position.x < m_OriginalHorizontalPosition.x)
            //        m_RigidBody.position = new Vector3(m_OriginalHorizontalPosition.x, m_RigidBody.position.y);
            //}
            //if (m_RigidBody.position.x < m_OriginalHorizontalPosition.x)
            //{
            //    m_RigidBody.MovePosition(m_RigidBody.transform.position + Vector3.right * Time.deltaTime * m_HorizontalCorrectionSpeed);
            //    if (m_RigidBody.position.x > m_OriginalHorizontalPosition.x)
            //        m_RigidBody.position = new Vector3(m_OriginalHorizontalPosition.x, m_RigidBody.position.y);
            //}
            //else
            //{

            //}
        }


        //transform.position = new Vector3(l_CurrentPosition.x, m_CurrentMousePosition.y, l_CurrentPosition.z);
    }

    private void FixedUpdate()
    {
        Vector3 l_CurrentPosition = transform.position;


        if (Input.GetMouseButtonDown(0))
        {

        }


        m_CurrentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Compute New  Direction
        Vector3 l_Direction = m_CurrentMousePosition - transform.position;
        l_Direction = new Vector3(0.0f, l_Direction.y, 0.0f).normalized;


        // Apply Force Up
        if (l_Direction.y > 0.0f)
        {
            if (!m_ApplingForceUp)
            {
                m_RigidBody.velocity = Vector3.zero;
            }

            m_ApplingForceUp = true;
        }

        // Apply Force Down
        else
        {

            if (m_ApplingForceUp)
            {
                m_RigidBody.velocity = Vector3.zero;
            }


            m_ApplingForceUp = false;
        }


        // Update Position of Hammer
        m_RigidBody.AddForce(l_Direction * m_Acceleration, ForceMode2D.Force);
    }


    void OnCollisionEnter2D(Collision2D i_Collision)
    {
        m_CollidingWithNail = true;
        // If Colliding don't force horizontal movement
    }
    void OnCollisionExit2D(Collision2D i_Collision)
    {
        m_CollidingWithNail = false;
        // If Colliding don't force horizontal movement
    }
}
