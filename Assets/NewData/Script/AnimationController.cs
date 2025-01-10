using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController instance { get; private set; }
    public Animator animator;

    public GameObject TopAssembly;
    public Vector3 TopAssemblyEndPos;
    public Vector3 TopAssemblyStartPos;
    public float lerpDuration = 2f;

    public float tweenDuration = 2f;
    public Color ConnectorDefaultColor;
    public Material highlightedConnector;

    private Coroutine colorTweenCoroutine;
    bool isColorGet = false;

    public GameObject connector1;
    public Vector3 Connector1EndPosition;
    public Vector3 Connector1StartPosition;

    public GameObject connector2;
    public Vector3 Connector2EndPosition;
    public Vector3 Connector2StartPosition;

    public float fadeDuration = 2f;

    public GameObject LeftScwer;
    public Vector3 LeftScwerEndPos;
    public Vector3 LeftScwerStartPos;

    public GameObject RightScrew;
    public Vector3 RightScwerEndPos;
    public Vector3 RightScwerStartPos;

    public GameObject PowerButton;
    public Vector3 PowerButtonEndPosition;
    public Vector3 PowerButtonStartPosition;

    public GameObject ControlBox;
    public Vector3 ControlBoxEndPosition;
    public Vector3 ControlBoxStartPosition;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFirstTaskAnimation()
    {
        if (animator != null)
        {
            Debug.Log("Play First Task Animation");
            animator.SetBool("Task1Animation", true);
            animator.SetBool("LastTaskAnimation", false);
        }
    }

    // Method to play Last Task Animation
    public IEnumerator PlayLastTaskAnimation()
    {
        if (animator != null)
        {
            Debug.Log("Play Last Task Animation");
            animator.SetBool("LastTaskAnimation", true);
            animator.SetBool("Task1Animation", false);
            yield return new WaitForSeconds(2f);
            WriteText("Testing operation");
        }
    }

    public void ObjectMove(string task, string moveDirection)
    {
        switch (task)
        {

            case "MoveTopAssemblyToEndPoint":
                if (moveDirection.Equals("forward"))
                {
                    StartCoroutine(LerpPosition(TopAssembly, TopAssemblyStartPos, TopAssemblyEndPos, lerpDuration));
                }
                else
                {
                    StartCoroutine(LerpPosition(TopAssembly, TopAssemblyEndPos, TopAssemblyStartPos, lerpDuration));
                }
                break;
            case "PowerButton":
                if (moveDirection.Equals("forward"))
                {
                    StartCoroutine(LerpPosition(PowerButton, PowerButtonStartPosition, PowerButtonEndPosition, lerpDuration));
                }
                else
                {
                    StartCoroutine(LerpPosition(PowerButton, PowerButtonEndPosition, PowerButtonStartPosition, lerpDuration));
                }
                break;
            case "ControlBox":
                if (moveDirection.Equals("forward"))
                {
                    StartCoroutine(LerpPosition(ControlBox, ControlBoxStartPosition, ControlBoxEndPosition, lerpDuration));
                }
                else
                {
                    StartCoroutine(LerpPosition(ControlBox, ControlBoxEndPosition, ControlBoxStartPosition, lerpDuration));
                }
                break;
            case "MoveConnectorToEndPoint":
                if (moveDirection.Equals("forward"))
                {
                    StartCoroutine(LerpPosition(connector1, Connector1StartPosition, Connector1EndPosition, lerpDuration));
                    StartCoroutine(LerpPosition(connector2, Connector2StartPosition, Connector2EndPosition, lerpDuration));
                }
                else
                {
                    StartCoroutine(LerpPosition(connector1, Connector1EndPosition, Connector1StartPosition, lerpDuration));
                    StartCoroutine(LerpPosition(connector2, Connector2EndPosition, Connector2StartPosition, lerpDuration));
                }
                break;
            case "RemoveScrew":
                if (moveDirection.Equals("forward"))
                {
                    StartCoroutine(LerpPosition(LeftScwer, LeftScwerStartPos, LeftScwerEndPos, lerpDuration));
                    StartCoroutine(LerpPosition(RightScrew, RightScwerStartPos, RightScwerEndPos, lerpDuration));
                }
                else
                {
                    StartCoroutine(LerpPosition(LeftScwer, LeftScwerEndPos, LeftScwerStartPos, lerpDuration));
                    StartCoroutine(LerpPosition(RightScrew, RightScwerEndPos, RightScwerStartPos, lerpDuration));
                }
                break;
            default:
                break;
        }
    }

    public IEnumerator LerpPosition(GameObject obj, Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            obj.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localPosition = end;
        Debug.Log("Lerp Complete!");
    }

    public void ResetColor()
    {
        highlightMaterial(ConnectorDefaultColor);
    }

    public void highlightMaterial(Color clr)
    {
        if (colorTweenCoroutine != null)
        {
            StopCoroutine(colorTweenCoroutine); // Stop the current coroutine if it's already running
        }
        if (isColorGet)
        {
            ConnectorDefaultColor = highlightedConnector.color;
            isColorGet = false;
        }
        colorTweenCoroutine = StartCoroutine(TweenMaterialColor(highlightedConnector, highlightedConnector.color, clr, tweenDuration));
    }

    private IEnumerator TweenMaterialColor(Material material, Color fromColor, Color toColor, float duration)
    {
        if (material == null)
        {
            Debug.LogWarning("Target material is not assigned.");
            yield break;
        }

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Normalize elapsed time
            material.color = Color.Lerp(fromColor, toColor, t); // Update material color
            yield return null;
        }

        // Ensure the final color is set
        material.color = toColor;
        Debug.Log("Material color tween complete.");
    }

    // Method to fade out all objects in the list
    public void FadeOutAll(string objName)
    {
        if (objName.Equals("controlBox"))
        {
            if (ControlBox != null)
            {
                StartCoroutine(FadeObject(ControlBox.transform.GetChild(0).gameObject, 1f, 0f));
            }
        }
    }

    public void FadeInAll(string objName)
    {
        if (objName.Equals("controlBox"))
        {
            if (ControlBox != null)
            {
                StartCoroutine(FadeInObject(ControlBox.transform.GetChild(0).gameObject, 2f));
            }
        }
    }

    // Method to fade in all objects in the list
    public void FadeInAll(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            StartCoroutine(FadeObject(obj, 0f, 1f));
        }
    }

    private IEnumerator FadeObject(GameObject targetObject, float startAlpha, float endAlpha)
    {
        Renderer objectRenderer = targetObject.GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogWarning($"Object {targetObject.name} does not have a Renderer component.");
            yield break;
        }

        Material objectMaterial = objectRenderer.material;
        if (!objectMaterial.HasProperty("_Color"))
        {
            Debug.LogWarning($"Material on {targetObject.name} does not have a '_Color' property.");
            yield break;
        }

        Color color = objectMaterial.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            objectMaterial.color = color;
            yield return null;
        }

        // Ensure the final alpha is set
        color.a = endAlpha;
        objectMaterial.color = color;

        // Optional: Disable the object if fully transparent
        if (Mathf.Approximately(endAlpha, 0f))
        {
            targetObject.SetActive(false);
        }
    }

    private IEnumerator FadeInObject(GameObject targetObject, float fadeDuration)
    {
        Renderer objectRenderer = targetObject.GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogWarning($"Object {targetObject.name} does not have a Renderer component.");
            yield break;
        }

        Material objectMaterial = objectRenderer.material;
        if (!objectMaterial.HasProperty("_Color"))
        {
            Debug.LogWarning($"Material on {targetObject.name} does not have a '_Color' property.");
            yield break;
        }

        // Ensure the object is active
        targetObject.SetActive(true);

        Color color = objectMaterial.color;
        float startAlpha = 0f;
        float endAlpha = 1f;
        float elapsedTime = 0f;

        // Initialize with start alpha
        color.a = startAlpha;
        objectMaterial.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            objectMaterial.color = color;
            yield return null;
        }

        // Ensure the final alpha is set
        color.a = endAlpha;
        objectMaterial.color = color;
    }



    public void StopAnimations()
    {
        animator.SetBool("PlayAnimation1", false);
        animator.SetBool("PlayAnimation2", false);
        Debug.Log("Stopped all animations");
    }

    public void SetNullAllRef()
    {
        animator = null;
        TopAssembly = null;
        highlightedConnector = null;
        connector1 = null;
        connector2 = null;
        LeftScwer = null;
        RightScrew = null;
        PowerButton = null;
        ControlBox = null;
    }



    [Header("Text Settings")]
    public TMP_Text targetText;
    public GameObject parentObject;
    [TextArea]
    public string fullText;    // Full text to display

    [Header("Typewriter Settings")]
    public float typeSpeed = 0.05f; // Time delay between characters

    [Header("Background Settings")]
    public RectTransform background; // Reference to a background object
    public Vector2 padding = new Vector2(10f, 10f); // Padding around the text
    public Color backgroundColor = Color.black; // Background color

    private Coroutine typingCoroutine;

    public void WriteText(string txt)
    {
        fullText = txt;

        // Optionally start the effect automatically
        StartTypewriterEffect();
    }

    public void StartTypewriterEffect()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText());
    }

    public void StopTypewriterEffect()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // Ensure all text is displayed if stopped prematurely
        targetText.text = fullText;
    }

    private IEnumerator TypeText()
    {
        targetText.text = ""; // Clear existing text

        Color highlightColor = Color.yellow; // Highlight color
        Color markColor = Color.cyan; // Mark color (for example, blue)
        Color currentHighlightColor = highlightColor; // Permanent highlight color
        Color currentMarkColor = markColor; // Permanent mark color

        for (int i = 0; i <= fullText.Length; i++)
        {
            string textToShow = fullText.Substring(0, i);

            // Apply the highlight and mark (underline, bold, or background color)
            if (i > 0)
            {
                textToShow = textToShow.Substring(0, i - 1) +
                             $"<color=#FFFFFF>" + // White color for the text
                             $"<mark=#{ColorUtility.ToHtmlStringRGB(currentMarkColor)}>" +
                             fullText[i - 1] + "</mark></color>";
            }


            // Update the text with the highlighted and marked version
            targetText.text = textToShow;

            // Wait for the next character to appear
            yield return new WaitForSeconds(typeSpeed);
        }

        typingCoroutine = null; // Mark coroutine as finished
    }





}