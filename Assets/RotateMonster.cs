using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMonster : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {

        
            //rotate

            Vector3 targ = collision.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        


    }
}
