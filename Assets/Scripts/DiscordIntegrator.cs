using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using System;

public class DiscordIntegrator : MonoBehaviour
{
    public bool works;
    public static bool engaged;
    public static bool working;
    public static bool discordIsRunning;
    public static long startUnixTime;
    public static DiscordIntegrator theIntegrator;
    public Discord.Discord discord;
    void Awake()
    {
        // (welp taht gives me an error, so I'll try the other one)
        /*
        for (int i = 0; i < System.Diagnostics.Process.GetProcesses().Length; i++)
        {
            // checks if current process is discord
            if (System.Diagnostics.Process.GetProcesses()[i].ToString() == "System.Diagnostics.Process (Discord)")
            {
                discordIsRunning = true;
                break;
            }
        }
        */
        OldDiscordAwake();
    }

    public void OldDiscordAwake()
    {
        System.Diagnostics.Process[] pname = System.Diagnostics.Process.GetProcessesByName("Discord");
        Debug.Log(pname);
        if (pname.Length == 0)
        {
            discordIsRunning = false;
            Debug.Log("DiscordNotRunning");
        }
        else
        {
            discordIsRunning = true;
            Debug.Log("DiscordRunning");
        }
        if (discordIsRunning)
        {
            if (!engaged)
            {
                DontDestroyOnLoad(gameObject);
                engaged = true;
                working = works;
                theIntegrator = this;
                startUnixTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                discord = new Discord.Discord(954070173797744670, (ulong)CreateFlags.Default);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    /*
    void Start()
    {
        discord = new Discord.Discord(954070173797744670, (ulong)CreateFlags.Default);
    }*/
    public static void UpdateActivity(string details, string state, string largeImage, string largeImageText)
    {
        if (discordIsRunning)
        {
            var activityManager = theIntegrator.discord.GetActivityManager();
            var activity = new Activity
            {
                //Details = "Level: " + (saveData.playerLevel + 1).ToString() + " | Money: " + saveData.moneyAmount.ToString(),
                Details = details,
                State = state,
                Timestamps =
            {
                Start = startUnixTime,
            },
                Assets =
            {
                LargeImage = largeImage,
                LargeText = largeImageText,
            },
            };
            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res == Result.Ok)
                    Debug.Log("All good!");
                else
                    Debug.LogWarning("Discord failed!!!");

            });
        }
    }
    public static void UpdateActivity(string details, string state, string largeImage, string largeImageText, string smallImage, string SmallImageText)
    {
        if (discordIsRunning)
        {
            var activityManager = theIntegrator.discord.GetActivityManager();
            var activity = new Activity
            {
                //Details = "Level: " + (saveData.playerLevel + 1).ToString() + " | Money: " + saveData.moneyAmount.ToString(),
                Details = details,
                State = state,
                Timestamps =
            {
                Start = startUnixTime,
            },
                Assets =
            {
                LargeImage = largeImage,
                LargeText = largeImageText,
                SmallImage = smallImage,
                SmallText = SmallImageText,
            },
            };
            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res == Result.Ok)
                    Debug.Log("All good!");
                else
                    Debug.LogWarning("Discord failed!!!");

            });
        }
    }
    void Update()
    {
        if(discordIsRunning)
            discord.RunCallbacks();
    }
}
