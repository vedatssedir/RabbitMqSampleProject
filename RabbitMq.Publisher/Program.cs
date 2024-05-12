using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


//Baglantı olusturma
var connectionFactory = new ConnectionFactory
{
    Uri = new Uri("amqps://zhzldpkc:EZ54e01fC011SsRIwkw1DHfhwDncM4S1@chimpanzee.rmq.cloudamqp.com/zhzldpkc")
};

//Baglantı aktifleştirme ve kanal açma 

using IConnection connection = connectionFactory.CreateConnection();

using IModel channel = connection.CreateModel();

//Queue olusturma 
//durable: kuyruktaki mesajların kalıcılıgını belirler.
//exlusive = baska bir baglantı baglanamaz false olması gerekmektedir.
//autoDelete: kuyruktaki mesaj tüketimi bittiginde silinmesi controldür.
channel.QueueDeclare(queue: "example-queue", exclusive: false, autoDelete: false);

//queue mesaj gönderme 

//Rabbitmq kuyruga atacagı byte türünden kabul etmektedir.


for (int i = 1; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Data: " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.Read();


//byte[] message = "vedatssedir"u8.ToArray();

