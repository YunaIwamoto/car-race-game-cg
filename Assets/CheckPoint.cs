using UnityEngine;
using TMPro;
public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "car1" )
        {
            UIManager.Instance.AddCount(1);
        }
    }


}
