using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotData : MonoBehaviour
{
    public Animator RobotAnimator;

    public GameObject ControlBox;
    public Vector3 ControlBoxEndPosition;
    public Vector3 ControlBoxStartPosition;

    public GameObject PowerButton;
    public Vector3 PowerButtonEndPosition;
    public Vector3 PowerButtonStartPosition;

    public GameObject TopAssembly;
    public Vector3 TopAssemblyEndPosition;
    public Vector3 TopAssemblyStartPosition;

    public GameObject LeftScrew;
    public Vector3 LeftScrewEndPosition;
    public Vector3 LeftScrewStartPosition;

    public GameObject RightScrew;
    public Vector3 RightScrewEndPosition;
    public Vector3 RightScrewStartPosition;


    public GameObject connector1;
    public Vector3 Connector1EndPosition;
    public Vector3 Connector1StartPosition;

    public GameObject connector2;
    public Vector3 Connector2EndPosition;
    public Vector3 Connector2StartPosition;

    public Material highlightedConnector;

    private void Start()
    {
       // ConfiguratorController.instance.TaskPanel.SetActive(true);
        TopAssemblyStartPosition = TopAssembly.transform.localPosition;
        TopAssemblyEndPosition.y = TopAssembly.transform.localPosition.y + 3f;



        PowerButtonStartPosition = PowerButton.transform.localPosition;

        ControlBoxStartPosition = ControlBox.transform.localPosition;
        ControlBoxEndPosition.y = ControlBox.transform.localPosition.y + 2f;

        if (RobotAnimator != null)
        {
            AnimationController.instance.animator = RobotAnimator;
        }
        if (connector1 != null)
        {
            AnimationController.instance.Connector1EndPosition = Connector1EndPosition;
            AnimationController.instance.Connector1StartPosition = Connector1StartPosition;
        }
        if (connector2 != null)
        {
            AnimationController.instance.Connector2EndPosition = Connector2EndPosition;
            AnimationController.instance.Connector2StartPosition = Connector2StartPosition;
        }

        if (TopAssembly != null)
        {
            AnimationController.instance.TopAssembly = TopAssembly;
            AnimationController.instance.TopAssemblyStartPos = TopAssemblyStartPosition;
            AnimationController.instance.TopAssemblyEndPos = TopAssemblyEndPosition;
        }


        if (LeftScrew != null)
        {
            AnimationController.instance.LeftScwer = LeftScrew;
            AnimationController.instance.LeftScwerEndPos = LeftScrewEndPosition;
            AnimationController.instance.LeftScwerStartPos = LeftScrewStartPosition;
        }
        if (RightScrew != null)
        {
            AnimationController.instance.RightScrew = RightScrew;
            AnimationController.instance.RightScwerEndPos = RightScrewEndPosition;
            AnimationController.instance.RightScwerStartPos = RightScrewStartPosition;
        }
        if (PowerButton != null)
        {
            AnimationController.instance.PowerButton = PowerButton;
            AnimationController.instance.PowerButtonEndPosition = PowerButtonEndPosition;
            AnimationController.instance.PowerButtonStartPosition = PowerButtonStartPosition;
        }
        if (highlightedConnector != null)
        {
            AnimationController.instance.highlightedConnector = highlightedConnector;
        }
        if (connector1 != null && connector2 != null)
        {
            AnimationController.instance.connector1 = connector1;
            AnimationController.instance.connector2 = connector2;
        }
        if (ControlBox != null)
        {
            AnimationController.instance.ControlBox = ControlBox;
            AnimationController.instance.ControlBoxStartPosition = ControlBoxStartPosition;
            AnimationController.instance.ControlBoxEndPosition = ControlBoxEndPosition;
        }
    }

}
