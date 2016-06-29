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
        
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageMouseInput();
    }
    void ManageMouseInput()
    {
        
        m_transform.position = m_player.position + m_startOffSetPlayer;
        m_transform.LookAt( m_player );   

        float rotY = Mathf.Abs(Input.GetAxis("Mouse X"))>.01f?Input.GetAxis("Mouse X"):0;
        float rotX = Mathf.Abs(Input.GetAxis("Mouse Y"))>.05f?Input.GetAxis("Mouse Y"):0; ;

        m_rotationAngle += rotY * m_rotationSpeed;
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
