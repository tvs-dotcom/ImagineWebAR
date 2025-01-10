using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguratorController : MonoBehaviour
{
    public static ConfiguratorController instance { get; private set; }
    public GameObject TaskPanel;
    public enum TaskButton
    {
        Task1,
        Task2,
        Task3,
        Task4,
        Task5,
        Task6,
        Task7,
        Task8,
        Task9,
        Task10,
        Task11,
        Task12
    }

    public Button resetButton;
    public GameObject ARFloor;

    [System.Serializable]
    public class ButtonMapping
    {
        public AudioClip clip;
        public Button button;               // Reference to the UI Button
        public TaskButton taskbuttonAction; // Action assigned to the button
    }

    [Header("Button List")]
    public List<ButtonMapping> buttonMappings = new List<ButtonMapping>();

    private int currentIndex = 0;

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

    private void Start()
    {
        if (ARFloor != null)
        {
            ARFloor.SetActive(true);
        }
        resetButton.onClick.AddListener(EnableTasksFromStart);
        // Add listeners to each button
        for (int i = 0; i < buttonMappings.Count; i++)
        {
            if (buttonMappings[i].button != null)
            {
                int index = i; // Use a local copy of the index to avoid closure issues
                buttonMappings[i].button.onClick.AddListener(() => OnButtonClicked(index));
            }
            else
            {
                Debug.LogWarning($"Button at index {i} is not assigned!");
            }
        }

        // Initialize the button states (only the first button is enabled)
        UpdateButtonStates();

        //hide all tash buttons
        HideAllButtons();
    }

    private void Update()
    {
        if (ARFloor == null)
        {
            ARFloor = GameObject.Find("Trackables");
        }
    }

    private void HideAllButtons()
    {
        for (int i = 0; i < buttonMappings.Count; i++)
        {
            if (i == 0)
            {
                buttonMappings[i].button.gameObject.SetActive(true);
                // Ensure the reset button is active
                if (resetButton != null)
                {
                    resetButton.gameObject.SetActive(false);
                }
            }
            else
            {
                buttonMappings[i].button.gameObject.SetActive(false);
            }
        }
    }

    public void OnButtonClicked(int index)
    {

        StartCoroutine(ButtonOnAfterDelay(index));
    }

    IEnumerator ButtonOnAfterDelay(int index)
    {
        // Perform the action associated with the clicked button
        PerformAction(buttonMappings[index].taskbuttonAction);
        PlaySound(index);
        buttonMappings[index].button.gameObject.SetActive(false);
        // Disable the current button and enable the next button
        currentIndex = (index + 1);
        yield return new WaitForSeconds(4f);
        AnimationController.instance.fullText = "";
        AnimationController.instance.StopTypewriterEffect();

        if (currentIndex < buttonMappings.Count)
        {
            buttonMappings[currentIndex].button.gameObject.SetActive(true);
        }

        // Check if all tasks have been completed
        if (currentIndex >= buttonMappings.Count)
        {
            Debug.Log("All tasks have been completed!");
            // Ensure the reset button is always active
            yield return new WaitForSeconds(5f);
            resetButton.gameObject.SetActive(true);
            HandleAllTasksCompleted();
        }
        else
        {
            buttonMappings[currentIndex].button.gameObject.SetActive(true);
            UpdateButtonStates();
        }
    }
    private void UpdateButtonStates()
    {
        for (int i = 0; i < buttonMappings.Count; i++)
        {
            if (buttonMappings[i].button != null)
            {
                buttonMappings[i].button.interactable = (i == currentIndex);
            }
        }
    }

    void PlaySound(int index)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (buttonMappings[index].clip != null && audioSource != null)
        {
            audioSource.clip = buttonMappings[index].clip;
            audioSource.Play();
        }
    }

    private void HandleAllTasksCompleted()
    {
        // Disable all task buttons
        foreach (var mapping in buttonMappings)
        {
            if (mapping.button != null)
            {
                mapping.button.interactable = false;
            }
        }

        Debug.Log("All tasks completed. Only the reset button is active.");
    }

    public void EnableTasksFromStart()
    {
        currentIndex = 0; // Reset to the first task
        HideAllButtons();
        // Enable Task 1 and disable others
        for (int i = 0; i < buttonMappings.Count; i++)
        {
            if (buttonMappings[i].button != null)
            {
                buttonMappings[i].button.interactable = (i == 0); // Only Task 1 enabled
            }
        }

        Debug.Log("Tasks have been reset and Task 1 is now active.");
    }

    private void PerformAction(TaskButton action)
    {
        // Perform the action based on the button's enum value
        switch (action)
        {
            case TaskButton.Task1:
                Debug.Log("Task 1 performed!");
                PerformTaskOne();
                break;
            case TaskButton.Task2:
                Debug.Log("Action 2 performed!");
                StartCoroutine(Perform2ndTask());
                break;
            case TaskButton.Task3:
                Debug.Log("Action 3 performed!");
                Perform3rdTask();
                break;
            case TaskButton.Task4:
                Debug.Log("Action 4 performed!");
                StartCoroutine(Perform4thTask());
                break;
            case TaskButton.Task5:
                Debug.Log("Action 5 performed!");
                PerformTask5();
                break;
            case TaskButton.Task6:
                Debug.Log("Action 6 performed!");
                PerformTask6();
                break;
            case TaskButton.Task7:
                Debug.Log("Action 7 performed!");
                StartCoroutine(PerformTask7());
                break;
            case TaskButton.Task8:
                Debug.Log("Action 8 performed!");
                PerformTask8();
                break;
            case TaskButton.Task9:
                Debug.Log("Action 9 performed!");
                PerformTask9();
                break;
            case TaskButton.Task10:
                Debug.Log("Action 10 performed!");
                PerformTask10();
                break;
            case TaskButton.Task11:
                Debug.Log("Action 11 performed!");
                PerformTask11();
                break;
            case TaskButton.Task12:
                Debug.Log("Action 12 performed!");
                PerformTask12();
                break;
        }
    }

    void PerformTaskOne()
    {
        AnimationController.instance.PlayFirstTaskAnimation();
        AnimationController.instance.WriteText("Robot Lit 4.5\" Up\r\n");
    }

    IEnumerator Perform2ndTask()
    {
        AnimationController.instance.WriteText("Shutting down the robot.");
        AnimationController.instance.ObjectMove("PowerButton", "forward");
        yield return new WaitForSeconds(2f);
        AnimationController.instance.ObjectMove("PowerButton", "backward");
    }

    void Perform3rdTask()
    {
        AnimationController.instance.WriteText("Removing the top assembly.");
        AnimationController.instance.ObjectMove("MoveTopAssemblyToEndPoint", "forward");

    }

    IEnumerator Perform4thTask()
    {
        AnimationController.instance.WriteText("Connector highlighted.");
        AnimationController.instance.highlightMaterial(Color.red);
        yield return new WaitForSeconds(2f);
        AnimationController.instance.ResetColor();

    }

    void PerformTask5()
    {
        AnimationController.instance.WriteText("Disconnecting plugs.");
        AnimationController.instance.ObjectMove("MoveConnectorToEndPoint", "forward");

    }

    void PerformTask6()
    {
        AnimationController.instance.WriteText("Removing 4 Allen screws.");
        AnimationController.instance.ObjectMove("RemoveScrew", "forward");
    }

    IEnumerator PerformTask7()
    {
        AnimationController.instance.WriteText("Sliding the control box out");
        AnimationController.instance.ObjectMove("ControlBox", "forward");
        yield return new WaitForSeconds(2f);
        AnimationController.instance.FadeOutAll("controlBox");
    }

    void PerformTask8()
    {
        AnimationController.instance.WriteText("Inserting the new control box.");
        AnimationController.instance.FadeInAll("controlBox");
        AnimationController.instance.ObjectMove("ControlBox", "backward");
    }
    void PerformTask9()
    {
        AnimationController.instance.WriteText("Reinstalling 4 Allen screws.");
        AnimationController.instance.ObjectMove("RemoveScrew", "backward");
    }
    void PerformTask10()
    {
        AnimationController.instance.WriteText("Reinstalling the top assembly.");
        AnimationController.instance.ObjectMove("MoveTopAssemblyToEndPoint", "backward");
    }
    void PerformTask11()
    {
        AnimationController.instance.WriteText("Reconnecting plugs");
        AnimationController.instance.ObjectMove("MoveConnectorToEndPoint", "backward");
    }
    void PerformTask12()
    {
        AnimationController.instance.WriteText("Turning robot on");
        StartCoroutine(AnimationController.instance.PlayLastTaskAnimation());
    }

    private void OnDestroy()
    {
        // Remove listeners to avoid memory leaks
        foreach (var mapping in buttonMappings)
        {
            if (mapping.button != null)
            {
                mapping.button.onClick.RemoveAllListeners();
            }
        }

        resetButton.onClick.RemoveAllListeners();
    }
}
