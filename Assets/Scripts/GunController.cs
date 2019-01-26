using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct BallonInfo
{
    public static int activeBalloonNum = 0;
}

public class GunController : MonoBehaviour
{
    public int maxBalloonNum = 8;
    [SerializeField] private float bulletSpeed = 10;
    public GameObject[] bullets;
    [SerializeField] private float rayCastDistance = 100;
    [SerializeField] private int rayCastBuffer = 4;
    private GameObject muzzle;
    private GameObject player;
    private ContactFilter2D contactFilter;

    //void FindAssets()
    //{
    //    bullets = new GameObject[2];
    //    bullets[0] = (GameObject)Resources.Load("Assets/Prefabs/Bullet1", typeof(GameObject));
    //    bullets[1] = (GameObject)Resources.Load("Assets/Prefabs/Bullet2", typeof(GameObject));
    //}

    // Start is called before the first frame update
    void Start()
    {
        //FindAssets();

        player = GameObject.FindGameObjectWithTag("Player");
        muzzle = transform.Find("Muzzle").gameObject;

        contactFilter.useTriggers = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) { return; }
        AimAtMouse();

        if (Input.GetButtonDown("Fire1"))
        {
            if(BallonInfo.activeBalloonNum < maxBalloonNum)
            {
                // InstantiateBullet(0);
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            InstantiateBullet(1);
        }

    }

    void AimAtMouse()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, transform.position.z));
        transform.up = (Vector2)(mouseWorld - player.transform.position);
    }

    void InstantiateBullet(int bulletIndex)
    {
        if(bullets[bulletIndex] == null) { return; }
        GameObject newbullet = Instantiate(bullets[bulletIndex], muzzle.transform.position, muzzle.transform.rotation);
        Vector2 parentVelocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector2 relativeVelocity = muzzle.transform.up * bulletSpeed;
        newbullet.GetComponent<BulletController>().SetLaunchVelocity(parentVelocity + relativeVelocity);
    }

    public bool GetHitResult(out RaycastHit2D outHit)
    {
        RaycastHit2D[] hits = new RaycastHit2D[rayCastBuffer];
        int hitNumbers = Physics2D.Raycast(muzzle.transform.position, muzzle.transform.up, contactFilter, hits, rayCastDistance);
        if(hitNumbers > 0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                if (hit.transform)
                {
                    if (hit.transform.gameObject.CompareTag("House"))
                    {
                        outHit = hit;
                        return true;
                    }
                }
                
            }
        }
        outHit = new RaycastHit2D();
        return false;
    }
}
