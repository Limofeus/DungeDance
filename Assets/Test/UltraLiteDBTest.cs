using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltraLiteDB;
using UnityEngine.TextCore.Text;
using System;

[Serializable]
public class SomeUltraLiteTestClass
{
    public string someString;
    public SomeUltraLiteInnerClass[] inerClasses;
}

[Serializable]
public class SomeUltraLiteInnerClass
{
    public string someString;
    public int someInt;
}

public class UltraLiteDBTest : MonoBehaviour
{
    private string fileName = "C:\\Users\\Limofeus\\Desktop\\DDstuff\\ULDBtest\\ULTest.db";
    [SerializeField] private float testSaveValue = 1f;
    [SerializeField] private SomeUltraLiteTestClass ulTC;
    [SerializeField] private bool testSaveB = false;
    [SerializeField] private bool testLoadB = false;
    [SerializeField] private bool testSaveCB = false;
    [SerializeField] private bool testLoadCB = false;
    void Start()
    {
        UpsertTest();
    }

    private void Update()
    {
        if (testSaveB) UpsertTest();
        if (testLoadB) LoadTest();

        if (testSaveCB) UpsertClass();
        if (testLoadCB) LoadClass();
    }

    private void UpsertTest()
    {
        testSaveB = false;

        var db = new UltraLiteDatabase(fileName);

        var col = db.GetCollection("testsaves");
        var testSave = new BsonDocument();
        testSave["SomeFloatValue"] = testSaveValue;
        col.Upsert(testSave);
        db.Dispose();
    }

    private void LoadTest()
    {
        testLoadB = false;

        var db = new UltraLiteDatabase(fileName);
        var col = db.GetCollection("testsaves");
        List<BsonDocument> allTestSaves = new List<BsonDocument>(col.FindAll());
        float retrieveValue = 0f;
        foreach (var testSave in allTestSaves)
        {
            retrieveValue = (float)testSave["SomeFLoatValue"].AsDouble;
        }
        testSaveValue = retrieveValue;
        db.Dispose();
    }

    private void UpsertClass()
    {
        testSaveCB = false;

        var db = new UltraLiteDatabase(fileName);
        var mapper = new BsonMapper();
        mapper.UseLowerCaseDelimiter('_');
        mapper.IncludeFields = true;

        var col = db.GetCollection("testclasses");
        var testCSave = new BsonDocument();

        //SomeUltraLiteInnerClass silcO = new SomeUltraLiteInnerClass() {someString = "Abob", someInt = 69};
        //SomeUltraLiteInnerClass silcT = new SomeUltraLiteInnerClass() { someString = "Bibu", someInt = 420};
        //SomeUltraLiteInnerClass[] silcs = new SomeUltraLiteInnerClass[2] { silcO , silcT };
        //SomeUltraLiteTestClass silc = new SomeUltraLiteTestClass() { someString = "Testing", inerClasses = silcs };

        SomeUltraLiteTestClass silc = ulTC;

        Debug.Log("Saving test class");

        var testDoc = mapper.ToDocument<SomeUltraLiteTestClass>(silc);
        Debug.Log(testDoc);
        SomeUltraLiteTestClass silcUnmapped = mapper.ToObject<SomeUltraLiteTestClass>(testDoc);
        Debug.Log(silcUnmapped.someString);
        testCSave["MyTestClass1"] = testDoc;

        col.Upsert(testCSave);
        db.Dispose();
    }

    private void LoadClass()
    {
        Debug.Log("Loading class");
        testLoadCB = false;

        var db = new UltraLiteDatabase(fileName);
        var mapper = new BsonMapper();
        mapper.UseLowerCaseDelimiter('_');
        mapper.IncludeFields = true;

        var col = db.GetCollection("testclasses");
        List<BsonDocument> allTestSaves = new List<BsonDocument>(col.FindAll());

        foreach (var testSave in allTestSaves)
        {
            Debug.Log("Test save found");
            Debug.Log(testSave["MyTestClass1"].AsDocument);
            ulTC = mapper.ToObject<SomeUltraLiteTestClass>(testSave["MyTestClass1"].AsDocument);
        }
        db.Dispose();
    }
}
