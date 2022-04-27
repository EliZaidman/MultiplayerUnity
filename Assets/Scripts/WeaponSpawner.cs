using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> UIElements;
    [SerializeField] GameObject WeaponType1;

    [SerializeField] bool canSpawn = false;
    private int startingWeapon = 0;

    private void Awake()
    {
        //add specific ui to the list
    }

    private void OnTriggerEnter(Collider other)
    {
        canSpawn = true;
        foreach (var item in UIElements)
        {
            item.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canSpawn = false;
        foreach (var item in UIElements)
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        if (canSpawn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //take players money
                Instantiate(WeaponType1, transform);
                gameObject.SetActive(false);
                print("E pressed");
            }
        }
    }

}