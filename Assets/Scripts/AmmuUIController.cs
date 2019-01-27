using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmuUIController : MonoBehaviour
{
    GunController gunController;
    GameObject gunObj;
    public Text ammuNum;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        gunObj = transform.Find("Gun").gameObject;
        gunController = gunObj.GetComponent<GunController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gunController.GetCurrentAmmu();
        ammuNum.text = gunController.GetCurrentAmmu().ToString();
        ammuNum.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
