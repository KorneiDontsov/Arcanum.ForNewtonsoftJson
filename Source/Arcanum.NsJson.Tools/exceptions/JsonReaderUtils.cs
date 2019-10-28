﻿// Copyright (c) Kornei Dontsov. All Rights Reserved. Licensed under the MIT. See LICENSE in the project root for license information.

namespace Arcanum.NsJson.Tools {
	using Newtonsoft.Json;
	using System;

	public static class JsonReaderUtils {
		public static JsonReaderException Exception (
		this JsonReader jsonReader, String message, Exception? innerException) =>
			JsonExceptionFactory.ReaderException(jsonReader, message, innerException);

		public static JsonReaderException Exception (this JsonReader jsonReader, String message) =>
			JsonExceptionFactory.ReaderException(jsonReader, message);

		[StringFormatMethod("messageFormat")]
		public static JsonReaderException Exception (
		this JsonReader jsonReader,
		IFormatProvider formatProvider,
		String messageFormat,
		params Object[] messageArgs) =>
			JsonExceptionFactory.ReaderException(jsonReader, formatProvider, messageFormat, messageArgs);

		[StringFormatMethod("messageFormat")]
		public static JsonReaderException Exception (
		this JsonReader jsonReader, String messageFormat, params Object[] messageArgs
		) =>
			JsonExceptionFactory.ReaderException(jsonReader, messageFormat, messageArgs);

		public static void ReadNext (this JsonReader jsonReader) {
			if (! jsonReader.Read()) throw jsonReader.Exception("Unexpected end when reading JSON.");
		}

		public static void CurrentTokenMustBe (this JsonReader jsonReader, JsonToken expected) {
			if (jsonReader.TokenType != expected)
				throw
					jsonReader.Exception("Expected token to be {0} but accepted {1}.", expected, jsonReader.TokenType);
		}
	}
}