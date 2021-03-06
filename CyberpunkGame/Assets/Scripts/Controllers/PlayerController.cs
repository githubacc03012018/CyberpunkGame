﻿using Assets.Scripts.Ability;
using Assets.Scripts.Contracts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IRotator
{
    #region Fields
    private CameraController cameraController;
    private float nextFire = 0.0f;
    private Abilities availableAbilities;
    private List<Ability> abilities;
    #endregion

    #region Public properties
    public float moveSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;
    public float fireRate;
    public new Camera camera;
    #endregion

    // Use this for initialization
    void Start()
    {
        this.cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        this.availableAbilities = GetComponent<Abilities>();
        this.abilities = this.availableAbilities.abilities;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Vector3 targetRotation = this.cameraController.GetMouseCoordinatesAndReturnRotation();
        Rotate(targetRotation);
        //Dodge();
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFire)
        {
            if(Physics.Raycast(this.bulletSpawnPosition.position, this.bulletSpawnPosition.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
            {
                Debug.Log("Hit!");
                Debug.DrawRay(this.bulletSpawnPosition.position, this.bulletSpawnPosition.TransformDirection(Vector3.forward), Color.red);
            }

            nextFire = Time.time + fireRate;
        }

    }
 
    private void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 sideDirection = this.transform.right * horizontalMovement * moveSpeed;
        Vector3 forwardDirection = this.transform.forward * verticalMovement * moveSpeed;

        this.transform.position += (sideDirection + forwardDirection) * Time.deltaTime;
    }

    public void Rotate(Vector3 targetRotation)
    {
        this.transform.rotation = Quaternion.Euler(targetRotation);
    }

    protected void LateUpdate()
    {
        this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0); //constrain rotation
     
    }

    private void Dodge()
    {
        if (Input.GetKey("space") && (Input.GetKey("d") || Input.GetKey("a")))
        {
            DodgeAbility foundAbility = abilities.Find(item => item.GetType() == typeof(DodgeAbility)) as DodgeAbility;
            if (foundAbility != null)
            {
                if (Input.GetKey("d"))
                {
                    foundAbility.Dodge(this.transform, DodgeDirection.Right);
                }
                else if (Input.GetKey("a"))
                {
                    foundAbility.Dodge(this.transform, DodgeDirection.Left);

                }

            }
        }
    }
}
