using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeleScript : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Look at the player
        transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }
}
