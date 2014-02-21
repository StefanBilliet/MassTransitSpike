using System;
using MassTransit;
using Shared;

namespace HostB {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("This is the client");
      Bus.Initialize(sbc => {
        sbc.UseRabbitMq();
        sbc.ReceiveFrom("rabbitmq://guest:guest@localhost/test_queue_client");
      });

      Bus.Instance
        .GetEndpoint(new Uri("rabbitmq://guest:guest@localhost/Commands"))
        .Send((dynamic) new Message
        {
          PayloadClrType = typeof(CancelScheduledEventCommand),
          Payload = new CancelScheduledEventCommand {Id = Guid.NewGuid(), Reason = "Don't feel like it"}
        });


      Console.ReadKey();
    }
  }

  public class CancelScheduledEventCommand
  {
    public Guid Id { get; set; }
    public string Reason { get; set; }
  }
}
