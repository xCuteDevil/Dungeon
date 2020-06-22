using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject meeleAttackCollider;

    public Vector2 aimVector;
    public Vector2 crosshairPos;

    public static Vector2 mousePos;
    public float crosshairOffset;
    
    public Camera cam;

    public Vector2 GetMousePos()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }

    public void MoveCrosshair(Vector2 mousePos)
    {
        aimVector = new Vector2(mousePos.x, mousePos.y);
        aimVector.Normalize();
        crosshair.transform.localPosition = aimVector * crosshairOffset;
    }

    public void MoveMeeleAttackCollider(Vector2 mousePos)
    {
        aimVector = new Vector2(mousePos.x, mousePos.y);
        aimVector.Normalize();
        meeleAttackCollider.transform.localPosition = aimVector * (PlayerMeeleCombat.attackRange);
    }

    void Update()
    {
        GetMousePos();
        MoveCrosshair(GetMousePos());
        MoveMeeleAttackCollider(GetMousePos());
    }
}
