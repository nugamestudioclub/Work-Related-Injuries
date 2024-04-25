using UnityEngine;

public class ReceptacleController : MonoBehaviour
{
    [SerializeField]
    private BoxOrientationManager.BoxOrientation acceptedOrientation;

    public bool ProcessBox(GameObject box)
    {
        if (box.TryGetComponent<BoxOrientationManager>(out var component))
        {
            return component.IsOfOrientation(acceptedOrientation);
        }

        return false;
    }
}
