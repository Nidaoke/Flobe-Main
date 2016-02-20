using UnityEngine;
using System.Collections;
using System.ComponentModel;
using Steamworks;

public class SteamStatsAndAchievements : MonoBehaviour
{

    public Achievement_t[] m_Achievements = new Achievement_t[] {
        new Achievement_t(Achievement.ACH_100_BLUES, "ACH_100_BLUES", "a"),
        new Achievement_t(Achievement.ACH_300_BLUES, "ACH_300_BLUES", "a"),
        new Achievement_t(Achievement.ACH_500_BLUES, "ACH_500_BLUES", "a"),

        new Achievement_t(Achievement.ACH_100_PURPLES, "ACH_100_PURPLES", "a"),
        new Achievement_t(Achievement.ACH_300_PURPLES, "ACH_300_PURPLES", "a"),
        new Achievement_t(Achievement.ACH_500_PURPLES, "ACH_500_PURPLES", "a"),

        new Achievement_t(Achievement.ACH_100_GREENS, "ACH_100_GREENS", "aa"),
        new Achievement_t(Achievement.ACH_300_GREENS, "ACH_300_GREENS", "a"),
        new Achievement_t(Achievement.ACH_500_GREENS, "ACH_500_GREENS", "a"),

        new Achievement_t(Achievement.ACH_100_MISSILES, "ACH_100_MISSILES", "a"),
        new Achievement_t(Achievement.ACH_300_MISSILES, "ACH_300_MISSILES", "a"),
        new Achievement_t(Achievement.ACH_500_MISSILES, "ACH_500_MISSILES", "a"),

        new Achievement_t(Achievement.ACH_25_BOMBS, "ACH_25_BOMBS", "a"),
        new Achievement_t(Achievement.ACH_50_BOMBS, "ACH_50_BOMBS", "a"),
        new Achievement_t(Achievement.ACH_100_BOMBS, "ACH_100_BOMBS", "a"),

        new Achievement_t(Achievement.ACH_10_MULTIPLIER, "ACH_10_MULTIPLIER", "a"),
        new Achievement_t(Achievement.ACH_25_MULTIPLIER, "ACH_25_MULTIPLIER", "a"),
        new Achievement_t(Achievement.ACH_50_MULTIPLIER, "ACH_50_MULTIPLIER", "a"),

        new Achievement_t(Achievement.ACH_DOUBLE_TROUBLE, "ACH_DOUBLE_TROUBLE", "a"),
        //new Achievement_t(Achievement.ACH_TWISTED_FIRESTARTER, "ACH_TWISTED_FIRESTARTER", "a"),
        new Achievement_t(Achievement.ACH_SURVIVOR, "ACH_SURVIVOR", "a"),
        new Achievement_t(Achievement.ACH_DOUBLE_DIPPER, "ACH_DOUBLE_DIPPER", "a"),

    };

    public enum Achievement : int
    {
        ACH_100_BLUES,
        ACH_300_BLUES,
        ACH_500_BLUES,

        ACH_100_PURPLES,
        ACH_300_PURPLES,
        ACH_500_PURPLES,

        ACH_100_GREENS,
        ACH_300_GREENS,
        ACH_500_GREENS,

        ACH_100_MISSILES,
        ACH_300_MISSILES,
        ACH_500_MISSILES,

        ACH_25_BOMBS,
        ACH_50_BOMBS,
        ACH_100_BOMBS,

        ACH_10_MULTIPLIER,
        ACH_25_MULTIPLIER,
        ACH_50_MULTIPLIER,

        ACH_DOUBLE_TROUBLE,
        //ACH_TWISTED_FIRESTARTER,
        ACH_SURVIVOR,
        ACH_DOUBLE_DIPPER
    };

    public int BluesCollected;
    public int PurplesCollected;
    public int GreensDodged;
    public int MissilesDodged;
    public int BombsExploded;
    public int Multiplier;

    // Our GameID
    private CGameID m_GameID;

    // Did we get the stats from Steam?
    private bool m_bRequestedStats;
    private bool m_bStatsValid;

    // Should we store stats this frame?
    public bool m_bStoreStats;

    protected Callback<UserStatsReceived_t> m_UserStatsReceived;
    protected Callback<UserStatsStored_t> m_UserStatsStored;
    protected Callback<UserAchievementStored_t> m_UserAchievementStored;

