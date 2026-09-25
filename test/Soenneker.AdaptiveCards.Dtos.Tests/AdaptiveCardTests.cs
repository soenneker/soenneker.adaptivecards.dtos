using System;
using System.Collections.Generic;
using System.Text.Json;
using Soenneker.AdaptiveCards.Dtos.Models;
using TUnit.Core;

namespace Soenneker.AdaptiveCards.Dtos.Tests;

public sealed class AdaptiveCardTests
{
    [Test]
    public void ModelsSerializeWithoutCustomOptions()
    {
        var card = new AdaptiveCard
        {
            Type = AdaptiveCardType.AdaptiveCard,
            Version = "1.5",
            Body = new List<ImplementationsOfElement>
            {
                ImplementationsOfElement.FromVariant16(new TextBlock
                {
                    Type = TextBlockType.TextBlock,
                    Text = "Hello",
                    Color = Colors.FromVariant1(ColorsVariant1.Accent),
                    Wrap = false
                })
            },
            Rtl = new Optional<bool?>(null)
        };
        string json = JsonSerializer.Serialize(card);
        AdaptiveCard restored = JsonSerializer.Deserialize<AdaptiveCard>(json)!;
        TextBlock text = restored.Body.Value[0].AsVariant16();
        if (text.Color.Value!.AsVariant1() != ColorsVariant1.Accent || text.Wrap.Value
            || !restored.Rtl.IsDefined || restored.Rtl.Value != null || restored.FallbackText.IsDefined)
            throw new Exception("Generated models did not preserve types and optional values.");
    }
}
