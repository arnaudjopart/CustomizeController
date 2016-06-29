using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {

    // Use this for initialization
    #region Public And Protected Members
    public float m_moveSpeed;
    public float m_rotationSpeed;
    public float m_velocity;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    
    #endregion

    #region Main Methods
    void Start()
    {
        m_transform = GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageKeyboardInput();
        ManageMouseInput();
    }

    private void ManageKeyboardInput()
    {
        float rotationOnYAxis = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        float speedPerFrame = m_moveSpeed*Time.deltaTime;
        float rotationSpeedPerFrame = m_rotationSpeed*Time.deltaTime;

        
        //m_rb.velocity = m_transform.forward * m_velocity*moveForward;
        m_transform.Translate( m_transform.InverseTransformDirection(m_transform.forward) * moveForward * speedPerFrame );
        Debug.DrawLine( m_transform.position, m_transform.position+m_transform.forward * 3 );
        m_transform.Rotate( 0, rotationOnYAxis * rotationSpeedPerFrame,0 );
    }

    private void ManageMouseInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        yaw = speedH * Input.GetAxis( "Mouse X" );
        pitch -= speedV * Input.GetAxis( "Mouse Y" );
        m_transform.Rotate( 0, yaw, 0 );
        Camera.main.transform.localEulerAngles = new Vector3( pitch, 0, 0.0f );

    }
    #region Utils

    #endregion
    #endregion
    #region Private Members
    private Transform m_transform;
    private Rigidbody m_rb;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    #endregion


}
