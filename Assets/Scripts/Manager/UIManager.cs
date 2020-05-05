using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text goldText;
    [SerializeField]
    Text stepText;
    [SerializeField]
    Text levelText;

    [SerializeField]
    Animator cameraAnimator;

    public void UpdateGold() => goldText.text = ResourceManager.Instance.gold.ToString();

    public void UpdateStep() => stepText.text = string.Format(ResourceManager.Instance.step.ToString() + "/" + ResourceManager.Instance.maxStep.ToString());

    public void UpdateLevel() => levelText.text = ResourceManager.Instance.level.ToString();

    public void MoveBattlePanel()
    {

    }

    public void SetCameraBattleMode() => cameraAnimator.SetBool("isBattle", true);

    public void SetCameraReadyMode() => cameraAnimator.SetBool("isBattle", false);
}
