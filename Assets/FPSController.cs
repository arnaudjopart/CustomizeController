using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {

    // Use this for initialization
    #region Public And Protected Members
    public float m_moveSpeed;
    public float m_rotationSpeed;
    public float m_velocity;
    public bool m_aimingAssist;
    public Transform m_laserStartPosition;
    public LayerMask m_laserCollideWith;
    public Transform m_testTarget;
    public float speedH = 20.0f;
    public float speedV = 2.0f;
    [Range(.5f,5)]
    public float aimRange;

   

    #endregion


    #region Main Methods

    void Awake()
    {
        Cursor.visible = false;
        m_transform = GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody>();
        m_laserLR = m_laserStartPosition.GetComponent<LineRenderer>();
        m_startRotationLerp = Camera.main.transform.rotation;
        m_endRotationLerp = Camera.main.transform.rotation;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ManageMoveInput();
        ManageMouseInput();
        ManageLaser();
        ManageShoot();
        
    }

    private void ManageMoveInput()
    {
        if( m_isLerping )
        {
            LerpPlayer( m_startRotationLerp, m_endRotationLerp );
        }
        else
        {
            float moveSide = Input.GetAxis("Horizontal");
            float moveForward = Input.GetAxis("Vertical");

            float speedPerFrame = m_moveSpeed*Time.deltaTime;
            float rotationSpeedPerFrame = m_rotationSpeed*Time.deltaTime;


            //m_rb.velocity = m_transform.forward * m_velocity*moveForward;
            m_transform.Translate( m_transform.InverseTransformDirection( m_transform.forward ) * moveForward * speedPerFrame );
            m_transform.Translate( m_transform.InverseTransformDirection( m_transform.right ) * moveSide * speedPerFrame );

            //m_transform.Rotate( 0, rotationOnYAxis * rotationSpeedPerFrame,0 );
        }


    }
    private void ManageShoot()
    {
        if( Input.GetButtonDown( "Fire1" ) )
        {
            print( "Shoot" );
            if( m_aimingAssist )
            {
                
                AssistShoot( m_currentTarget );
            }
        }
    }
    private void ManageMouseInput()
    {
        if( !m_isLerping )
        {
            Vector3 mousePosition = Input.mousePosition;
            m_yaw = Mathf.Abs( Input.GetAxis( "Mouse X" ) ) > 0.05f ? speedH * Input.GetAxis( "Mouse X" ) : 0;
            m_pitch -= Mathf.Abs( Input.GetAxis( "Mouse Y" ) ) > 0.05f ? speedV * Input.GetAxis( "Mouse Y" ) : 0;
            m_transform.Rotate( 0, m_yaw * speedH, 0 );
            Camera.main.transform.localEulerAngles = new Vector3( m_pitch, 0, 0.0f );
        }
              

    }
    private void ManageLaser()
    {
        Ray ray = new Ray(m_laserStartPosition.position,m_laserStartPosition.forward);
        //Debug.DrawLine( m_laserStartPosition.position, m_laserStartPosition.position + m_laserStartPosition.forward * 10 );
        RaycastHit hit;
        float lengthOfLaser;
        if( Physics.SphereCast(ray, .5f,out hit, 20, m_laserCollideWith  ) )
        {
            lengthOfLaser = hit.distance;
            m_currentTarget = hit.collider.transform;
        }
        else
        {
            lengthOfLaser =  20;
            m_currentTarget = null;
        }
        //m_laserLR.material.color = Color.red; 
        m_laserLR.SetPosition( 0, m_laserStartPosition.position );
        m_laserLR.SetPosition( 1, m_laserStartPosition.position+ m_laserStartPosition.forward* lengthOfLaser);

    }
    #endregion

    #region Utils
    private Transform FindSuitableTarget()
    {
        return m_currentTarget;
    }
    private void AssistShoot(Transform _target)
    {
        if( _target )
        {
            Vector3 TargetPositionRelativeToPlayer = new Vector3(_target.position.x, m_transform.position.y,_target.position.z);
            Vector3 direction = TargetPositionRelativeToPlayer - m_transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction,m_transform.up);
            m_startRotationLerp = m_transform.rotation;
            m_endRotationLerp = rotation;
            //m_transform.rotation = rotation;

            Camera.main.transform.LookAt( _target );
            m_yaw = 0;
            m_pitch = Camera.main.transform.localEulerAngles.x;
            m_currentLerptime = 0;
            m_isLerping = true;
        }
        
    }
    private void LerpPlayer(Quaternion _start, Quaternion _stop)
    {
        print( _start );
        if( m_currentLerptime < m_lerpTime )
        {
            m_currentLerptime += Time.deltaTime;
        }
        else
        {
            m_currentLerptime = m_lerpTime;
            m_isLerping = false;
        }
        float perc = m_currentLerptime/m_lerpTime;

        m_transform.rotation = Quaternion.Lerp( _start, _stop, perc );

    }
    #endregion
    
    #region Private Members
    private Transform m_transform;
    private Rigidbody m_rb;
    private LineRenderer m_laserLR;

    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;
    private Transform m_currentTarget;
    private float m_currentLerptime;
    private float m_lerpTime=1f;
    private Quaternion m_startRotationLerp,m_endRotationLerp;
    private bool m_isLerping;
    #endregion


}
