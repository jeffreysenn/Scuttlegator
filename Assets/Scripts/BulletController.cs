using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2;
    private Vector2 launchVelocity = Vector2.zero;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(launchVelocity.x * Time.deltaTime, launchVelocity.y * Time.deltaTime, 0);

        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetLaunchVelocity(Vector2 absoluteVelocity)
    {
        launchVelocity = absoluteVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }


}
