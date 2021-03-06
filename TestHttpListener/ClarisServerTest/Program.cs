﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;
using //Newtonsoft.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ClarisServerTest
{
	class Program
	{
		private static List<ClarisAnswer> _clarisPasses = new List<ClarisAnswer>()
		{
			new ClarisAnswer("123456", "BMW", "10", "Driver01", DateTime.Now, DateTime.Now),
			new ClarisAnswer("123457", "BMW", "11", "Driver02", DateTime.Now, DateTime.Now),
			new ClarisAnswer("123458", "BMW", "12", "Driver03", DateTime.Now, DateTime.Now),
			new ClarisAnswer("123459", "BMW", "13", "Driver04", DateTime.Now, DateTime.Now),
			new ClarisAnswer("1234510", "BMW", "14", "Driver05", DateTime.Now, DateTime.Now),
			new ClarisAnswer("1234511", "BMW", "15", "Driver06", DateTime.Now, DateTime.Now),
			new ClarisAnswer("1234512", "BMW", "16", "Driver07", DateTime.Now, DateTime.Now),
			new ClarisAnswer("1234513", "BMW", "17", "Driver08", DateTime.Now, DateTime.Now),
		};

		public static IReadOnlyList<string> _prefixes = new List<string>()
		{
			@"http://127.0.0.1:47777/recognition/",
			@"http://127.0.0.1:47777/motion/",
			@"http://127.0.0.1:47777/cgi/",
			@"http://127.0.0.1:47777/",
			@"http://127.0.0.1:47777/api/rest/medium/ticket/discount/lp/",
			@"http://127.0.0.1:47777/api/rest/medium/changezone/lp/",
			@"http://127.0.0.1:47777/vNext/v1/"
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

						foreach(var keys in request.QueryString.AllKeys)
						{
							Console.WriteLine(keys + "= " + request.QueryString[keys]);
						}
						using(var response = context.Response)
						using(var reader = new StreamReader(request.InputStream))
						{
							var body = new StringBuilder();
							body.Append(reader.ReadToEnd());
							//var motionState = JsonSerializer.Deserialize<Test>(answerText.ToString());
							//Console.WriteLine(motionState.motion);
							//Console.WriteLine(motionState.videoChannelId);
							Console.Write(DateTime.Now.ToString("HH:mm:ss"));
							Console.Write(request.Url.AbsolutePath);
							Console.Write(body);
							Console.WriteLine();

							using(var sw = new StreamWriter(response.OutputStream))
							{
								var json = JsonConvert.SerializeObject(_clarisPasses);
								sw.Write(json);
							}
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
