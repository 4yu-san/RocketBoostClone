using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 1000f;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust(){
        if(thrust.IsPressed())
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
    }

    private void ProcessRotation(){
        float rotaionInput = rotation.ReadValue<float>();
        //Debug.Log(rotaionInput);
        if(rotaionInput < 0)
            ApplyRotation(rotationStrength);
        else if(rotaionInput > 0)
            ApplyRotation(-rotationStrength);
    }

    private void ApplyRotation(float rotationThisFrame){
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
    }
}
