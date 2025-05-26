namespace Innago.Platform.Messaging.Publisher.Amqp;

using global::Amqp;

/// <summary>
/// Represents the configuration settings for AMQP (Advanced Message Queuing Protocol).
/// This class is used to define essential details required to establish
/// and configure an AMQP connection and communication.
/// </summary>
public class AmqpConfiguration
{
    /// <summary>
    /// Gets the AMQP server address configuration used for establishing the connection.
    /// This property specifies the endpoint for the AMQP server, which includes details such as
    /// the protocol, hostname, port, and credentials if required.
    /// </summary>
    public AddressInfo Address { get; set; }

    /// <summary>
    /// Gets the name of the exchange used for message publishing in the AMQP protocol.
    /// This property determines the target exchange where messages will be routed
    /// for further delivery to the appropriate queues or consumers.
    /// </summary>
    public string? ExchangeName { get; set; }

    /// <summary>
    /// Specifies the name of the AMQP sender that is used for identifying the producer of messages.
    /// This property allows customization of the sender's identifier, which can be used for logging,
    /// debugging, or message tracing purposes.
    /// </summary>
    public string? SenderName { get; set; }

    /// <summary>
    /// Represents connection details required to establish communication
    /// with a server or broker, including host, port, credentials, and scheme.
    /// </summary>
    public record struct AddressInfo(string Host, int Port, string? User = null, string? Password = null, string Path = "/", string Scheme = "AMQPS")
    {
        /// <summary>
        /// Converts the AddressInfo structure into an AMQP Address object.
        /// </summary>
        /// <returns>
        /// An instance of the AMQP Address class that encapsulates the configuration details
        /// such as host, port, user credentials, path, and scheme for establishing a connection.
        /// </returns>
        public Address ToAddress()
        {
            return new Address(this.Host, this.Port, this.User, this.Password, this.Path, this.Scheme);
        }
    }
}