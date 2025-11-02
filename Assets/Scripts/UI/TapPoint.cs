using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPoint : MonoBehaviour
{
    [SerializeField]private GameObject point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        point.SetActive(Player.ScreenPressed);
        if (Camera.main.orthographic)
        {
            point.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Player.TouchStartPosition).x, Camera.main.ScreenToWorldPoint(Player.TouchStartPosition).y, transform.position.z);
        }
        else
        {
            point.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Player.TouchStartPosition).x, Camera.main.ScreenToWorldPoint(Player.TouchStartPosition).y, transform.position.z);

        }

    }
}
