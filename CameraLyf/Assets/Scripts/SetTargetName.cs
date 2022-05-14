using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetName : MonoBehaviour
{
    public void SetTarget()
    {
        PopupSelect.mTarget = this.gameObject;
    }
}
