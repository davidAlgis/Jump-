using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    private bool m_jump = false;
    public float m_moveForce = 100f;
    public float m_maxSpeed = 5f;
    public float m_jumpForce = 100f;
    private bool m_stopMove = false;

    [SerializeField]
    private bool m_grounded = false;
    [SerializeField]
    private Transform m_groundedTransform = default;
    [SerializeField]
    private LayerMask m_whatIsGrounded = default;
    [SerializeField]
    private float m_radiusCollision = 0.3f;

    private Rigidbody2D m_rigidbody = new Rigidbody2D();

    public Transform GroundedTransform { get => m_groundedTransform; set => m_groundedTransform = value; }
    public bool StopMove { get => m_stopMove; set => m_stopMove = value; }

    void Awake()
    {
        if (TryGetComponent(out m_rigidbody) == false)
            Debug.LogError("Unable to find any rigidbody attached to the player GO");
    }



    void Update()
    {
        if ((Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && m_grounded)
            m_jump = true;
    }

    void FixedUpdate()
    {
        if (m_stopMove)
            return;

        m_grounded = Physics2D.OverlapCircle(m_groundedTransform.position, m_radiusCollision, m_whatIsGrounded);


        //apply a constant forces. 
        if (m_rigidbody.velocity.x < m_maxSpeed)
            m_rigidbody.AddForce(Vector2.right * m_moveForce);

        if (Mathf.Abs(m_rigidbody.velocity.x) > m_maxSpeed)
            m_rigidbody.velocity = new Vector2(Mathf.Sign(m_rigidbody.velocity.x) * m_maxSpeed, m_rigidbody.velocity.y);
        

        if (m_jump)
        {
            m_rigidbody.AddForce(new Vector2(0f, m_jumpForce));
            m_jump = false;
        }
    }
}
