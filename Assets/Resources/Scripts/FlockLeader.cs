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
    int counter;
    Vector3[] target;
    int counter2;
    float targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        speed = 12;
        counter = 0;
        //target = new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));
        target = new Vector3[4];

        target[0] = new Vector3(20, 0, 20);
        target[1] = new Vector3(-30, 0, 20);
        target[2] = new Vector3(-20, 0, -40);
        target[3] = new Vector3(20, 0, -20);
        //Parent
        counter2 = 0;
        targetAngle = Random.Range(-10, 10);
        targetAngle = targetAngle / 10;
        if (targetAngle < 0 && targetAngle > -0.4) targetAngle = -0.4f;
        if (targetAngle > 0 && targetAngle < 0.4) targetAngle = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (isLeader == true)
        {
            if (time < 10)
            {
                if ((int)time / 2 > counter2)
                {
                    targetAngle = Random.Range(-10, 10);
                    targetAngle = targetAngle / 10;
                    if (targetAngle < 0 && targetAngle > -0.4) targetAngle = -0.4f;
                    if (targetAngle > 0 && targetAngle < 0.4) targetAngle = 0.4f;
                    Debug.Log(targetAngle);
                    counter2++;
                }
                //angle = angle/2;
                Quaternion rot = this.transform.rotation;
                rot = rot * Quaternion.Euler(0, targetAngle, 0);
                this.transform.rotation = rot;

                Vector3 myvec = new Vector3(0, 0, speed * Time.deltaTime);
                myvec = this.transform.rotation * myvec;

                Vector3 vec = this.transform.position;
                Rigidbody body = this.GetComponent<Rigidbody>();
                body.AddForce(myvec, ForceMode.Impulse);
                //this.transform.position = vec;
            }
            else
            {


                if ((this.transform.position - target[((int)time / 20) % 4]).magnitude > 10)
                {
                    Rigidbody body = GetComponent<Rigidbody>();
                    Vector3 targetVector = target[((int)time / 20) % 4] - this.transform.position;
                    Vector3 temp = new Vector3(0, 0, 1);
                    temp = this.transform.rotation * temp;
                    float angle = Vector3.SignedAngle(temp, targetVector, Vector3.up);
                    if (angle > 2)
                    {
                        angle = 2;
                    }
                    else if (angle < -2)
                    {
                        angle = -2;
                    }
                    Quaternion rot = this.transform.rotation;
                    rot = rot * Quaternion.Euler(0, angle, 0);
                    this.transform.rotation = rot;

                    Vector3 myvec = new Vector3(0, 0, speed * Time.deltaTime);
                    myvec = this.transform.rotation * myvec;

                    Vector3 vec = this.transform.position;

                    body.AddForce(myvec, ForceMode.Impulse);
                }




            }
        }
        else
        {
            GameObject target = GetComponentInParent<Flock>().leader;
            float total = 0;
            float count = 0;
            foreach (GameObject raptor in GetComponentInParent<Flock>().units)
            {

                if (raptor != null && (this.transform.position - raptor.transform.position).magnitude < 5)
                {
                    Vector3 targetVector = raptor.transform.position - this.transform.position;
                    Vector3 temp = new Vector3(0, 0, 1);
                    temp = this.transform.rotation * temp;
                    float angle = Vector3.SignedAngle(temp, targetVector, Vector3.up);
                    total += angle;
                    count++;
                }
            }
            float average;
            Quaternion rot2;
            Vector3 space;
            Rigidbody body;
            average = total / count;
            rot2 = this.transform.rotation;
            rot2 = rot2 * Quaternion.Euler(0, average + 180, 0);
            space = new Vector3(0, 0, speed * Time.deltaTime);
            space = rot2 * space;
            body = this.GetComponent<Rigidbody>();
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

            if (count != 0)
            {
                average = total / count;
                rot2 = this.transform.rotation;
                rot2 = rot2 * Quaternion.Euler(0, average, 0);
                space = new Vector3(0, 0, speed * Time.deltaTime);
                space = rot2 * space;
                body = this.GetComponent<Rigidbody>();
                if (average == double.NaN)
                {
                    Debug.Log("I'm here");
                }
                body.AddForce(space, ForceMode.Impulse);
            }


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
