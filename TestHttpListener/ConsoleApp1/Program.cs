﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace TestHttpListener
{
	public class Program
	{
		public static IReadOnlyList<string> _prefixes =  new List<string>()
		{
			@"http://127.0.0.1:47777/recognition/",
			@"http://127.0.0.1:47777/motion/",
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
							var answerText = new StringBuilder();
							answerText.Append(reader.ReadToEnd());
							Console.WriteLine(answerText);
							response.Close();
						}
						listener.Stop();
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Нажмите Enter для переподключения к Автомаршалу");
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
