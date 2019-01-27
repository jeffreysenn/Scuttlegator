using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{

    public GameObject balloonPrefab;
    public GunController gunController;
    public float swayFactor = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
        

        BallonInfo.activeBalloonNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BallonInfo.activeBalloonNum < gunController.maxBalloonNum)
        {
            RaycastHit2D hit;

            if(gunController.CanShoot(out hit))
            {
                
                    if(hit.rigidbody == null) { return; }
                    Vector3 tetherPosition = new Vector3(hit.point.x, hit.point.y, 0);

                    GameObject balloon = Instantiate(balloonPrefab, tetherPosition, Quaternion.identity);
                    SpringJoint2D joint = balloon.GetComponent<SpringJoint2D>();
                    AudioManager.instance.PlaySound("BalloonSpawn", 1.0f, true, 0.1f);

                    joint.connectedBody = hit.rigidbody;

                    Vector4 worldPos = joint.connectedBody.transform.worldToLocalMatrix * new Vector4(hit.point.x, hit.point.y, 0, 1);
                    joint.connectedAnchor = new Vector2(worldPos.x, worldPos.y);

                    joint.distance = 5.0f;

                    if (true)
                    {
                        hit.rigidbody.AddForce(new Vector2((hit.point.x - hit.transform.position.x) * swayFactor, 0.0f), ForceMode2D.Impulse);
                    }

                    BallonInfo.activeBalloonNum++;
                
                 

            }
            
            

        }
    }
}
