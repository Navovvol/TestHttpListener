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
			@"http://192.168.10.219:2022/ISAPI/",
			@"http://192.168.10.219:2022/",
			@"http://192.168.10.219:2023/ISAPI/",
			@"http://192.168.10.219:2023/",
			@"http://127.0.0.1/V8_TEST_DULICH_PR_CHERNOZEM_BUH/ru_RU/",
			@"http://192.168.10.219/V8_TEST_DULICH_PR_CHERNOZEM_BUH/ru_RU/",
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
							
							var record = JsonSerializer.Deserialize<Record>(body.ToString());
							Console.WriteLine(record);
							
							response.StatusCode = (int)HttpStatusCode.OK;
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
	
	public class Record
	{
		public long id { get; set; }
		public string plate { get; set; }
		public string stencil { get; set; }
		public DateTime timestamp { get; set; }
		public string time { get; set; }
		public string date { get; set; }
		public string chDirection { get; set; }
		public string movDirection { get; set; }
		public string nameDir { get; set; }
		public string status { get; set; }
		public string userList { get; set; }
		public string channelName { get; set; }
		public int channelNum { get; set; }
		public string dongle { get; set; }
		public string vehicleType { get; set; }
		public string duration { get; set; }
		public string speed { get; set; }
		public string datetimepassage { get; set; }
		public string rearplate { get; set; }
		public string namepassage { get; set; }
		public string statuspassage { get; set; }

		public override string ToString()
		{
			return string.Format(@"{0}	{1}", timestamp.ToString("dd MMMM yyyy hh:mm:ss"), plate);
		}
	}
}
