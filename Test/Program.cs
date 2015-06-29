using System;
using Autofac;
using AutofacSerilogIntegration;
using Serilog;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            Log.Logger = new LoggerConfiguration()
                      .WriteTo.Seq("http://localhost:5341")
                      .CreateLogger();

            builder.RegisterLogger(Log.Logger, true);

            var container = builder.Build();
            var v = new MyType(container);
            v.Log();
            Console.ReadLine();
        }
    }

    public class MyType
    {
        public MyType(IContainer container)
        {
            //Does not populate SourceContext
            container.InjectUnsetProperties(this);
        }

        public ILogger Logger { get; set; }

        public void Log()
        {
            Logger.Information("Test call {Now}", DateTime.Now);
        }
    }
}
