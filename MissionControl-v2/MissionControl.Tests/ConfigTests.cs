using System.Text.Json;

namespace Unity.ClusterDisplay.MissionControl.MissionControl.Tests
{
    public class ConfigTests
    {
        [Test]
        public void Equal()
        {
            Config configA = new();
            Config configB = new();
            Assert.That(configA, Is.EqualTo(configB));

            configA.ControlEndPoints = new[] { "EndPointA", "EndPointB" };
            configB.ControlEndPoints = new[] { "EndPointB", "EndPointA" };
            Assert.That(configA, Is.Not.EqualTo(configB));
            configB.ControlEndPoints = new[] { "EndPointA", "EndPointB" };
            Assert.That(configA, Is.EqualTo(configB));

            configA.LaunchPadsEntry = new("http://127.0.0.1:7000");
            configB.LaunchPadsEntry = new("http://127.0.0.1:9000");
            Assert.That(configA, Is.Not.EqualTo(configB));
            configB.LaunchPadsEntry = new("http://127.0.0.1:7000");
            Assert.That(configA, Is.EqualTo(configB));

            configA.StorageFolders = new[] { new StorageFolderConfig() { Path = "C:\\Somewhere"} };
            configB.StorageFolders = new[] { new StorageFolderConfig() { Path = "C:\\Else"} };
            Assert.That(configA, Is.Not.EqualTo(configB));
            configB.StorageFolders = new[] { new StorageFolderConfig() { Path = "C:\\Somewhere"} };
            Assert.That(configA, Is.EqualTo(configB));

            configA.HealthMonitoringIntervalSec = 42;
            configB.HealthMonitoringIntervalSec = 28;
            Assert.That(configA, Is.Not.EqualTo(configB));
            configB.HealthMonitoringIntervalSec = 42;
            Assert.That(configA, Is.EqualTo(configB));

            configA.LaunchPadFeedbackTimeoutSec = 28;
            configB.LaunchPadFeedbackTimeoutSec = 42;
            Assert.That(configA, Is.Not.EqualTo(configB));
            configB.LaunchPadFeedbackTimeoutSec = 28;
            Assert.That(configA, Is.EqualTo(configB));
        }

        [Test]
        public void SerializeDefault()
        {
            Config toSerialize = new();
            var serializedParameter = JsonSerializer.Serialize(toSerialize, Json.SerializerOptions);
            var deserialized = JsonSerializer.Deserialize<Config>(serializedParameter, Json.SerializerOptions);
            Assert.That(deserialized, Is.EqualTo(toSerialize));
        }

        [Test]
        public void SerializeFull()
        {
            Config toSerialize = new();
            toSerialize.ControlEndPoints = new[] { "EndPointA", "EndPointB" };
            toSerialize.LaunchPadsEntry = new("http://127.0.0.1:7000");
            toSerialize.StorageFolders = new[] { new StorageFolderConfig() { Path = "C:\\Somewhere"} };
            toSerialize.HealthMonitoringIntervalSec = 42;
            toSerialize.LaunchPadFeedbackTimeoutSec = 28;
            var serializedParameter = JsonSerializer.Serialize(toSerialize, Json.SerializerOptions);
            var deserialized = JsonSerializer.Deserialize<Config>(serializedParameter, Json.SerializerOptions);
            Assert.That(deserialized, Is.EqualTo(toSerialize));
        }
    }
}