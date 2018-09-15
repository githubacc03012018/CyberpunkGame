using Assets.Scripts.Ability;
using Assets.Scripts.Contracts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IRotator
{
    #region Fields
    private new Rigidbody rigidbody;
    public List<Ability> abilities;
    private CameraController cameraController;
    #endregion

    #region Public properties
    public float moveSpeed = 10f;

    #endregion
    
    // Use this for initialization
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        this.abilities = new List<Ability>();
        DodgeAbility ab = new DodgeAbility();
        this.abilities.Add(ab);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Vector3 targetRotation = this.cameraController.GetMouseCoordinatesAndReturnRotation(); 
        Rotate(targetRotation);
    }

    private void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 sideDirection = this.transform.right * horizontalMovement * moveSpeed * Time.deltaTime;
        Vector3 forwardDirection = this.transform.forward * verticalMovement * moveSpeed * Time.deltaTime;
        this.rigidbody.position += sideDirection + forwardDirection;

        if (Input.GetKey("space") && Input.GetKey("d"))
        {
            foreach (Ability ability in abilities)
            {
                DodgeAbility dodgeAbility = ability as DodgeAbility;
                if(dodgeAbility != null)
                {
                    
                    dodgeAbility.Dodge(this.transform, DodgeDirection.Right);
                }
            }
        }
        else if(Input.GetKey("space") && Input.GetKey("a"))
        {
            foreach (Ability ability in abilities)
            {
                DodgeAbility dodgeAbility = ability as DodgeAbility;
                if (dodgeAbility != null)
                {
                    
                    dodgeAbility.Dodge(this.transform, DodgeDirection.Left);
                }
            }
        }
    }

    public void Rotate(Vector3 targetRotation)
    {
        this.transform.rotation = Quaternion.Euler(targetRotation);
    }

    protected void LateUpdate()
    {
        this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0); //constrain rotation
    }
}
