using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_RotationSpeed;
    
    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] Transform m_BallSpawnPos;
    [SerializeField] float m_BallInitSpeed;

    [SerializeField] float m_ShootingPeriod;
    float m_TimeNextShoot;
    
    [SerializeField] float m_BallLifeTime;
    
    Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_TimeNextShoot = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    GameObject ShootBall()
    {
        GameObject newBallGO = Instantiate(m_BallPrefab);
        newBallGO.transform.position = m_BallSpawnPos.position;
        newBallGO.GetComponent<Rigidbody>().velocity = m_BallSpawnPos.forward * m_BallInitSpeed;
        return newBallGO;
    }
    
    // Update is called once per frame
    void Update()
    {
        /*float hInput, vInput;
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 vect = new Vector3(0, 0, 1) * (vInput * (m_TranslationSpeed * Time.deltaTime));
        transform.Translate(vect ,Space.Self);
        
        float deltaAngle = hInput * (m_RotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, deltaAngle, Space.Self);*/
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying) return; // HACK !! je n'utilise pas les évènements pour changer l'état
                                                     // du jeu car j'ai la flemme
        // Comportement dynamiques
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        
        #region POSITIONAL
        // pas le droit de changer la position de l'objet depuis le transform
        // Mode positionnel (téléportation)
        // translation
        /*Vector3 vect = transform.forward * (vInput * (m_TranslationSpeed * Time.fixedDeltaTime));
        m_Rigidbody.MovePosition(transform.position + vect);
        
        // rotation
        float deltaAngle = hInput * (m_RotationSpeed * Time.fixedDeltaTime);
        Quaternion qRot = Quaternion.AngleAxis(deltaAngle, transform.up);
        
        Quaternion qUprightRot = Quaternion.FromToRotation(transform.up, Vector3.up);
        Quaternion qUprightOrient = Quaternion.Slerp(transform.rotation, qUprightRot*transform.rotation, Time.fixedDeltaTime*4);
        
        Quaternion qNewOrient = qRot * qUprightOrient;
        m_Rigidbody.MoveRotation(qNewOrient);
        
        m_Rigidbody.AddForce(-m_RigidBody.velocity, ForceMode.VelocityChange);
        m_Rigidbody.AddTorque(-m_RigidBody.angularVelocity, ForceMode.VelocityChange);
        */
        #endregion
        
        // Mode VELOCITY
        Vector3 targetVelocity = transform.forward * (vInput * m_TranslationSpeed);
        Vector3 deltaVelocity = targetVelocity - m_Rigidbody.velocity;
        m_Rigidbody.AddForce(deltaVelocity, ForceMode.VelocityChange);
        
        Vector3 targetAngularVelocity = transform.up * (hInput * m_RotationSpeed * Mathf.Deg2Rad);
        Vector3 deltaAngularVel = targetAngularVelocity - m_Rigidbody.angularVelocity;
        m_Rigidbody.AddTorque(deltaAngularVel, ForceMode.VelocityChange);
        
        Quaternion qUprightRot = Quaternion.FromToRotation(transform.up, Vector3.up);
        Quaternion qUprightOrient =
            Quaternion.Slerp(transform.rotation, qUprightRot * transform.rotation, Time.fixedDeltaTime * 8);
        m_Rigidbody.MoveRotation(qUprightOrient);
        
        //
        
        bool isFiring = Input.GetButton("Fire1");
        if (isFiring && Time.time > m_TimeNextShoot)
        {
            Destroy(ShootBall(), m_BallLifeTime);
            m_TimeNextShoot = Time.time + m_ShootingPeriod;
        }
        
    }
}
