using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject weaponPrefab1;

    [SerializeField] bool canSpawn = false;
    private Vector3 myLoc;

    private void Awake()
    {
        myLoc.x = transform.position.x;
        myLoc.y = transform.position.y;
        myLoc.z = transform.position.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        canSpawn = true;
        canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canSpawn = false;
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (canSpawn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //take players money
                Instantiate(weaponPrefab1, myLoc, Quaternion.identity);
                gameObject.SetActive(false);

                canvas.SetActive(false);
                canSpawn = false;
            }
        }
    }

}