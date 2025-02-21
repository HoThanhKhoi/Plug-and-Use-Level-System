using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-30)]
public class FloatingLevelUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private bool billboard = true;

	private ILevelTracker levelProgression;
	private Transform mainCamera;

	private void Start()
	{
		mainCamera = Camera.main.transform;

		levelProgression = GetComponentInParent<ILevelTracker>();
		// or search children if needed
		if (levelProgression == null)
		{
			Debug.LogError("No LevelTracker found on this enemy!");
			return;
		}
		levelProgression.onLevelChanged += UpdateLevelText;
		UpdateLevelText();
	}

	private void OnDestroy()
	{
		if (levelProgression != null)
			levelProgression.onLevelChanged -= UpdateLevelText;
	}

	private void Update()
	{
		// Optional: Billboard so it always faces the camera
		if (billboard && mainCamera != null)
		{
			transform.LookAt(transform.position + mainCamera.forward);
		}
	}

	private void UpdateLevelText()
	{
		levelText.text = $"Lv. {levelProgression.Level}";
	}
}
