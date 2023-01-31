using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistane;
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteractable curIntearctable;
    public GameObject objectTextInteractable;

    public TextMeshProUGUI promptText;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        objectTextInteractable.SetActive(false);
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxCheckDistane, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curIntearctable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curIntearctable = null;
                objectTextInteractable.SetActive(false);
            }
        }
    }

    void SetPromptText()
    {
        objectTextInteractable.SetActive(true);
        promptText.text = string.Format("<b>[E]</b> {0}", curIntearctable.GetInteractPrompt());
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curIntearctable != null)
        {
            curIntearctable.OnInteract();
            curInteractGameObject = null;
            curIntearctable = null;
            objectTextInteractable.gameObject.SetActive(false);
        }

    }
}

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}