    void OnEnable()
    {
        if (!SteamManager.Initialized)
            return;

        // Cache the GameID for use in the Callbacks
        m_GameID = new CGameID(SteamUtils.GetAppID());

        m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
        m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

        // These need to be reset to get the stats upon an Assembly reload in the Editor.
        m_bRequestedStats = false;
        m_bStatsValid = false;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {

            m_bStoreStats = true;
            
        }

        

        if (Input.GetKeyDown(KeyCode.P))
        {

            SteamUserStats.ResetAllStats(true);
            SteamUserStats.RequestCurrentStats();
        }*/

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
        {
            PurplesCollected = 0;
            BluesCollected = 0;
            GreensDodged = 0;
            Multiplier = 0;
            MissilesDodged = 0;
            BombsExploded = 0;
            SteamUserStats.ResetAllStats(true);
        }

        

        if (!SteamManager.Initialized)
            return;

        if (!m_bRequestedStats)
        {
            // Is Steam Loaded? if no, can't get stats, done
            if (!SteamManager.Initialized)
            {
                m_bRequestedStats = true;
                return;
            }

            // If yes, request our stats
            bool bSuccess = SteamUserStats.RequestCurrentStats();

            // This function should only return false if we weren't logged in, and we already checked that.
            // But handle it being false again anyway, just ask again later.
            m_bRequestedStats = bSuccess;
        }

        if (!m_bStatsValid)
            return;

        // Get info from sources

        // Evaluate achievements
        foreach (Achievement_t achievement in m_Achievements)
        {
            if (achievement.m_bAchieved)
                continue;

            switch (achievement.m_eAchievementID)
            {
                case Achievement.ACH_100_BLUES:
                    if (BluesCollected >= 100)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_300_BLUES:
                    if (BluesCollected >= 300)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_500_BLUES:
                    if (BluesCollected >= 500)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_100_PURPLES:
                    if (PurplesCollected >= 100)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_300_PURPLES:
                    if (PurplesCollected >= 300)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_500_PURPLES:
                    if (PurplesCollected >= 500)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_100_GREENS:
                    if (GreensDodged >= 100)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_300_GREENS:
                    if (GreensDodged >= 300)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_500_GREENS:
                    if (GreensDodged >= 500)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_100_MISSILES:
                    if (MissilesDodged >= 100)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_300_MISSILES:
                    if (MissilesDodged >= 300)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_500_MISSILES:
                    if (MissilesDodged >= 500)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_25_BOMBS:
                    if (BombsExploded >= 25)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_50_BOMBS:
                    if (BombsExploded >= 50)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_100_BOMBS:
                    if (BombsExploded >= 100)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_10_MULTIPLIER:
                    if (Multiplier >= 10)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_25_MULTIPLIER:
                    if (Multiplier >= 25)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_50_MULTIPLIER:
                    if (Multiplier >= 50)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;

            }
        }

        //Store stats in the Steam database if necessary
        if (m_bStoreStats)
        {
            // already set any achievements in UnlockAchievement

            // set stats
            SteamUserStats.SetStat("BluesCollected", BluesCollected);
            SteamUserStats.SetStat("PurplesCollected", PurplesCollected);
            SteamUserStats.SetStat("GreensDodged", GreensDodged);
            SteamUserStats.SetStat("MissilesDodged", MissilesDodged);
            SteamUserStats.SetStat("BombsExploded", BombsExploded);
            SteamUserStats.SetStat("Multiplier", Multiplier);

            bool bSuccess = SteamUserStats.StoreStats();
            // If this failed, we never sent anything to the server, try
            // again later.
            m_bStoreStats = !bSuccess;
        }
    }

    public void UnlockAchievement(Achievement_t achievement)
    {
        achievement.m_bAchieved = true;

        // the icon may change once it's unlocked
        //achievement.m_iIconImage = 0;

        // mark it down
        SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());

