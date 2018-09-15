using Assets.Scripts.Ability;
using Assets.Scripts.Contracts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IRotator
{
    #region Fields
    private CameraController cameraController;
    private CharacterController characterController;
    private Abilities availableAbilities;
    private List<Ability> abilities;
    private float coolDown = 0.0f;
    #endregion

    #region Public properties
    public float moveSpeed = 10f;


    #endregion

    // Use this for initialization
    void Start()
    {
        this.cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        this.characterController = GetComponent<CharacterController>();
        this.availableAbilities = GetComponent<Abilities>();
        this.abilities = this.availableAbilities.abilities;
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
        Vector3 sideDirection = this.transform.right * horizontalMovement * moveSpeed;
        Vector3 forwardDirection = this.transform.forward * verticalMovement * moveSpeed;
        this.characterController.SimpleMove(sideDirection + forwardDirection);
          

        if (Input.GetKeyDown("space") && Input.GetKey("d"))
        {
            DodgeAbility foundAbility = abilities.Find(item => item.GetType() == typeof(DodgeAbility)) as DodgeAbility;

            if (foundAbility != null && Time.time > coolDown)
            {     
                foundAbility.Dodge(this.characterController, DodgeDirection.Right);
                coolDown = Time.time + foundAbility.Cooldown;
            }
        }
        else if (Input.GetKeyDown("space") && Input.GetKey("a"))
        {
            DodgeAbility foundAbility = abilities.Find(item => item.GetType() == typeof(DodgeAbility)) as DodgeAbility;

            if (foundAbility != null && Time.time > coolDown)
            {
                foundAbility.Dodge(this.characterController, DodgeDirection.Left);
                coolDown = Time.time + foundAbility.Cooldown;
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
