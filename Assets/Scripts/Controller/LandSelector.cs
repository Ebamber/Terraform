using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSelector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                if (hit.transform.tag.Equals("Tile")){
                    Debug.Log("ACTIVATE CLAIM FUNCTION IN TURN MANAGER");
                }
            }
        }
    }
}
