using UnityEngine;

public class PhysicMaterial : MonoBehaviour
{
    /// <summary>
    /// 플레이어의 마찰력을 줄여서 벽에서 안떨어지는것 방지
    /// </summary>
    void Start()
    {
        UnityEngine.PhysicMaterial lowFrictionMaterial = new UnityEngine.PhysicMaterial("LowFriction");

        lowFrictionMaterial.dynamicFriction = 0f;
        lowFrictionMaterial.staticFriction = 0f;
        lowFrictionMaterial.frictionCombine = PhysicMaterialCombine.Minimum;


        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.material = lowFrictionMaterial;
        }
    }
}
