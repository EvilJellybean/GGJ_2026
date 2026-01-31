using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMask : MonoBehaviour
{
    private static readonly Plane plane = new Plane(Vector3.up, Vector3.zero);

    [SerializeField]
    private InputActionReference toggleMaskInput;

    [SerializeField]
    private GameObject mask;
    [SerializeField]
    private Transform noMaskLight;
    [SerializeField]
    private Transform maskLight;

    private Camera camera;

    public bool MaskMode { get; private set; } = false;

    private void Awake()
    {
        camera = Camera.main;
        mask.SetActive(false);
        noMaskLight.gameObject.SetActive(true);
        maskLight.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        toggleMaskInput.action.Enable();
    }

    private void Update()
    {
        if(toggleMaskInput.action.WasPressedThisFrame())
        {
            MaskMode = !MaskMode;
            mask.SetActive(MaskMode);
            noMaskLight.gameObject.SetActive(!MaskMode);
            maskLight.gameObject.SetActive(MaskMode);
        }

        if (MaskMode)
        {
            Vector3 aimPosition = GetMouseWorldPosition();
            Vector3 offset = aimPosition - transform.position;
            offset.y = 0;

            maskLight.rotation = Quaternion.LookRotation(offset);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector2 mousePosition = Mouse.current.position.value;
        Ray mouseRay = camera.ScreenPointToRay(mousePosition);
        if (plane.Raycast(mouseRay, out float distance))
        {
            Vector3 worldPosition = mouseRay.GetPoint(distance);
            return worldPosition;
        }
        return Vector3.zero;
    }
}
