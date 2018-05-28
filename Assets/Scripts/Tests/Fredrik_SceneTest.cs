using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fredrik_SceneTest
{

    //int currentBloodLevel;
    //int bloodLoss;
    //int BloodLevel;

    //bool gamePaused;

    //GameObject player;

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
        //Scene thisScene = SceneManager.GetActiveScene();
        //Load an existing scene
        //yield return SceneManager.LoadSceneAsync("Fredrik_Scene", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Fredrik_Scene"));

        //Assert.NotNull(player);

        //Assert.Less(currentBloodLevel, BloodLevel);

        //Assert.IsTrue(gamePaused);
        yield return null;
    }
}
