using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControllerHoverObject : MonoBehaviour
{
    public GameObject CurrentMenuItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.StartsWith("Item"))
            CurrentMenuItem = other.gameObject;
        //CurrentMenuItem = FindMenuItem(other);
    }

    private GameObject FindMenuItem(Collider other)
    {
        GameObject menuItem = null;
        Transform currentParent = other.transform.parent;
        int parentCounter = 0;

        while (menuItem == null && parentCounter < 15)
        {
            if (currentParent.gameObject && currentParent.gameObject.name != null && currentParent.gameObject.name.StartsWith("Item"))
            {
                menuItem = currentParent.gameObject;
                currentParent = null;
            }
            else
                currentParent = currentParent.parent;

            parentCounter++;
        }
        
        return menuItem;
    }
}
