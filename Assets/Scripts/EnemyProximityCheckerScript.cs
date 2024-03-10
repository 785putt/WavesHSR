using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximityCheckerScript : MonoBehaviour
{
    public float distanceToPlayer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Professor Herta");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }
}