        // Store stats end of frame
        m_bStoreStats = true;
    }

    /*public void OnGameStateChange(EClientGameState eNewState)
    {
        if (!m_bStatsValid)
            return;

        if (eNewState == EClientGameState.k_EClientGameActive)
        {
            // Reset per-game stats
            m_flGameFeetTraveled = 0;
            m_ulTickCountGameStart = Time.time;
        }
        else if (eNewState == EClientGameState.k_EClientGameWinner || eNewState == EClientGameState.k_EClientGameLoser)
        {
            if (eNewState == EClientGameState.k_EClientGameWinner)
            {
                m_nTotalNumWins++;
            }
            else {
                m_nTotalNumLosses++;
            }

            // Tally games
            m_nTotalGamesPlayed++;

            // Accumulate distances
            m_flTotalFeetTraveled += m_flGameFeetTraveled;

            // New max?
            if (m_flGameFeetTraveled > m_flMaxFeetTraveled)
                m_flMaxFeetTraveled = m_flGameFeetTraveled;

            // Calc game duration
            m_flGameDurationSeconds = Time.time - m_ulTickCountGameStart;

            // We want to update stats the next frame.
            m_bStoreStats = true;
        }
    }*/

    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if (!SteamManager.Initialized)
            return;

        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("Received stats and achievements from Steam\n");

                m_bStatsValid = true;

                // load achievements
                foreach (Achievement_t ach in m_Achievements)
                {
                    bool ret = SteamUserStats.GetAchievement(ach.m_eAchievementID.ToString(), out ach.m_bAchieved);
                    if (ret)
                    {
                        ach.m_strName = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "name");
                        ach.m_strDescription = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "desc");
                    }
                    else {
                        Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + ach.m_eAchievementID + "\nIs it registered in the Steam Partner site?");
                    }
                }

                // load stats

                SteamUserStats.GetStat("BluesCollected", out BluesCollected);
                SteamUserStats.GetStat("PurplesCollected", out PurplesCollected);
                SteamUserStats.GetStat("GreensDodged", out GreensDodged);
                SteamUserStats.GetStat("MissilesDodged", out MissilesDodged);
                SteamUserStats.GetStat("BombsExploded", out BombsExploded);
                SteamUserStats.GetStat("Multiplier", out Multiplier);

                /*SteamUserStats.GetStat("NumGames", out m_nTotalGamesPlayed);
                SteamUserStats.GetStat("NumWins", out m_nTotalNumWins);
                SteamUserStats.GetStat("NumLosses", out m_nTotalNumLosses);
                SteamUserStats.GetStat("FeetTraveled", out m_flTotalFeetTraveled);
                SteamUserStats.GetStat("MaxFeetTraveled", out m_flMaxFeetTraveled);
                SteamUserStats.GetStat("AverageSpeed", out m_flAverageSpeed);*/
            }
            else {
                Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // Purpose: Our stats data was stored!
    //-----------------------------------------------------------------------------
    private void OnUserStatsStored(UserStatsStored_t pCallback)
    {
        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("StoreStats - success");
            }
            else if (EResult.k_EResultInvalidParam == pCallback.m_eResult)
            {
                // One or more stats we set broke a constraint. They've been reverted,
                // and we should re-iterate the values now to keep in sync.
                Debug.Log("StoreStats - some failed to validate");
                // Fake up a callback here so that we re-load the values.
                //UserStatsReceived_t callback = new UserStatsReceived_t();
                //callback.m_eResult = EResult.k_EResultOK;
                //callback.m_nGameID = (ulong)m_GameID;
                //OnUserStatsReceived(callback);
            }
            else {
                Debug.Log("StoreStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // Purpose: An achievement was stored
    //-----------------------------------------------------------------------------
    private void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        // We may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (0 == pCallback.m_nMaxProgress)
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
            }
            else {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
            }
        }
    }

    public class Achievement_t
    {
        public Achievement m_eAchievementID;
        public string m_strName;
        public string m_strDescription;
        public bool m_bAchieved;

        /// <summary>
        /// Creates an Achievement. You must also mirror the data provided here in https://partner.steamgames.com/apps/achievements/yourappid
        /// </summary>
        /// <param name="achievement">The "API Name Progress Stat" used to uniquely identify the achievement.</param>
        /// <param name="name">The "Display Name" that will be shown to players in game and on the Steam Community.</param>
        /// <param name="desc">The "Description" that will be shown to players in game and on the Steam Community.</param>
        public Achievement_t(Achievement achievementID, string name, string desc)
        {
            m_eAchievementID = achievementID;
            m_strName = name;
            m_strDescription = desc;
            m_bAchieved = false;
        }
    }
}
