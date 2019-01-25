using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private GameObject bullet;
    private GameObject muzzle;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        muzzle = transform.Find("Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) { return; }
        AimAtMouse();

        if (Input.GetMouseButtonDown(0))
        {
            InstantiateBullet();
        }
    }

    void AimAtMouse()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, transform.position.z));
        transform.up = (Vector2)(mouseWorld - player.transform.position);
    }

    void InstantiateBullet()
    {
        if(bullet == null) { return; }
        GameObject newbullet = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
        Vector2 parentVelocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector2 relativeVelocity = muzzle.transform.up * bulletSpeed;
        newbullet.GetComponent<BulletController>().SetLaunchVelocity(parentVelocity + relativeVelocity);
    }
}
