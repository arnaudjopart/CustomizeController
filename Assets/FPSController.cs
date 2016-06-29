using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {

    // Use this for initialization
    #region Public And Protected Members
    public float m_moveSpeed;
    public float m_rotationSpeed;
    public float m_velocity;
    public Transform m_laserStartPosition;
    public LayerMask m_laserCollideWith;

    public float speedH = 20.0f;
    public float speedV = 2.0f;

    
    #endregion

    #region Main Methods
    void Start()
    {
        Cursor.visible = false;
        m_transform = GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody>();
        m_laserLR = m_laserStartPosition.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageKeyboardInput();
        ManageMouseInput();
        ManageLaser();
    }

    private void ManageKeyboardInput()
    {
        float moveSide = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        float speedPerFrame = m_moveSpeed*Time.deltaTime;
        float rotationSpeedPerFrame = m_rotationSpeed*Time.deltaTime;

        
        //m_rb.velocity = m_transform.forward * m_velocity*moveForward;
        m_transform.Translate( m_transform.InverseTransformDirection(m_transform.forward) * moveForward * speedPerFrame );
        m_transform.Translate( m_transform.InverseTransformDirection( m_transform.right ) * moveSide * speedPerFrame );
        
        //m_transform.Rotate( 0, rotationOnYAxis * rotationSpeedPerFrame,0 );
    }

    private void ManageMouseInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        m_yaw =  Mathf.Abs(Input.GetAxis( "Mouse X" ))>0.05f? speedH * Input.GetAxis( "Mouse X" ):0;
        m_pitch -= Mathf.Abs(Input.GetAxis( "Mouse Y" ))>0.05f? speedV * Input.GetAxis( "Mouse Y" ):0;
        m_transform.Rotate( 0, m_yaw* speedH, 0 );
        Camera.main.transform.localEulerAngles = new Vector3( m_pitch, 0, 0.0f );

    }
    private void ManageLaser()
    {
        Ray ray = new Ray(m_laserStartPosition.position,m_laserStartPosition.forward);
        Debug.DrawLine( m_laserStartPosition.position, m_laserStartPosition.position + m_laserStartPosition.forward * 10 );
        RaycastHit hit;
        float lengthOfLaser;
        if( Physics.Raycast( ray, out hit, 100, m_laserCollideWith  ) )
        {
            lengthOfLaser = hit.distance;
        }
        else
        {
            lengthOfLaser =  20;
        }
        //m_laserLR.material.color = Color.red; 
        m_laserLR.SetPosition( 0, m_laserStartPosition.position );
        m_laserLR.SetPosition( 1, m_laserStartPosition.position+ m_laserStartPosition.forward* lengthOfLaser);

    }
    #region Utils

    #endregion
    #endregion
    #region Private Members
    private Transform m_transform;
    private Rigidbody m_rb;
    private LineRenderer m_laserLR;

    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;
    #endregion


}
