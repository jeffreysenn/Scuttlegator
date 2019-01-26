using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBallonLine : MonoBehaviour
{

    private LineRenderer line;
    private SpringJoint2D joint;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        joint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LineTest();   
    }
    
    private void LineTest()
    {
        line.SetPosition(0, joint.anchor);
        line.SetPosition(1, joint.connectedAnchor);
    }
}
