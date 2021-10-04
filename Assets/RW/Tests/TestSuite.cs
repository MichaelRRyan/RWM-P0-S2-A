using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {

        game.isGameOver = true;
        game.NewGame();

        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {

        GameObject laser = game.GetShip().SpawnLaser();

        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator NewGameSetsScoreToZero()
    {
        game.NewGame();
        Assert.AreEqual(game.score, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShipMovementLeftAndRight()
    {
        game.NewGame();
        float xPos = game.GetShip().transform.position.x;
        game.GetShip().MoveLeft();
        Assert.Less(game.GetShip().transform.position.x, xPos);
        xPos = game.GetShip().transform.position.x;
        game.GetShip().MoveRight();
        Assert.Greater(game.GetShip().transform.position.x, xPos);
        yield return null;
    }

    [UnityTest]
    public IEnumerator NewGameSetsShipLivesToThree()
    {
        game.NewGame();
        Assert.AreEqual(game.GetShip().lives, 3);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShipMovementUp()
    {
        game.NewGame();
        float yPos = game.GetShip().transform.position.y;
        game.GetShip().MoveUp();
        Assert.Greater(game.GetShip().transform.position.y, yPos);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerLosesLifeOnHit()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(game.GetShip().lives, 2);
    }

    [UnityTest]
    public IEnumerator GameEndsAtZeroLives()
    {
        game.GetShip().LoseLife();
        game.GetShip().LoseLife();
        game.GetShip().LoseLife();
        Assert.True(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShipMovementDown()
    {
        game.NewGame();
        float yPos = game.GetShip().transform.position.y;
        game.GetShip().MoveDown();
        Assert.Less(game.GetShip().transform.position.y, yPos);
        yield return null;
    }

    [UnityTest]
    public IEnumerator intialSpeedOfAsteroid()
    {
        game.NewGame();
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float speed = asteroid.GetComponent<Asteroid>().getSpeed();
        Assert.AreEqual(speed, 1.0f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LivesTextUpdates()
    {
        game.GetShip().LoseLife();
        Assert.AreEqual(game.livesText.text, "Lives: 2");
        yield return null;
    }

    [UnityTest]
    public IEnumerator speedIncreaseOverTime()
    {
        game.NewGame();
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float Intialspeed = asteroid.GetComponent<Asteroid>().getSpeed();
        Assert.AreEqual(Intialspeed, 1.0f);
        yield return new WaitForSeconds(0.1f);
        float speedAfterTime = asteroid.GetComponent<Asteroid>().getSpeed();
        Assert.Greater(speedAfterTime, Intialspeed);
        yield return null;
    }
}
