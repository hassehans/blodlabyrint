using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class Simon_SceneTest
{

    int a = 1;
    int b = 1;

    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        //Reference to current scene
        //Scene someScene = SceneManager.GetActiveScene();
        //Load an existing scene
        //yield return SceneManager.LoadSceneAsync("Simon_Scene", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Simon_Scene"));

        Assert.AreEqual(a, b);
        yield return null;
    }
}