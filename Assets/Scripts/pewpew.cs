using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class pewpew : MonoBehaviour
{
    public GameObject bullet;
    public GameObject barrel;
    [SerializeField] int shotsfired = 0;
    [SerializeField] float counttime;
    [SerializeField] float cooldown = 1;
    public float speed = 10f;
    public TextMeshPro reload;
    public AudioSource audioSource;
    public AudioClip shot;
    public AudioClip ping;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        reload = GameObject.Find("bullettext").GetComponent<TextMeshPro>();
        reload.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (shotsfired < 8)
        {
            reload.text = "AMMO " + (8 - shotsfired).ToString() + "/8";
        }
        if (cooldown < 0.75 && shotsfired < 8)
        {
            cooldown += Time.deltaTime;
        }

        if (cooldown < 3 && shotsfired >= 8)
        {
            Debug.Log("RELOAD");
            reload.text = "RELOADING ...";
            cooldown += Time.deltaTime;
        }
        if (cooldown >= 3)
        {
            shotsfired = 0;
        }
        // if (Input.GetMouseButtonDown(0))
        // {
        //     pew();
        // }

    }

    public void pew()
    {
        if (shotsfired < 8)
        {
            if (cooldown >= 0.75)
            {
                GameObject thebullet = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
                thebullet.GetComponent<Rigidbody>().velocity = speed * barrel.transform.forward;
                Destroy(thebullet, 5);
                cooldown = 0;
                audioSource.clip = shot;
                audioSource.Play();
                if (shotsfired == 7)
                {
                    audioSource.clip = ping;
                    audioSource.Play();
                }
                shotsfired++;
            }
        }
        //Vector3 bulletspawnpos = barrel.transform.TransformDirection(new Vector3(0, 0, 1));
        //GameObject thebullet = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
        //thebullet.GetComponent<Rigidbody>().velocity = speed * barrel.transform.forward;
        //Destroy(thebullet,5);
    }
}
