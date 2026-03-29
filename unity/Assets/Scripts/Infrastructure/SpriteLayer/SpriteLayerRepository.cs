// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Infrastructure.Unity
{
	public class SpriteLayerRepository : ISpriteLayerRepository
	{
		private readonly string resourceRootPath = "Heroin";
		public string Get(SpriteLayerID id, LayerState state)
		{
			return $"{resourceRootPath}/{id.value}/{state.type}";
		}
	}
}
