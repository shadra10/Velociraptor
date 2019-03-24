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
            float total = 0;
            float count = 0;
            foreach (GameObject raptor in GetComponentInParent<Flock>().units)
            {
                
                if (raptor!=null&&(this.transform.position - raptor.transform.position).magnitude < 5)
                {
                    Vector3 targetVector = raptor.transform.position - this.transform.position;
                    Vector3 temp = new Vector3(0, 0, 1);
                    temp = this.transform.rotation * temp;
                    float angle = Vector3.SignedAngle(temp, targetVector, Vector3.up);
                    total += angle;
                    count++;
                }
            }
            float average = total / count;
            Quaternion rot2 = this.transform.rotation;
            rot2 = rot2 * Quaternion.Euler(0, average+180, 0);
            Vector3 space = new Vector3(0, 0, speed * Time.deltaTime);
            space = rot2*space;
            Rigidbody body = this.GetComponent<Rigidbody>();
            body.AddForce(space, ForceMode.Impulse);
            

            
            
            total = 0;
            count = 0;
            foreach (GameObject raptor in GetComponentInParent<Flock>().units)
            {

                if (raptor != null && (this.transform.position - raptor.transform.position).magnitude > 10)
                {
                    Vector3 targetVector = raptor.transform.position - this.transform.position;
                    Vector3 temp = new Vector3(0, 0, 1);
                    temp = this.transform.rotation * temp;
                    float angle = Vector3.SignedAngle(temp, targetVector, Vector3.up);
                    total += angle;
                    count++;
                }
            }

            average = total / count;
            rot2 = this.transform.rotation;
            rot2 = rot2 * Quaternion.Euler(0, average, 0);
            Vector3 space2 = new Vector3(0, 0, speed * Time.deltaTime);
            space2 = rot2 * space2;
            body = this.GetComponent<Rigidbody>();
            if (average == double.NaN)
            {
                Debug.Log("I'm here");
            }
            body.AddForce(space2, ForceMode.Impulse);

            
            total = 0;
            count = 0;
            foreach (GameObject raptor in GetComponentInParent<Flock>().units)
            {
                //Vector3 targetVector = raptor.transform.rotation;// - this.transform.position;
                if (raptor != null)
                {
                    Vector3 temp = new Vector3(0, 0, 1);
                    Vector3 temp2 = new Vector3(0, 0, 1);
                    temp = this.transform.rotation * temp;
                    temp2 = raptor.transform.rotation * temp2;
                    float angle = Vector3.SignedAngle(temp, temp2, Vector3.up);
                    total += angle;
                    count++;
                }
            }

            average = total / count;

            if (average > 0.25)
            {
                average = 0.25f;
            }
            else if (average < -0.25)
            {
                average = -0.25f;
            }
            Quaternion rot = this.transform.rotation;
            rot = rot * Quaternion.Euler(0, average, 0);
            this.transform.rotation = rot;

            space = new Vector3(0, 0, speed * Time.deltaTime);
            space = rot2 * space;
            body = this.GetComponent<Rigidbody>();
            body.AddForce(space, ForceMode.Impulse);

        }


    }

}
            /*
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
                
                body.AddForce(myvec, ForceMode.Impulse);
            }*/