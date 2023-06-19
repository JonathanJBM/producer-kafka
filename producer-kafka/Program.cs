using Confluent.Kafka;
using Newtonsoft.Json;
using producer_kafka;

var text = File.ReadAllText("data.json");
List list = JsonConvert.DeserializeObject<List>(text);

const string topic = "Actions";
var id_producer = $"Worker {new Random().Next(1, 100)}";
var config = new ProducerConfig()
{
    BootstrapServers = "172.176.196.216:9094"
};
try
{
    using (var producer = new ProducerBuilder<string, string>(config).Build())
    {
        foreach (var item in list.objects)
        {
            var json = JsonConvert.SerializeObject(item);
            await producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = id_producer,
                Value = json
            });
        }
    }
}
catch (ProduceException<string, string> e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(e.Message);
}