using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace NumpassServerTest
{
	internal class Program
	{
		public enum RequestType
		{
			auth = 0,
			config = 1,
			scan = 2,
			prices = 3,
		}

		private const string endpoint = "http://127.0.0.1:47777";
		public static IReadOnlyDictionary<RequestType, string> _prefixes =  new Dictionary<RequestType, string>
		{
			{ RequestType.auth,   @"/api/token-auth/" },
			{ RequestType.config, @"/api/parking/configurations/" },
			{ RequestType.scan,   @"/api/parking/scanings/" },
			{ RequestType.prices, @"/api/parking/prices/" },
		};

		private static string numberRecognizedURL = _prefixes[RequestType.scan] + "create";
		
		static readonly CancellationTokenSource cts = new CancellationTokenSource();

		private static int sessionId = 1;
		
		static void Main(string[] args)
		{
			foreach(var prefix in _prefixes)
			{
				Console.WriteLine("Get request " + prefix);
			}
			
			do
			{
				StartReceive(_prefixes.Values.Select(x => endpoint + x).ToList().AsReadOnly());
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
							var requestText = new StringBuilder();
							requestText.Append(reader.ReadToEnd());
							Console.Write(DateTime.Now.ToString("HH:mm:ss"));
							Console.Write(request.Url.AbsolutePath);
							Console.Write(requestText);
							Console.WriteLine();

							var requestType = _prefixes.FirstOrDefault(x => x.Value == request.Url.AbsolutePath).Key;
							requestType = request.Url.AbsolutePath == numberRecognizedURL
								? RequestType.scan
								: requestType;
							
							Console.WriteLine(requestType);
							var responseString = string.Empty;
							
							switch(requestType)
							{
								case RequestType.auth:
								{
									var auth = new AuthResponse()
									{
										Expiry = DateTimeOffset.Now,
										Token = Guid.NewGuid().ToString(),
										User = new NumpassUser()
										{
											Username = "numpass-test",
										}
									};
									responseString = JsonConvert.SerializeObject(auth);
								}
									break;
								case RequestType.config:
								{
									var config = new UserConfiguration()
									{
										ScaningsInterval = 20,//sec
										SendPricesInterval = 30,//sec
										PriceTimeDelta = 300,//min
									};
									responseString = JsonConvert.SerializeObject(config);
								}
									break;
								case RequestType.scan:
								{
									var session = new Session()
									{
										Id = sessionId,
									};
									responseString = JsonConvert.SerializeObject(session);
									sessionId++;
								}
									break;
							}

							if(!string.IsNullOrEmpty(responseString))
							{
								var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
								response.ContentLength64 = buffer.Length;
								var outputStream = response.OutputStream;
								outputStream.Write(buffer, 0, buffer.Length);
								outputStream.Close();
							}
							
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