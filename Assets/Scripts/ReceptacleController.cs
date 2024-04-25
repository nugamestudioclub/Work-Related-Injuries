using UnityEngine;

public class ReceptacleController : MonoBehaviour
{
    [SerializeField]
    private BoxOrientationManager.BoxOrientation acceptedOrientation;

    public Sprite[] orientation;

    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        switch (acceptedOrientation)
        {
            case BoxOrientationManager.BoxOrientation.Up:
                render.sprite = orientation[0];
                break;
            case BoxOrientationManager.BoxOrientation.Down:
                render.sprite = orientation[1];
                break;
            case BoxOrientationManager.BoxOrientation.Left:
                render.sprite = orientation[2];
                break;
            case BoxOrientationManager.BoxOrientation.Right:
                render.sprite = orientation[3];
                break;
            default:
                render.sprite = orientation[0];
                break;

        }
    }

    public bool ProcessBox(GameObject box)
    {
        if (box.TryGetComponent<BoxOrientationManager>(out var component))
        {
            return component.IsOfOrientation(acceptedOrientation);
        }

        return false;
    }
}
