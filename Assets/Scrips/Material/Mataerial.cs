using UnityEngine;

public class CreatePhysicMaterial : MonoBehaviour
{
    void Start()
    {
        PhysicMaterial lowFrictionMaterial = new PhysicMaterial("LowFriction");

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
