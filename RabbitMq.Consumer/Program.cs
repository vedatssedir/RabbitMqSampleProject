//Baglantı olusturma

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory
{
    Uri = new Uri("amqps://zhzldpkc:EZ54e01fC011SsRIwkw1DHfhwDncM4S1@chimpanzee.rmq.cloudamqp.com/zhzldpkc")
};

//Baglantı aktifleştirme ve kanal açma 

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);
var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queueName, "direct-exchange-example", "direct-queue-example");
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
consumer.Received += ConsumerReceived; 
void ConsumerReceived(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
}
Console.Read();