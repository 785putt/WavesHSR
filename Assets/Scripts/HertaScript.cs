using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HertaScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
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
        else if (Input.GetKey(KeyCode.Space))
        {
            anim.SetTrigger("Jumping");
            anim.ResetTrigger("Idling");
            anim.ResetTrigger("Running");
            anim.ResetTrigger("StrafeLeft");
            anim.ResetTrigger("StrafeRight");
            anim.ResetTrigger("Backwards");
        }
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
}
