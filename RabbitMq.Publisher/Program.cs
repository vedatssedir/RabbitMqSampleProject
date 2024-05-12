
using System.Text;
using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory
{
    Uri = new Uri("amqps://zhzldpkc:EZ54e01fC011SsRIwkw1DHfhwDncM4S1@chimpanzee.rmq.cloudamqp.com/zhzldpkc")
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);
//routingKey: hangi kuyruga mesajın gideceğini belirler.
while (true)
{
    Console.WriteLine("Mesaj:");
    var message = Console.ReadLine();
    var byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("direct-exchange-example", routingKey: "direct-queue-example", body: byteMessage);
}
Console.Read();


