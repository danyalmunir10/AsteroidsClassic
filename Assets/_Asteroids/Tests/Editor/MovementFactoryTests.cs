using Zenject;
using NUnit.Framework;
using Asteroids;
using Asteroids.Movement;
using Asteroids.Utilities;
using UnityEngine;
using Asteroids.Data;

[TestFixture]
public class MovementFactoryTests : ZenjectUnitTestFixture
{
    [Test]
    public void ContainerShouldRetureMovementFactoryBindedInstance()
    {
        Container.BindFactory<LinearMovement, LinearMovement.Factory>().WhenInjectedInto<MovementFactory>();
        Container.BindFactory<ShipMovement, ShipMovement.Factory>().WhenInjectedInto<MovementFactory>();
        Container.Bind<MovementFactory>().AsSingle();

        Assert.IsNotNull(Container.Resolve<MovementFactory>());
    }

    [Test]
    public void MovementFactoryCreateInstanceOfLinearMovementWhenLinearMovementType()
    {
        Container.BindFactory<LinearMovement, LinearMovement.Factory>().WhenInjectedInto<MovementFactory>();
        Container.BindFactory<ShipMovement, ShipMovement.Factory>().WhenInjectedInto<MovementFactory>();
        Container.Bind<MovementFactory>().AsSingle();
        GameObject obj = new GameObject();
        var camera = obj.AddComponent<Camera>();
        Container.BindInstances(camera);

        Container.Bind<CameraHelper>().AsSingle();

        var movementFactory = Container.Resolve<MovementFactory>();
        var linearMovement = movementFactory.CreateMovement(MovementType.Linear);
        Assert.IsTrue(linearMovement is LinearMovement);
    }

    [Test]
    public void MovementFactoryCreateInstanceOfShipMovementWhenShipMovementType()
    {
        Container.BindFactory<LinearMovement, LinearMovement.Factory>().WhenInjectedInto<MovementFactory>();
        Container.BindFactory<ShipMovement, ShipMovement.Factory>().WhenInjectedInto<MovementFactory>();
        Container.Bind<MovementFactory>().AsSingle();
        GameObject obj = new GameObject();
        var camera = obj.AddComponent<Camera>();
        Container.BindInstances(camera);

        Container.Bind<CameraHelper>().AsSingle();
        Container.BindInstances(new ShipDataModel());

        var movementFactory = Container.Resolve<MovementFactory>();
        var shipMovement = movementFactory.CreateMovement(MovementType.Ship);
        Assert.IsTrue(shipMovement is ShipMovement);
    }
}