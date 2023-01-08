using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMarker : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    void FixedUpdate()
    {
        this.transform.position = new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z);
        this.transform.forward = Player.transform.forward;
    }
}
