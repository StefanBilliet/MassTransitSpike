using System;
using System.Collections.Generic;
using System.IO;
using MassTransit;
using MassTransit.Pipeline;
using MassTransit.Serialization;
using Newtonsoft.Json;
using Shared;

namespace HostA {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("This is the server");
      Bus.Initialize(sbc => {
        sbc.UseRabbitMq();
        sbc.ReceiveFrom("rabbitmq://guest:guest@localhost/Commands");

        sbc.Subscribe(subs =>
        {
          subs.Handler<Message>(msg =>
          {
            Console.WriteLine("Type of payload: "+msg.PayloadClrType.ToString());
            Console.WriteLine(JsonConvert.SerializeObject(msg));
          });
        });
      });
      Console.ReadKey();
    }
  }
}
