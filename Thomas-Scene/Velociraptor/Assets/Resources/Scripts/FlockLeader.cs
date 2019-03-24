using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockLeader : MonoBehaviour
{
    public int dmg;
    public float t, attT;
    public float range, attSpeed;
    public float speed = 1.0F;
    float time;
    public bool isLeader;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        speed = 12;
        //Parent
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (isLeader == true)
        {
            transform.Rotate(0, 1, 0);

            if (time % 2 < 1)
            {
                //transform.Rotate(0, 0.5f, 0);
                float angle = 0.25f;
                Quaternion rot = this.transform.rotation;
                rot = rot * Quaternion.Euler(0, angle, 0);
                this.transform.rotation = rot;
            }            //Transform trans;
            else
            {
                Debug.Log("I SHOULD BE FUCKING TURNING");
                //transform.Rotate(0, -0.5f, 0);
                
                float angle = -2f;
                Quaternion rot = this.transform.rotation;
                rot = rot * Quaternion.Euler(0, angle, 0);
                this.transform.rotation = rot;
            }
            Vector3 myvec = new Vector3(0, 0, speed * Time.deltaTime);
            myvec = this.transform.rotation * myvec;

            Vector3 vec = this.transform.position;
            Rigidbody body = this.GetComponent<Rigidbody>();
            body.AddForce(myvec, ForceMode.Impulse);
            //this.transform.position = vec;
        }
        else
        {
            GameObject target = GetComponentInParent<Flock>().leader;
            if ((this.transform.position - target.transform.position).magnitude > 10)
            {
                Vector3 targetVector = target.transform.position-this.transform.position;
                Vector3 temp = new Vector3(0, 0, 1);
                temp = this.transform.rotation*temp;
                float angle = Vector3.SignedAngle(temp, targetVector,Vector3.up);
                if (angle > 2)
                {
                    angle = 2;
                }
                else if (angle < -2)
                {
                    angle = -2;
                }
                Quaternion rot = this.transform.rotation;
                rot = rot * Quaternion.Euler(0,angle,0);
                this.transform.rotation = rot;

                Vector3 myvec = new Vector3(0, 0, speed * Time.deltaTime);
                myvec = this.transform.rotation * myvec;

                Vector3 vec = this.transform.position;
                Rigidbody body = this.GetComponent<Rigidbody>();
                body.AddForce(myvec, ForceMode.Impulse);
            }
        }


    }

}
