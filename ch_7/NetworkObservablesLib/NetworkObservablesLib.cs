using System.Net;
using System.Net.Sockets;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace NetworkObservablesLib;

public static class NetworkObservables
{
  //the extension method must be put in a static class
  public static IObservable<TcpClient> AcceptObservableClient(this TcpListener listener)
  {
    listener.Start(4);

    return Observable.Create<TcpClient>(observer =>
    {
      var cts = new CancellationTokenSource();

      Task.Factory.StartNew(() =>
      {
        try
        {
          while (!cts.Token.IsCancellationRequested)
          {
            var client = listener.AcceptTcpClient();
            Task.Factory.StartNew(() => observer.OnNext(client), TaskCreationOptions.LongRunning);
          }
        }
        catch (ObjectDisposedException)
        {
          // Listener was stopped/disposed, exit gracefully
        }
      }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

      return Disposable.Create(() =>
      {
        cts.Cancel();
        listener.Stop();
        cts.Dispose();
      });
    });
  }

  public static IObservable<KeyValuePair<IPEndPoint, byte>> AsNetworkByteSource(this IObservable<TcpClient> source)
  {
    return Observable.Create<KeyValuePair<IPEndPoint, byte>>(observer =>
    {
      var subscription = source.Subscribe(client => Task.Factory.StartNew(() =>
        {
          try
          {
            using var stream = client.GetStream();
            int b;
            if (client.Client.RemoteEndPoint is not IPEndPoint remoteEndPoint)
            {
              // Optionally handle the null case, e.g., skip or throw
              return;
            }
            while ((b = stream.ReadByte()) >= 0)
            {
              observer.OnNext(new KeyValuePair<IPEndPoint, byte>(remoteEndPoint, (byte)b));
            }
          }
          catch
          {
            // Optionally handle exceptions (e.g., log or ignore)
          }
        }, TaskCreationOptions.LongRunning));

      return Disposable.Create(() => subscription.Dispose());
    });
  }
}
