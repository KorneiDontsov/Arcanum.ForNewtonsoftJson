﻿// Copyright (c) Kornei Dontsov. All Rights Reserved. Licensed under the MIT. See LICENSE in the project root for license information.

namespace Arcanum.NsJson.Contracts {
	using Newtonsoft.Json.Serialization;

	public interface IMicroContractResolverBuilder {
		IMicroContractResolverBuilder AddCreator (IJsonContractCreator contractCreator);

		IMicroContractResolverBuilder AddFactory (IJsonContractFactory contractFactory);

		IMicroContractResolverBuilder AddPatch (IJsonContractPatch contractPatch);

		IMicroContractResolverBuilder AddMiddlewarePatch (IJsonMiddlewarePatch middlewarePatch);

		IContractResolver Build ();
	}
}