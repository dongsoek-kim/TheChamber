using System.Collections;
//using UnityEditor.Build;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Keycard Keycard;
    public GameObject leftDoor;
    public GameObject rightDoor;
    private Vector3 leftDoorOriginalPos;
    private Vector3 rightDoorOriginalPos;
    private bool doorOpen;
    private void Start()
    {
        leftDoorOriginalPos = leftDoor.transform.position;
        rightDoorOriginalPos = rightDoor.transform.position;
    }

    /// <summary>
    /// 키카드소지여부 판별이후 소지시 문 열어주는 메서드
    /// </summary>
    public void DoorOpen()
    {
        if (HasKeycard(Keycard))
        {
            if (!doorOpen)
            {
                doorOpen = true;
                StartCoroutine(OpenAndCloseDoor());
            }
        }
        else
        {
            Debug.Log("열쇠 카드가 필요합니다!");
        }
    }

    /// <summary>
    /// 문열고 닫는 동작 메서드
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenAndCloseDoor()
    {
        Vector3 leftDoorTarget = leftDoorOriginalPos + leftDoor.transform.forward * 1.5f;
        Vector3 rightDoorTarget = rightDoorOriginalPos + rightDoor.transform.forward * -1.5f;

        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            leftDoor.transform.position = Vector3.Lerp(leftDoorOriginalPos, leftDoorTarget, elapsedTime / duration);
            rightDoor.transform.position = Vector3.Lerp(rightDoorOriginalPos, rightDoorTarget, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        leftDoor.transform.position = leftDoorTarget;
        rightDoor.transform.position = rightDoorTarget;

        yield return new WaitForSeconds(5f); 

        elapsedTime = 0f;

       
        while (elapsedTime < duration)
        {
            leftDoor.transform.position = Vector3.Lerp(leftDoorTarget, leftDoorOriginalPos, elapsedTime / duration);
            rightDoor.transform.position = Vector3.Lerp(rightDoorTarget, rightDoorOriginalPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    
        leftDoor.transform.position = leftDoorOriginalPos;
        rightDoor.transform.position = rightDoorOriginalPos;
        doorOpen = false;
    }
    /// <summary>
    /// 키카드 보유여부 판별 메서드
    /// </summary>
    /// <param name="keycard"></param>
    /// <returns></returns>
    public bool HasKeycard(Keycard keycard)
    {
        return CharacterManager.Instance.Player.keycard[(int)keycard];
    }
}
