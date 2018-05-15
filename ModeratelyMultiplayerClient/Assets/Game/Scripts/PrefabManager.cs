using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public LayerMask PrefabsLayer;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit[] hits = Physics.RaycastAll(ray, PrefabsLayer);
            
            foreach (RaycastHit hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name + " clicked");
                //hit.collider.gameObject.GetComponent<>
                //hit.collider.gameObject
            }
        }
    }

    public void SetEditMode(bool toggle)
    {
        enabled = toggle;
    }
}
