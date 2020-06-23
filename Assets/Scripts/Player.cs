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

    [SerializeField]
    private bool m_grounded = false;
    private Rigidbody2D m_rigidbody = new Rigidbody2D();


    // Utilisez cette fonction pour l'initialisation
    void Awake()
    {
        if (TryGetComponent(out m_rigidbody) == false)
            Debug.LogError("Unable to find any rigidbody attached to the player GO");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        /*if (isLocalPlayer == false)
            return;*/

        if (col.gameObject.tag == "Ground") 
        {
            m_grounded = true;
        }
    }
   
    private void OnCollisionExit2D(Collision2D col)
    {


        if (col.gameObject.tag == "Ground") 
        {
            m_grounded = false;
        }
    }



    // Update est appelee une fois par frame
    void Update()
    {

        /*if (TryGetComponent(out PlayerNetwork playerNetwork))
            print("id of " + gameObject.name + " is " + playerNetwork.IdPlayer);*/
        if ((Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && m_grounded)
        {
            m_jump = true;
        }
        

    }

    void FixedUpdate()
    {


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
