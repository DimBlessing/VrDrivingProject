using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Speedometer : MonoBehaviour
{
    public Rigidbody car;
    public Transform arrow;
    
    private float maxSpeed = 160.0f;
    private float speed = 0.0f;
    public float x_minAngle = -99.534f;
    public float x_maxAngle = -344.019f;
    public float y_minAngle = -134.48f;
    public float y_maxAngle = -71.864f;
    public float z_minAngle = -24.58f;
    public float z_maxAngle = -99.014f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        speed = car.velocity.magnitude * 3.6f;
        
        //Debug.Log((int)speed);

        if (arrow != null)
            arrow.localEulerAngles = new Vector3(Mathf.Lerp(x_minAngle, x_maxAngle, speed / maxSpeed), Mathf.Lerp(y_minAngle, y_maxAngle, speed / maxSpeed), Mathf.Lerp(z_minAngle, z_maxAngle, speed / maxSpeed));
    }
}
