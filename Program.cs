﻿using System;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Net.Sockets;
using Terminal.Gui;

namespace Rishi.ProxyClient
{
	public class HTTPProxyClient
	{
		TcpClient Client=new TcpClient();
		string Username;
		string Pass;
		string Target=null;
		bool UseAuth=false;
		string Proxy;
		int ProxyPort;
		HTTPProxyClient (string Target, string Proxy, int ProxyPort)
		{
			this.Target=Target;
			this.Proxy=Proxy;
			this.ProxyPort=ProxyPort;
		}
		HTTPProxyClient (string Target, string Proxy, int ProxyPort, string Username, string Password)
		{
			this.Target=Target;
			this.Proxy=Proxy;
			this.ProxyPort=ProxyPort;
			this.Username=Username;
			this.Pass=Password;
		}
		///<summary>
		///Get the Stream formed by the proxy.
		///</summary>
		public Stream GetStream()
		{
			Client.Connect(Proxy, ProxyPort);
			Stream S = Client.GetStream();
			byte[] Buffer=new byte[]{};
			string AuthStr64=Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Pass}"));
			if(UseAuth==false) 
				Buffer = Encoding.UTF8.GetBytes($"CONNECT {Target} HTTP/1.1\r\n");
			else
				Buffer = Encoding.UTF8.GetBytes($"CONNECT {Target} HTTP/1.1\r\nHost: {Target}\r\nProxy-Authorization: {AuthStr64}\r\n");
			S.Write(Buffer);
			return S;
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Application.Init ();
			var top = Application.Top;

			// Creates the top-level window to show
			var win = new Window ("MyApp") {
				X = 0,
				  Y = 1, // Leave one row for the toplevel menu

				  // By using Dim.Fill(), it will automatically resize without manual intervention
				  Width = Dim.Fill (),
				  Height = Dim.Fill ()
			};
			top.Add (win);
			var proxy = new Label ("Proxy: ") { X = 3, Y = 2 };
			var target = new Label ("Target: ") {
				X = Pos.Left (login),
				  Y = Pos.Top (login) + 1
			};
			var proxyText = new TextField ("") {
				X = Pos.Right (password),
				  Y = Pos.Top (login),
				  Width = 40
			};
			var targetText = new TextField ("") {
				Secret = true,
					   X = Pos.Left (loginText),
					   Y = Pos.Top (password),
					   Width = Dim.Width (loginText)
			};
			var login = new Label ("Login: ") { X = 3, Y = 2 };
			var password = new Label ("Password: ") {
				X = Pos.Left (login),
				  Y = Pos.Top (login) + 1
			};
			var loginText = new TextField ("") {
				X = Pos.Right (password),
				  Y = Pos.Top (login),
				  Width = 40
			};
			var passText = new TextField ("") {
				Secret = true,
					   X = Pos.Left (loginText),
					   Y = Pos.Top (password),
					   Width = Dim.Width (loginText)
			};
			Application.Run ();
			Console.WriteLine("Hello World!");
		}
	}
}	
