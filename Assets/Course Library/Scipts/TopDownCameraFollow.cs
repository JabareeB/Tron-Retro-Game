using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float height = 10f; // How high the camera is above the player
    public float distance = 10f; // Distance behind the player
    public float angle = 45f; // Angle of looking down

    private Vector3 offset; 

    private void Start()
    {
        
        offset = Quaternion.Euler(angle, 0, 0) * new Vector3(0, height, -distance);
    }

    private void LateUpdate()
    {
        
        if (player == null) return;

        
        transform.position = player.position + offset;

        
        transform.LookAt(player.position);
    }
}
