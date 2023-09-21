using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARPlacementInteractableBlockedByUI : ARPlacementInteractable
{
    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.startPosition.IsPointerOverGameObject())
            return false;

        return gesture.targetObject == null;
    }
}
