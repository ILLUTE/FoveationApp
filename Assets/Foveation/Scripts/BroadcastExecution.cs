using UnityEngine;

public class BroadcastExecution : MonoBehaviour
{
    private void Start()
    {
        FoveationController.Instance.UpdateFoveation(1.0f);
    }
}
