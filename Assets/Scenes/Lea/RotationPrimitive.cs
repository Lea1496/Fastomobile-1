using UnityEngine;



/// <summary>
/// Code provenant de la PFI de la session dernière
/// </summary>
public class RotationPrimitive : MonoBehaviour
{
   [SerializeField]
   Vector3 VecteurRotation;

   bool RotationActivée { get; set; } = false;

   void Awake()
   {
      RotationActivée = true;
   }

   public void CommuterActivationRotation()
   {
      RotationActivée = !RotationActivée;
   }

   void Update()
   {
      if (RotationActivée)
      {
         transform.Rotate(VecteurRotation * Time.deltaTime);
      }
   }
}
