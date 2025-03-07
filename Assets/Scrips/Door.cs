using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Keycard Keycard;
    public GameObject LeftDoor;
    public GameObject RightDoor;
    private Vector3 leftDoorOriginalPos;
    private Vector3 rightDoorOriginalPos;

    private void Start()
    {
        leftDoorOriginalPos = LeftDoor.transform.position;
        rightDoorOriginalPos = RightDoor.transform.position;
    }

    public void DoorOpen()
    {
        if (HasKeycard(Keycard))
        {
            StartCoroutine(OpenAndCloseDoor());
        }
        else
        {
            Debug.Log("열쇠 카드가 필요합니다!");
        }
    }

    private IEnumerator OpenAndCloseDoor()
    {
        Vector3 leftDoorTarget = leftDoorOriginalPos + new Vector3(-1.5f, 0, 0); 
        Vector3 rightDoorTarget = rightDoorOriginalPos + new Vector3(1.5f, 0, 0); 

        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            LeftDoor.transform.position = Vector3.Lerp(leftDoorOriginalPos, leftDoorTarget, elapsedTime / duration);
            RightDoor.transform.position = Vector3.Lerp(rightDoorOriginalPos, rightDoorTarget, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        LeftDoor.transform.position = leftDoorTarget;
        RightDoor.transform.position = rightDoorTarget;

        yield return new WaitForSeconds(5f); 

        elapsedTime = 0f;

       
        while (elapsedTime < duration)
        {
            LeftDoor.transform.position = Vector3.Lerp(leftDoorTarget, leftDoorOriginalPos, elapsedTime / duration);
            RightDoor.transform.position = Vector3.Lerp(rightDoorTarget, rightDoorOriginalPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    
        LeftDoor.transform.position = leftDoorOriginalPos;
        RightDoor.transform.position = rightDoorOriginalPos;
    }

    public bool HasKeycard(Keycard keycard)
    {
        return CharacterManager.Instance.Player.keycard[(int)keycard];
    }
}
