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
        //line.SetPosition(0, transform.position);
        //line.SetPosition(0, transform.position);
        //line.SetPosition(1, joint.connectedBody.transform.position);

        line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        Vector4 worldPos = joint.connectedBody.transform.localToWorldMatrix * new Vector4(joint.connectedAnchor.x, joint.connectedAnchor.y, 0, 1);
        line.SetPosition(1, new Vector3(worldPos.x, worldPos.y, 0));

    }
}
