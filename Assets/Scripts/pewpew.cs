using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pewpew : MonoBehaviour
{
    public GameObject bullet;
    public GameObject barrel;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pew()
    {
        //Vector3 bulletspawnpos = barrel.transform.TransformDirection(new Vector3(0, 0, 1));
        GameObject thebullet = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
        thebullet.GetComponent<Rigidbody>().velocity = speed * barrel.transform.forward;
        Destroy(thebullet,5);
    }
}
