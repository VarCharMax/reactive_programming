using System.Net.Sockets;
using System.Reactive.Linq;
using System.Text;

using NetworkObservablesLib;

namespace NetworkReader;

internal class Program
{
  static void Main(string[] args)
  {
    //convert a TcpListener into an observable sequence on port 23 (telnet)
    var tcpClientsSequence = TcpListener.Create(23)
        .AcceptObservableClient()
        .AsNetworkByteSource();

    //map the source message into another with a byte buffer of a single byte
    var bufferUntilCRLFSequence = tcpClientsSequence
        .Select(x => new { x.Key, buffer = new[] { x.Value }.AsEnumerable() })
        //group by client session IPEndPoint (IP/Port)
        .GroupBy(x => x.Key);

    //a crlf byte buffer
    var crlf = "\r\n"u8.ToArray();

    //subscribe to all nested sequence groups per remote endpoint
    bufferUntilCRLFSequence.Subscribe(endpoint =>
    {
      var clientSequence = endpoint
          //apply an accumulator function to obtain the byte buffer per client
          //the function will check if the buffer terminates with the CRLF then in the case will create a new buffer otherwise it will concat the previous buffer with the new byte
          .Scan((last, i) => new { last.Key, buffer = last.buffer.Skip(last.buffer.Count() - 2).SequenceEqual(crlf) ? i.buffer : last.buffer.Concat(i.buffer) })
          //wait the CR+LF message to read per row
          .Where(x => x.buffer.Skip(x.buffer.Count() - 2)
          .SequenceEqual(crlf));

      //subscribe to the client sequence
      clientSequence.Subscribe(row =>
          Console.WriteLine("{0} -> {1}", row.Key, Encoding.ASCII.GetString([.. row.buffer])));
    });

    Console.ReadLine();
  }
}
