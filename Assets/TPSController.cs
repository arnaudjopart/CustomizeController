using UnityEngine;
using System.Collections;

public class TPSController : MonoBehaviour {
    #region Public And Protected Members
    public float m_moveSpeed;

    public TPSCameraManager cam;
    public Transform m_test;
    
    #endregion

    // Use this for initialization
    void Start () {
        m_transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        ManageKeyboardInput();        

    }
    void ManageKeyboardInput()
    {
        float moveSide = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");
        if( Mathf.Abs( moveForward ) > 0.1f || Mathf.Abs( moveSide ) >0.1f)
        {
            m_transform.rotation = Quaternion.Euler( 0, cam.m_rotationAngle, 0 );
        }
        float speedPerFrame = m_moveSpeed*Time.deltaTime;

        m_transform.Translate( m_transform.InverseTransformDirection( m_transform.forward ) * moveForward * speedPerFrame );
        m_transform.Translate( m_transform.InverseTransformDirection( m_transform.right ) * moveSide * speedPerFrame );

    }

    #region Private Members

    Transform m_transform; 
    #endregion
}
