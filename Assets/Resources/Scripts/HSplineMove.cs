using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSplineMove : MonoBehaviour
{
    public GameObject[] path;
    public float speed;
    private float distUsed;
 
    struct HSpline
    {
        public Vector3[] point;
        public Vector3[] tangent;
        public float[] length;
    }
    
    HSpline spline = new HSpline();
    
    Vector3 HSplineInterp(HSpline spline, float t)
    {
        if (t < 0.0f) { t = 0.0f; }
        if (t > 1.0f) { t = 1.0f; }
        
        float tlen = t * spline.length[spline.length.Length - 1];
        float s = (tlen - spline.length[0]) / (spline.length[1] - spline.length[0]);
        
        float s2 = Mathf.Pow(s, 2);
        float s3 = Mathf.Pow(s, 3);
        Vector3 pos = (2 * s3 - 3 * s2 + 1) * spline.point[0] +
                      (s3 - 2 * s2 + s) * spline.tangent[0] +
                      (-2 * s3 + 3 * s2) * spline.point[1] +
                      (s3 - s2) * spline.tangent[1];
        return pos;
    }
    
    void Start()
    {
        List<Vector3> plist = new List<Vector3>();

        for (int i = 0; i < path.Length; i++)
        {
            plist.Add(path[i].transform.position);
        }

        List<Vector3> tlist = new List<Vector3>();
        List<float> llist = new List<float>();
        tlist.Add(gameObject.transform.rotation * Vector3.forward);
        llist.Add(0.0f);

        for (int i = 1; i < (path.Length - 1); i++)
        {
            tlist.Add((plist[i + 1] - plist[i - 1]) / 2.0f);
            llist.Add(llist[i - 1] + Vector3.Distance(plist[i], plist[i - 1]));
        }

        tlist.Add(Quaternion.LookRotation(path[1].transform.position - path[0].transform.position) * Vector3.forward);
        llist.Add(llist[path.Length - 2] + Vector3.Distance(plist[path.Length - 1], plist[path.Length - 2]));
        
        spline.point = plist.ToArray();
        spline.tangent = tlist.ToArray();
        spline.length = llist.ToArray();

        distUsed = Vector3.Distance(path[0].transform.position, path[1].transform.position);
        
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        float s = 0.0f;
        float sInc = 0.0f;
        while (s < 1.0f)
        {
            this.transform.position = HSplineInterp(spline, s);
            Vector3 curve_tan = HSplineInterp(spline, s + ((0.1f / distUsed) * speed)) - HSplineInterp(spline, s);
            curve_tan.Normalize();

            if (s >= 0.99f)
            {
                curve_tan = spline.tangent[spline.tangent.Length - 1];
            }

            Quaternion orient = new Quaternion();
            orient.SetLookRotation(curve_tan, Vector3.up);

            orient *= Quaternion.AngleAxis(180, Vector3.forward);
            orient *= Quaternion.AngleAxis(180, Vector3.right);
            orient *= Quaternion.AngleAxis(180, Vector3.up);

            this.transform.rotation = orient;
            this.GetComponent<Unit>().oldPos = this.transform.position;

            sInc += (0.05f / distUsed) * speed;
            s = Mathf.SmoothStep(0, 1, sInc);
            yield return new WaitForSeconds(0.05f);
        }
        this.transform.position = spline.point[spline.point.Length - 1];
        yield return null;

        Destroy(path[0]);
        Destroy(path[1]);
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
