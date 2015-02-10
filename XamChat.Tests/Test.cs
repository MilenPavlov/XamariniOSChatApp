﻿using NUnit.Framework;
using System;
using XamChat.Core;
using XamChat.Core.Fakes;
using XamChat.Core.Abstract;

namespace XamChat.Tests
{
	public static class Test
	{
		public static void SetUp()
		{
			ServiceContainer.Register<IWebService> (() => new FakeWebService {
			
				SleepDuration = 0
			});

			ServiceContainer.Register<ISettings> (() => new FakeSettings ());
		}
	}
}

