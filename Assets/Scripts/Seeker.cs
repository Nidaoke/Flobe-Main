using UnityEngine;
using System.Collections;

public class Seeker : Ball
{
	void Start() 
	{
		//dir = (GameController.instance.lineScr.transform.position - transform.position).normalized;

		transform.localScale = Vector3.one*Mathf.Clamp(Random.Range(transform.localScale.x,transform.localScale.x),0.25f,10f);

		transform.up = dir;
	}

    void OnDestroy()
    {
		if (!GameObject.FindObjectOfType<ScoreCenter> ().currentlyBadBonus) {
			GameObject.FindObjectOfType<SpawnMonsterFakes> ().AddRedEnemy ();
			GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().MissilesDodged++;
			GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().m_bStoreStats = true;
		}
    }

    ////void MoveSpeedInc(float amt)
    //{
    //	moveSpeed += amt;
    //}
}
