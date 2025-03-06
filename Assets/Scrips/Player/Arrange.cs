using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrange : MonoBehaviour
{
    new private Camera camera;
    public float arrangeDistance;
    private GameObject ghostObject;
    public Transform displaytran;
    void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (CharacterManager.Instance.Player.itemData != null)
            Ghosting();
    }
    public void OnArrange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && CharacterManager.Instance.Player.hand.NowEuqipped())
        {
            Instantiate(CharacterManager.Instance.Player.itemData.dropPrefab, ghostObject.transform.position, Quaternion.identity);
            CharacterManager.Instance.Player.hand.Arrange();
            CharacterManager.Instance.Player.itemData = null;
            Destroy(ghostObject);
        }
    }
    public void Ghosting()
    {

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Raycast�� �浹 Ȯ��
        if (Physics.Raycast(ray, out hit, arrangeDistance))
        {
            // �浹�� ������Ʈ�� "Ground"�� ���
            if (hit.collider.CompareTag("Ground"))
            {
                // ����(����)�� �浹�� ���
                if (hit.normal == Vector3.up)
                {
                    // �̸����� �������� ���ٸ� ����
                    if (ghostObject == null)
                    {
                        ghostObject = Instantiate(CharacterManager.Instance.Player.itemData.ghostPrefab, hit.point, Quaternion.identity);
                    }

                    // �̸����� �������� �浹 ��ġ�� ������Ʈ
                    ghostObject.transform.position = new Vector3(
                        Mathf.RoundToInt(hit.point.x),  // X��ǥ�� ������ �ݿø�
                        Mathf.RoundToInt(hit.point.y),  // Y��ǥ�� ������ �ݿø�
                        Mathf.RoundToInt(hit.point.z)   // Z��ǥ�� ������ �ݿø�
                    );
                    ghostObject.SetActive(true);
                }
            }
        }
        else
        {
            // ���̰� ���鿡 ���� ������ �̸����� �������� ��Ȱ��ȭ
            if (ghostObject != null)
            {
                ghostObject.SetActive(false);
            }
        }
    }
}
