using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rigidbody;

    public Vector3 target = Vector3.zero;
    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = transform.position;

        Fire();
    }
    // Update is called once per frame
    void Update()
    {
        // rigidbody.velocity = target.normalized * speed;
        Fire();
    }

    public void Fire(){
        // rigidbody.AddForce(target.normalized * speed,ForceMode.Force);
        transform.position = Vector3.MoveTowards(transform.position,target,speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("TargetGame1")){
            //Show question
            // other.GetComponent<Target>().HideTarget();

            //new show sub target
            other.GetComponent<Target>().ShowSubTarget();
        }

        if(other.CompareTag("SubTargetGame1")){
            //calculate

            //Check answer and Hide Target
            other.GetComponent<SubTarget>().target.HideSubTarget(other.GetComponent<SubTarget>().answer);
            other.GetComponent<SubTarget>().target.HideTarget();
        }

        Destroy(this.gameObject);
    }

    
}
