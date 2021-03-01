using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;
///using Newtonsoft.Json;
using System.Text.Json;

namespace TestHttpListener
{
	public class Program
	{
		public static IReadOnlyList<string> _prefixes =  new List<string>()
		{
			@"http://127.0.0.1:47777/recognition/",
			@"http://127.0.0.1:47777/motion/",
			@"http://127.0.0.1:47777/api/rest/medium/ticket/discount/lp/",
			@"http://127.0.0.1:47777/api/rest/medium/changezone/lp/",
			@"http://127.0.0.1/V8_TEST_DULICH_PR_CHERNOZEM_BUH/ru_RU/"
		};

		static readonly CancellationTokenSource cts = new CancellationTokenSource();

		static void Main(string[] args)
		{
			foreach(var prefix in _prefixes)
			{
				Console.WriteLine("Get request " + prefix);
			}
			
			do
			{
				StartReceive(_prefixes);
			}
			while(Console.ReadKey().Key == ConsoleKey.Enter);
		}

		private static void Receive(IReadOnlyList<string> prefixes, CancellationToken cancelToken)
		{
			try
			{
				while(true)
				{
					if(cancelToken.IsCancellationRequested)
					{
						return;
					}
					if(!HttpListener.IsSupported)
					{
						Console.WriteLine("OS Not support HttpListener class");
						return;
					}

					using(var listener = new HttpListener())
					{
						foreach(var prefix in prefixes)
						{
							listener.Prefixes.Add(prefix);
						}

						listener.Start();
						var context      = listener.GetContext();
						var request      = context.Request;
						using(var response = context.Response)
						using(var reader = new StreamReader(request.InputStream))
						{
							var body = new StringBuilder();
							body.Append(reader.ReadToEnd());
							//var motionState = JsonSerializer.Deserialize<Test>(answerText.ToString());
							//Console.WriteLine(motionState.motion);
							//Console.WriteLine(motionState.videoChannelId);
							Console.Write(DateTime.Now.ToString("HH:mm:ss"));
							Console.Write("__");
							Console.Write(request.Url.ToString());
							Console.WriteLine();
							Console.WriteLine(body);
							foreach(var header in request.Headers.AllKeys)
							{
								Console.WriteLine(header + ": " + request.Headers[header]);
							}
							Console.WriteLine();
							response.Close();
						}
						listener.Stop();
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Press Enter for reconnection.");
			}
		}

		/// <summary>Start receive in thread</summary>
		public static void StartReceive(IReadOnlyList<string> prefixes)
		{
			new Thread(() =>
			{
				try
				{
					Receive(prefixes, cts.Token);
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}).Start();
		}
	}

	public class Test
	{
		public bool motion { get; set; }
		
		public int videoChannelId { get; set; }
	}
}
