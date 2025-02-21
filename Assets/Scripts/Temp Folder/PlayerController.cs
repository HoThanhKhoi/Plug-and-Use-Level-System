using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			LevelUp();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			ResetPlayerSystem();
		}

	}

    void LevelUp()
	{
		LevelingService.Instance.AddExperience(LevelingCategory.Player, 2);
	}

	public void ResetPlayerSystem()
	{
		LevelingService.Instance.ResetSystem(LevelingCategory.Player);
	}

}
