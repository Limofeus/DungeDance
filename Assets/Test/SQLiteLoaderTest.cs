using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEditor.MemoryProfiler;

public class SQLiteLoaderTest : MonoBehaviour
{
    void Start() // 13
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();

        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM Locales";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read()) // 18
        {
            Debug.Log(dataReader.GetString(0) + " | " + dataReader.GetString(1) + " | " + dataReader.GetString(2));
        }

        dbConnection.Close();
    }
    private static string GetDatabasePath()
    {
        return Path.Combine(Application.streamingAssetsPath, "Locales.db");
    }
    private IDbConnection CreateAndOpenDatabase() // 3
    {

        string dbPath = GetDatabasePath();
        IDbConnection dbConnection = new SqliteConnection("Data Source=" + dbPath);
        dbConnection.Open();

        return dbConnection;
    }
}
