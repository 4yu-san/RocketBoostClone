using UnityEngine;

public class SkyLimit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Reset the player's position to the starting point
                other.transform.position = new Vector3(0, 1, 0); // Change this to your desired starting position
            }
        }
    }
}
