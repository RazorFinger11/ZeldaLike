using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float height, heightLook, distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // idk 
        transform.LookAt(player.transform.position + Vector3.up * heightLook);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.up * height + player.transform.forward * distance, Time.smoothDeltaTime);
    }
}
