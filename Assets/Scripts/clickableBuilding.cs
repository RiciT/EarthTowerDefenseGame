using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class clickableBuilding : MonoBehaviour
{
    public GameObject building;
    public GameObject root;
    public float buildingPickupRadius = 0.5f;
    private ContactFilter2D contactFilter2D;
    GameObject currentBuilding;
    public Collider2D[] allSlot;
    private int wastouching = -1;

    private void Start()
    {
        currentBuilding = null;
        contactFilter2D = new ContactFilter2D();
        LayerMask mask = LayerMask.NameToLayer("Slot");
        contactFilter2D.SetLayerMask(mask);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBuilding != null)
        {
            currentBuilding.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            
            for (int i = 0; i < allSlot.Length; i++)
            {
                if (currentBuilding.GetComponent<BoxCollider2D>().IsTouching(allSlot[i]))
                {
                    if (Input.GetMouseButtonDown(0) && currentBuilding.transform.parent == allSlot[i].transform)
                    {
                        //Debug.Log(allSlot[i].transform.name.ToString());
                        currentBuilding.transform.localPosition = new Vector3(0, 0, 0);
                        currentBuilding.transform.localEulerAngles = new Vector3(0, 0, 0);
                        currentBuilding.transform.localScale = new Vector3(1, 1, 1);
                        if (currentBuilding.GetComponent<Building>())
                        {
                            currentBuilding.GetComponent<Building>().SetPlaced(true);
                        }
                        currentBuilding = null;
                        allSlot[i].transform.GetComponent<Collider2D>().enabled = false;
                        break;
                    }
                    else if (currentBuilding.transform.parent != allSlot[i].transform)
                    {
                        currentBuilding.transform.SetParent(allSlot[i].transform);
                        currentBuilding.transform.localEulerAngles = new Vector3(0, 0, 0);
                        currentBuilding.transform.localScale = new Vector3(1, 1, 1);
                        wastouching = i;
                    }
                }
                else if (wastouching == i)
                {
                    currentBuilding.transform.SetParent(root.transform);
                    currentBuilding.transform.localScale = new Vector3(1, 1, 1);
                    //currentBuilding.transform.eulerAngles = new Vector3(0, 0, 0);
                    wastouching = -1;
                }
                //Debug.Log(wastouching);
            }
        }

        else if (Input.GetMouseButtonDown(0) && Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) < buildingPickupRadius &&
            Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y) < buildingPickupRadius)
        {
            currentBuilding = Instantiate(building, new Vector3(transform.position.x, transform.position.y, 2),
                Quaternion.identity, root.transform);
        }
    }
}
