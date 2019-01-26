﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10;
    public GameObject bullet;
    [SerializeField] private float rayCastDistance = 100;
    [SerializeField] private int rayCastBuffer = 4;
    private GameObject muzzle;
    private GameObject player;
    private ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        muzzle = transform.Find("Muzzle").gameObject;

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(bullet.gameObject.layer));
        contactFilter.useLayerMask = true;
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

    public bool GetHitResult(out RaycastHit2D outHit)
    {
        RaycastHit2D[] hits = new RaycastHit2D[rayCastBuffer];
        int hitNumbers = Physics2D.Raycast(muzzle.transform.position, muzzle.transform.up, contactFilter, hits, rayCastDistance);
        if(hitNumbers > 0)
        {
            outHit = hits[0];
            Debug.Log(outHit.point);
            return true;
        }
        outHit = new RaycastHit2D();
        return false;
    }
}
