using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rigidbody;
    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("TargetGame1")){
            //Show question
        }

        Destroy(this.gameObject);
    }
}
