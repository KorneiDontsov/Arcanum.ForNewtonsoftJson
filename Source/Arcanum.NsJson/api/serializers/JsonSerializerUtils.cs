﻿// Copyright (c) Kornei Dontsov. All Rights Reserved. Licensed under the MIT. See LICENSE in the project root for license information.

// disable nullable warnings

#pragma warning disable 8601

namespace Arcanum.NsJson {
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using System;

	public static class JsonSerializerUtils {
		public static T MayRead<T> (this IJsonSerializer jsonSerializer, JsonReader jsonReader) =>
			(T) jsonSerializer.MayRead(jsonReader, typeof(T));

		static Exception DeserializedNullException (Type expectedDataType) =>
			new JsonSerializationException($"'null' was deserialized instead of {expectedDataType}.");

		public static Object Read (this IJsonSerializer jsonSerializer, JsonReader jsonReader, Type dataType) =>
			jsonSerializer.MayRead(jsonReader, dataType) is { } data
				? data
				: throw DeserializedNullException(dataType);

		public static T Read<T> (this IJsonSerializer jsonSerializer, JsonReader jsonReader) =>
			(T) jsonSerializer.Read(jsonReader, typeof(T));

		public static String ToText (this IJsonSerializer jsonSerializer, Object? maybeData) {
			using var jsonTextBuilder = JsonFactory.TextWriter();
			jsonSerializer.Write(jsonTextBuilder, maybeData);
			return jsonTextBuilder.ToString();
		}

		public static Object? MayFromText (
		this IJsonSerializer jsonSerializer, String text, Type dataType) {
			using var jsonTextReader = JsonFactory.TextReader(text);
			return jsonSerializer.MayRead(jsonTextReader, dataType);
		}

		public static T MayFromText<T> (this IJsonSerializer jsonSerializer, String text) =>
			(T) jsonSerializer.MayFromText(text, typeof(T));

		public static Object FromText (this IJsonSerializer jsonSerializer, String text, Type dataType) =>
			jsonSerializer.MayFromText(text, dataType) is { } data
				? data
				: throw DeserializedNullException(dataType);

		public static T FromText<T> (this IJsonSerializer jsonSerializer, String text) =>
			(T) jsonSerializer.FromText(text, typeof(T));

		public static JToken ToToken (this IJsonSerializer jsonSerializer, Object? maybeData) {
			using var jsonTokenWriter = JsonFactory.TokenWriter();
			jsonSerializer.Write(jsonTokenWriter, maybeData);
			return jsonTokenWriter.Token;
		}

		public static Object? MayFromToken (this IJsonSerializer jsonSerializer, JToken token, Type dataType) {
			using var jsonTokenReader = JsonFactory.TokenReader(token);
			return jsonSerializer.MayRead(jsonTokenReader, dataType);
		}

		public static T MayFromToken<T> (this IJsonSerializer jsonSerializer, JToken token) =>
			(T) jsonSerializer.MayFromToken(token, typeof(T));

		public static Object FromToken (this IJsonSerializer jsonSerializer, JToken token, Type dataType) =>
			jsonSerializer.MayFromToken(token, dataType) is { } data
				? data
				: throw DeserializedNullException(dataType);

		public static T FromToken<T> (this IJsonSerializer jsonSerializer, JToken token) =>
			(T) jsonSerializer.FromToken(token, typeof(T));
	}
}