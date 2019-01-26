using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{

    public GameObject balloonPrefab;
    public GunController gunController;

    // Start is called before the first frame update
    void Start()
    {
        if (!gunController) Debug.LogError("gun controller not set");
        if (!balloonPrefab) Debug.LogError("balloon prefab not set");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;
            if(gunController.GetHitResult(out hit))
            {
                Vector3 tetherPosition = new Vector3(hit.point.x, hit.point.y, 0);
                
                GameObject balloon = Instantiate(balloonPrefab, tetherPosition, Quaternion.identity);
                SpringJoint2D joint = balloon.GetComponent<SpringJoint2D>();
                joint.connectedAnchor = hit.point;
                joint.connectedBody = hit.rigidbody;
                joint.distance = 10.0f;

            }
            
            

        }
    }
}
