using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public float cameraScroll = 5f;
    public float cameraMove = 7f;
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    public void UpdateCamera()
    {
        //if (UserControl.instance.control == Control.PlayerChoice) body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * cameraMove, Input.GetAxisRaw("Vertical") * cameraMove);
        //else
        //{
        //    Vector3 targetPos = new Vector3(UserControl.instance.controlledCharacter.transform.position.x, UserControl.instance.controlledCharacter.transform.position.y, -10);
        //    transform.position = Vector3.Lerp(transform.position, targetPos, cameraMove * Time.deltaTime);
        //}
        //Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * cameraScroll;
    }

    private void Move(int x, int y)
    {
        Vector3 v = new Vector3(x, y).normalized;
        transform.Translate(v * Time.deltaTime * cameraMove);
    }
}