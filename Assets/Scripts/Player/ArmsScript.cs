using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsScript : MonoBehaviour
{
   [SerializeField] private GameObject leftChar;
   [SerializeField] private GameObject rightChar;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(leftChar.transform.position, rightChar.transform.position, 0.5f);
        float scalex = Mathf.Abs((leftChar.transform.position - rightChar.transform.position).magnitude*(transform.localScale.x/ transform.lossyScale.x));
        transform.localScale = new Vector3(scalex, transform.lossyScale.y, transform.lossyScale.z);
    }
}
