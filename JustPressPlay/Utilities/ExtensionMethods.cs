﻿/*
 * Copyright 2014 Rochester Institute of Technology
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using System.Web.Mvc;
using System.Reflection;
using System.ComponentModel;

namespace JustPressPlay.Utilities
{
	/// <summary>
	/// Contains custom extension methods
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// Converts this object to a JSON string
		/// </summary>
		/// <param name="obj">The object to convert</param>
		/// <returns>The JSON string representation of the object</returns>
		public static string ToJSON(this object obj)
		{
			// Create the necessary objects
			DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
			String jsonString = "";

			// Set up the memory stream
			using (MemoryStream ms = new MemoryStream())
			{
				json.WriteObject(ms, obj);
				jsonString = Encoding.Default.GetString(ms.ToArray());
				ms.Close();
			}

			// All done
			return jsonString;
		}
	}
}