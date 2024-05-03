using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    private void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }
            
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            //wŰ�����ų� ���콺�� ����� ȭ�� ���κа�輱�κ� ���� �����Ѱ��
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime,Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            //sŰ�����ų� ���콺�� ����� ȭ�� �غκа�輱�κ� ���� �����Ѱ��
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            //dŰ�����ų� ���콺�� ����� ȭ�� �����ʺκа�輱�κ� ���� �����Ѱ��
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            //aŰ�����ų� ���콺�� ����� ȭ�� ���ʺκа�輱�κ� ���� �����Ѱ��
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY,maxY);

        transform.position = pos;
    }
}
