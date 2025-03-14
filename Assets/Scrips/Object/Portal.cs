using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Player player;
    public ParticleSystem myParticleSystem;

    private bool isCoroutineRunning = false;
    private void Start()
    {
        player = CharacterManager.Instance.Player;
    }
    /// <summary>
    /// 플레이어가 부딫히면 메서드 실행
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& !isCoroutineRunning)
        {
            Debug.Log("플레이어 부딪침");
            StartCoroutine(PlayerGoStart());
        }
    }
    /// <summary>
    /// 초기장소로 돌아가는 연출
    /// 플레이어는 정지하고 파티클이 많아진다
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerGoStart()
    {
        isCoroutineRunning = true;

        ParticleSystem.EmissionModule emission = myParticleSystem.emission;
        ParticleSystem.MainModule main = myParticleSystem.main;

        float initialEmissionRate = emission.rateOverTime.constant;
        float initialStartSpeed = main.startSpeed.constant;

        float targetEmissionRate = 1000f;
        float targetStartSpeed = 3f;
        float duration = 2f; 
        float elapsedTime = 0f;
        player.controller.Dontmove();
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            float currentEmissionRate = Mathf.Lerp(initialEmissionRate, targetEmissionRate, t);
            float currentStartSpeed = Mathf.Lerp(initialStartSpeed, targetStartSpeed, t);

            emission.rateOverTime = currentEmissionRate;
            main.startSpeed = currentStartSpeed;

            yield return null;
        }

        emission.rateOverTime = targetEmissionRate;
        main.startSpeed = targetStartSpeed;

        player.gameObject.transform.position = Vector3.zero;
        player.controller.Canmove();
        isCoroutineRunning = false;
    }
}
