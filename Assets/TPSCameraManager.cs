using UnityEngine;
using System.Collections;

public class TPSCameraManager : MonoBehaviour {

    #region Public And Protected Members
    public Transform m_player;
    
    public Vector3 m_startOffSetPlayer;
    public float angle;
    public LayerMask m_collisionLayer;
    public float m_rotationSpeed;

    #endregion

    #region Main Methods
    // Use this for initialization
    void Start()
    {
        m_transform = GetComponent<Transform>();
        //m_transform.position = m_player.position + m_startOffSetPlayer;
        
    }

    // Update is called once per frame
    void Update()
    {

        ManageMouseInput();




    }
    void ManageMouseInput()
    {
        m_transform.position = m_player.position + m_startOffSetPlayer + m_rotationVector;
        m_transform.LookAt( m_player );

        //angle += 10*Time.deltaTime;

        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        float rotY = Input.GetAxis("Mouse X");
        float rotX = Input.GetAxis("Mouse Y");

        if( Physics.Raycast( ray, out hit, 100 ) )
        {
            lookPosition = hit.point;
        }

        m_rotationAngle += rotY * m_rotationSpeed;//m_rotationAngle-Vector3.Angle(m_player.position,lookPosition);
        m_flipAngle -= rotX * m_rotationSpeed;
        m_transform.RotateAround( m_player.position, Vector3.up, m_rotationAngle );
        m_transform.RotateAround( m_player.position, m_transform.right, m_flipAngle );
    }
    #endregion

    #region Private Members

    private Transform m_transform;
    private Vector3 m_rotationVector;
    private Vector3 lookPosition;
    public float m_rotationAngle;
    public float m_flipAngle;
    #endregion
}
