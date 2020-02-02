using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSelector : MonoBehaviour
{

    public GameManager manager;

    private void Start()
    {
        manager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.tag.Equals("Tile")){
                    if (!manager.activeCardManager.bushfire)
                    {
                        manager.TryClaimTile(hit.transform.gameObject.GetComponent<Tile>());
                    }
                    else
                    {
                        Debug.Log("We are in Land selector under the BUSHFIRE card");
                        manager.TryBushfire(hit.transform.gameObject.GetComponent<Tile>());
                    }
                }
            }
        }
    }

}
