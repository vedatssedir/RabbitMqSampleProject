﻿

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


channel.QueueDeclare(queue: "example-queue", exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);


channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);

consumer.Received += async (sender, e) =>
{
    //Kuyruga gelen mesaj işlendiği yerdir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    await Task.Delay(3000);
    channel.BasicAck(e.DeliveryTag, false);
};

Console.Read();