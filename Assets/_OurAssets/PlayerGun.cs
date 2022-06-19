using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    public Image crossHair;
    public Transform shootPos;
    public GameObject _bullet;
    private GameObject currentBullet;
    private Vector3 aim;

    private Ray hitInfo;
    //Create a ray. Syntax: Physics.Raycast(startPosition, direction, hitInfo)


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(crossHair.transform.position);

                GameObject bullet = Instantiate(_bullet, crossHair.transform.position, crossHair.transform.rotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

                Vector3 direction = (ray.GetPoint(-0.5f) - bullet.transform.position) ;

                bulletRigidbody.AddForce(-direction, ForceMode.Impulse);


            }
        }





        
    }
}
