using UnityEngine;
using System.Collections;

public class Hazard : Ball 
{
	public float sizeVariance;

	void Start()
	{
		transform.localScale = Vector3.one*Mathf.Clamp(Random.Range(transform.localScale.x-sizeVariance,transform.localScale.x+sizeVariance),0.25f,10f);
	}

    void OnDestroy()
    {
		if (!GameObject.FindObjectOfType<ScoreCenter> ().currentlyBadBonus) {
			GameObject.FindObjectOfType<SpawnMonsterFakes> ().AddGreenEnemy ();
			GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().GreensDodged++;
			GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().m_bStoreStats = true;
		}
    }
}
