using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerComponent : MonoBehaviour
{

    [SerializeField] private float m_BaseForce = 1.0f;
    [SerializeField] private float m_TimeLagOnMeasuredPosition = 0.5f;

    private Vector3 m_PreviousMeasuredPosition;
    private Vector3 m_CurrentMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 l_CurrentPosition = transform.position;

        m_CurrentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // Update Position of Hammer
        transform.position = new Vector3(l_CurrentPosition.x, m_CurrentMousePosition.y, l_CurrentPosition.z);
    }
}
