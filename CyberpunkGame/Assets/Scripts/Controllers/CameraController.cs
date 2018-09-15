using Assets.Scripts.Contracts;
using UnityEngine;

public class CameraController : MonoBehaviour, IRotator
{

    public float mouseSensitivity = 2f;
    Transform attachPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        attachPoint = GameObject.Find("CameraPosition").transform;
        this.transform.position = attachPoint.position;

        Vector3 targetRotation = GetMouseCoordinatesAndReturnRotation();
        this.Rotate(targetRotation);
    }

    public void Rotate(Vector3 targetLocation)
    {
        this.transform.rotation = Quaternion.Euler(targetLocation);
    }
     
    public Vector3 GetMouseCoordinatesAndReturnRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationX = mouseX * this.mouseSensitivity;
        float rotationY = mouseY * this.mouseSensitivity;
        Vector3 targetRotation = this.transform.eulerAngles;
        targetRotation.x -= rotationY;
        targetRotation.y += rotationX;

        return targetRotation;
    }
}
