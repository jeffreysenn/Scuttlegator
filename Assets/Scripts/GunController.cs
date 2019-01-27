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
    [SerializeField] private int maxAmmo = 3;
    private int currentAmmo;
    [SerializeField] float ammoRefillTime = 2;
    private float refillTimer;
    [SerializeField] private float coolDown = 0.3f;
    private float coolDownTimer;
    private bool startCoolDown;
    public GameObject[] bullets;
    [SerializeField] private float rayCastDistance = 100;
    [SerializeField] private int rayCastBuffer = 4;
    private GameObject muzzle;
    private GameObject player;
    private ContactFilter2D contactFilter;
    private LineRenderer lineRenderer;
    public Gradient lineGradientAiming;
    public Gradient[] lineGradientFiring = new Gradient[2];
    [SerializeField] float lineLengthNotHit = 50;

    public GameObject PoppedBalloon;

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
        lineRenderer = GetComponent<LineRenderer>();

        contactFilter.useTriggers = false;

        currentAmmo = maxAmmo;
        coolDownTimer = 0;
        startCoolDown = false;
        refillTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) { return; }
        AimAtMouse();
        DrawLine(lineGradientAiming);

        if (startCoolDown) { coolDownTimer -= Time.deltaTime; }
        if(coolDownTimer < 0) { coolDownTimer = 0; startCoolDown = false; }
        if(currentAmmo < maxAmmo && refillTimer > 0) { refillTimer -= Time.deltaTime; }
        if(refillTimer < 0)
        {
            currentAmmo++;
            refillTimer = ammoRefillTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(currentAmmo > 0 && coolDownTimer == 0 && BallonInfo.activeBalloonNum < maxBalloonNum)
            {
                // InstantiateBullet(0);
                DrawLine(lineGradientFiring[0]);
                coolDownTimer = coolDown;
                startCoolDown = true;
                currentAmmo--;
                refillTimer = ammoRefillTime;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //InstantiateBullet(1);
            if (GetHitResult(out RaycastHit2D hit))
            {
                if (hit.collider.gameObject.CompareTag("Balloon"))
                {
                    BallonInfo.activeBalloonNum--;
                    GameObject g = Instantiate(PoppedBalloon, hit.transform.position, hit.transform.rotation);
                    g.GetComponent<Rigidbody2D>().velocity = hit.rigidbody.velocity;
                    Destroy(hit.transform.gameObject);
                }

            }
        }


    }

    void DrawLine(Gradient gradient)
    {
        lineRenderer.colorGradient = gradient;
        Vector3[] points = new Vector3[2];
        points[0] = muzzle.transform.position;
        RaycastHit2D outHit;
        if (!GetHitResult(out outHit))
        {
            points[1] = points[0] + muzzle.transform.up * lineLengthNotHit;
        }
        else
        {
            points[1] = outHit.point;
        }
        lineRenderer.SetPositions(points);
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

    public bool CanShoot(out RaycastHit2D outHit)
    {
        outHit = new RaycastHit2D();
        RaycastHit2D[] hits = new RaycastHit2D[rayCastBuffer];
        if (!(currentAmmo > 0 && coolDownTimer == 0 && BallonInfo.activeBalloonNum < maxBalloonNum))
        {
            return false;
        }
        if(GetHitResult(out outHit))
        {
            return true;
        }
        return false;
    }

    public bool GetHitResult(out RaycastHit2D outHit)
    {
        outHit = new RaycastHit2D();
        RaycastHit2D[] hits = new RaycastHit2D[rayCastBuffer];
        int hitNumbers = Physics2D.Raycast(muzzle.transform.position, muzzle.transform.up, contactFilter, hits, rayCastDistance);
        if(hitNumbers > 0)
        {
            outHit = hits[0];
            return true;
        }
        return false;
    }

    public int GetCurrentAmmu() { return currentAmmo; }
    public int GetMaxAmmu() { return maxAmmo; }
}
