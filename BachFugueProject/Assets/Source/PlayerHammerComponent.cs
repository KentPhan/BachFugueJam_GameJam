using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerComponent : MonoBehaviour
{
    [SerializeField] private float m_Acceleration = 1.0f;

    private Vector3 m_CurrentMousePosition;
    private Rigidbody2D m_RigidBody;
    private bool m_ApplingForceUp;

    public void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_ApplingForceUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {



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


    void OnCollisionEnter(Collision i_Collision)
    {
        //Debug.Log("Collided");
    }
}
