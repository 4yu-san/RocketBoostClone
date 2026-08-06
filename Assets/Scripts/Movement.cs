using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;
    
    Rigidbody rb;
    AudioSource audioSource;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);
            if(!mainEngineParticle.isPlaying)
                mainEngineParticle.Play();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void ProcessRotation(){
        float rotaionInput = rotation.ReadValue<float>();
        //Debug.Log(rotaionInput);
        if(rotaionInput < 0){
            ApplyRotation(rotationStrength);
            if(!rightThrusterParticle.isPlaying){
                leftThrusterParticle.Stop();
                rightThrusterParticle.Play();
            }
        }
        else if(rotaionInput > 0){
            ApplyRotation(-rotationStrength);
            if(!leftThrusterParticle.isPlaying){
                rightThrusterParticle.Stop();
                leftThrusterParticle.Play();
            }
        }
        else{
            leftThrusterParticle.Stop();
            rightThrusterParticle.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
