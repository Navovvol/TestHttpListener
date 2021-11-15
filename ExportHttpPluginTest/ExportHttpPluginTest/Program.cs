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
using ExportHttpPluginTest;
using Newtonsoft.Json;

namespace TestHttpListener
{
	public class Program
	{
		public static IReadOnlyList<string> _prefixes = new List<string>()
		{
			@"http://127.0.0.1:47777/recorddata/",
			@"http://127.0.0.1:47777/record/",
			@"http://127.0.0.1:47777/movedetected/",
			@"http://127.0.0.1:47777/videosignal/",
			@"http://127.0.0.1:47777/rfid/",
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
						var context = listener.GetContext();
						var request = context.Request;
						using(var response = context.Response)
						using(var reader = new StreamReader(request.InputStream))
						{
							var url = request.Url?.ToString();
							var body = new StringBuilder();
							body.Append(reader.ReadToEnd());
							Console.WriteLine();
							
							Console.WriteLine("---------BEGIN----------");
							Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
							Console.WriteLine("---------URL----------");
							Console.WriteLine(url);
							
							Console.WriteLine("---------BODY----------");
							Console.WriteLine(body);
							
							Console.WriteLine("---------HEADERS----------");
							foreach(var header in request.Headers.AllKeys)
							{
								Console.WriteLine(header + ": " + request.Headers[header]);
							}
							
							Console.WriteLine("---------OBJECT----------");
							switch(request.Url?.AbsolutePath)
							{
								case "/record/":
									if(request.ContentType == "application/json")
									{
										var json = JsonConvert.DeserializeObject<CarRecordedContext>(body.ToString());
										Console.WriteLine(json);
									}
									break;

								case "/recorddata/":
									if(request.ContentType == "application/json")
									{
										var json = JsonConvert.DeserializeObject<CarRecognizedContext>(body.ToString());
										Console.WriteLine(json);
									}
									break;
								case "/movedetected/":
									if (request.ContentType == "application/json")
									{
										var json = JsonConvert.DeserializeObject<MoveDetected>(body.ToString());
										Console.WriteLine(json);
									}
									break;
								case "/videosignal/":
									if(request.ContentType == "application/json")
									{
										var json = JsonConvert.DeserializeObject<VideoSignalStateContext>(body.ToString());
										Console.WriteLine(json);
									}
									break;
								case "/rfid/":
									if(request.ContentType == "application/json")
									{
										var json = JsonConvert.DeserializeObject<RFIDReader>(body.ToString());
										Console.WriteLine(json);
									}
									break;
							}
							
							Console.WriteLine("---------END----------");

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
}
