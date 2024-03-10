using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HertaScript : MonoBehaviour
{
    public Animator anim;
    public GameObject xrCameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        xrCameraOffset = GameObject.Find("Main Camera");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetTrigger("Running");
            anim.ResetTrigger("Idling");
            anim.ResetTrigger("StrafeLeft");
            anim.ResetTrigger("StrafeRight");
            anim.ResetTrigger("Backwards");
            anim.ResetTrigger("Jumping");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetTrigger("StrafeLeft");
            anim.ResetTrigger("Idling");
            anim.ResetTrigger("Running");
            anim.ResetTrigger("StrafeRight");
            anim.ResetTrigger("Backwards");
            anim.ResetTrigger("Jumping");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetTrigger("StrafeRight");
            anim.ResetTrigger("Idling");
            anim.ResetTrigger("Running");
            anim.ResetTrigger("StrafeLeft");
            anim.ResetTrigger("Backwards");
            anim.ResetTrigger("Jumping");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetTrigger("Backwards");
            anim.ResetTrigger("Idling");
            anim.ResetTrigger("Running");
            anim.ResetTrigger("StrafeLeft");
            anim.ResetTrigger("StrafeRight");
            anim.ResetTrigger("Jumping");
        }
        // else if (Input.GetKey(KeyCode.Space))
        // {
        //     anim.SetTrigger("Jumping");
        //     anim.ResetTrigger("Idling");
        //     anim.ResetTrigger("Running");
        //     anim.ResetTrigger("StrafeLeft");
        //     anim.ResetTrigger("StrafeRight");
        //     anim.ResetTrigger("Backwards");
        // }
        else
        {
            anim.SetTrigger("Idling");
            anim.ResetTrigger("Running");
            anim.ResetTrigger("StrafeLeft");
            anim.ResetTrigger("StrafeRight");
            anim.ResetTrigger("Backwards");
            anim.ResetTrigger("Jumping");
        }
    }

    // Check the position and move this object to the camera position
    // LateUpdate is called after Update
    private void LateUpdate()
    {
        transform.position = new Vector3(xrCameraOffset.transform.position.x, transform.position.y, xrCameraOffset.transform.position.z + 0.0f);
        transform.rotation = new Quaternion(0, -xrCameraOffset.transform.rotation.y, 0, -xrCameraOffset.transform.rotation.w);
    }
    // private void LateUpdate()
    // {
    //     // Calculate the desired position based on the camera's position and a fixed offset distance
    //     Vector3 desiredPosition = xrCameraOffset.transform.position - (xrCameraOffset.transform.forward * 1.25f);

    //     // Calculate the direction from the model to the desired position
    //     Vector3 direction = desiredPosition - transform.position;

    //     // Normalize the direction vector to ensure consistent distance
    //     direction.Normalize();

    //     // Move the model to the desired position with a fixed offset distance
    //     Vector3 newDesiredPosition = desiredPosition - direction * 1.25f;
    //     transform.position = new Vector3(newDesiredPosition.x, transform.position.y, newDesiredPosition.z);
        
    //     // // Get the y rotation of the camera
    //     float yRotation = xrCameraOffset.transform.rotation.eulerAngles.y;
    //     // Apply the y rotation to the character model
    //     transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    // }


}
