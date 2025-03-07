using UnityEngine;

public class PhysicMaterial : MonoBehaviour
{
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
