using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordTest : MonoBehaviour
{
    public Discord.Discord discord;
    public bool clearActivity;
    void Start()
    {
        SaveData saveData = SaveSystem.Load();
        discord = new Discord.Discord(954070173797744670, (ulong)CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Activity
        {
            //Details = "Level: " + (saveData.playerLevel + 1).ToString() + " | Money: " + saveData.moneyAmount.ToString(),
            Details = "DANCEtroing monsters!",
            State = "On swarms stage",
            Assets =
            {
                LargeImage = "disbg1",
                LargeText = "swarms",
            },
        };
        if (!clearActivity)
            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res == Result.Ok)
                    Debug.Log("All good!");
                else
                    Debug.LogWarning("Discord failed!!!");

            });
        else
            activityManager.ClearActivity((res) =>
            {
                if (res == Result.Ok)
                {
                    Debug.Log("Success!");
                }
                else
                {
                    Debug.LogWarning("Failed");
                }
            });
    }

    private void OnApplicationQuit()
    {
        var activityManager = discord.GetActivityManager();
        activityManager.ClearActivity((res) =>
        {
            if (res == Result.Ok)
            {
                Debug.Log("Success!");
            }
            else
            {
                Debug.LogWarning("Failed");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
}
