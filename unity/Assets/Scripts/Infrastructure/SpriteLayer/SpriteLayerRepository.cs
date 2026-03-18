// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Infrastructure.Unity.SpriteLayer
{
	public class SpriteLayerRepository : ISpriteLayerRepository
	{
		private readonly string resourceRootPath = "Heroin";
		public string Get(SpriteLayerID id, ExpressionType type)
		{
			return $"{resourceRootPath}/{id.value}/{type.type}";
		}
	}
}
