﻿using Demo.Peresentation.Helper;
using System.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;

namespace Demo.Peresentation.Helper
{
    public class EmailSettings
	{
		public static bool SendEmail(Email email)
		{
			try
			{
				var client = new SmtpClient("smtp.gmail.com", 587);
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential("abdalrhmanwa159@gmail.com", "txowxqvjhzsdxgzp");
				client.Send("abdalrhmanwa159@gmail.com", email.To, email.Subject, email.Body);
				return true;
			}
			catch (Exception ) 
			{
			
				return false;
			}
		}
	}
}
