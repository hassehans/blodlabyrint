using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class FirstTest {


    [Test]
    public void NewTestScriptSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]

    [TestCase(0.1f, 1)]
    public IEnumerator NewTestScriptWithEnumeratorPasses(float time, int directionx) {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject player = new GameObject("Player");
        Hello hello = player.AddComponent<Hello>();
        hello.Move(time, new Vector2(directionx, 0));
        Assert.AreNotEqual(player.transform.position.x, 0);
        yield return null;
    }
}